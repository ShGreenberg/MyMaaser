using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace MyMaaser.data
{
    public class MaaserRepository
    {
        private string _connString;
        private User _user;
        public MaaserRepository(string connString, string user)
        {
            _connString = connString;
            _user = GetUser(user);
        }

        public void AddAmount(MoneyEarned money)
        {
            using (var ctx = new MaaserContext(_connString))
            {
                money.UserId = _user.Id;
                ctx.MoneyEarned.Add(money);
                ctx.SaveChanges();
            }

        }

        public void AddMaaserGiven(MaaserGiven maaserGiven)
        {
            using (var ctx = new MaaserContext(_connString))
            {
                maaserGiven.UserId = _user.Id;
                ctx.MaaserGiven.Add(maaserGiven);
                ctx.SaveChanges();
                var money = AddGiveToMoney(maaserGiven);
                if(money.Count > 1)
                {
                    maaserGiven.GiveToMoney = money.Select(i => new GiveToMoney
                    { MaaserGivenId = maaserGiven.Id, UserId = _user.Id, MoneyId = i }).ToList();
                    ctx.MaaserGiven.Attach(maaserGiven);
                    ctx.Entry(maaserGiven).State = EntityState.Modified;
                }
               


                ctx.SaveChanges();
            }
        }

        public MaaserGiven GetLastMaaserGiven(int id)
        {
            using (var ctx = new MaaserContext(_connString))
            {
                return ctx.MaaserGiven.FirstOrDefault(mg => mg.Id == id);
            }
        }

        private List<int> AddGiveToMoney(MaaserGiven maaserGiven)
        {
            List<int> moneyIds = new List<int>();
            using (var ctx = new MaaserContext(_connString))
            {
                IEnumerable<MoneyEarned> money = ctx.MoneyEarned.Where(z => z.UserId == _user.Id);
                MoneyEarned m = money.FirstOrDefault(mo => mo.PaidUp == false);
                decimal amount = maaserGiven.Amount * 10;
                if (m == null)
                {
                    return null;
                }

                while (m.PaidUp == false && amount != 0 && m.AmountLeft != 0)
                {

                    m.AmountLeft = m.AmountLeft - amount;
                    moneyIds.Add(m.Id);
                    if (m.AmountLeft < 0)
                    {
                        amount = 0 - m.AmountLeft;
                        m.AmountLeft = 0;
                        m.PaidUp = true;
                        ctx.MoneyEarned.Attach(m);
                        ctx.Entry(m).State = EntityState.Modified;
                        m = money.FirstOrDefault(mo => mo.PaidUp == false);
                        if (m == null)
                        {
                            break;
                        }
                    }
                    else if (m.AmountLeft == 0)
                    {
                        m.PaidUp = true;
                        ctx.MoneyEarned.Attach(m);
                        ctx.Entry(m).State = EntityState.Modified;
                    }
                    else
                    {
                        ctx.SaveChanges();
                        break;
                    }
                    ctx.SaveChanges();
                }

                return moneyIds;
            }
        }

        public List<MoneyEarned> GetMoneyEarned()
        {
            using (var ctx = new MaaserContext(_connString))
            {
                return ctx.MoneyEarned.Where(m => m.UserId == _user.Id).ToList();
            }
        }

        public List<MaaserGiven> GetMaaserGiven()
        {
            using (var ctx = new MaaserContext(_connString))
            {
                return ctx.MaaserGiven.Where(m => m.UserId == _user.Id).ToList();
            }
        }

        public decimal GetTotalEarned()
        {
            using (var ctx = new MaaserContext(_connString))
            {
                var total = ctx.MoneyEarned.Where(m => m.UserId == _user.Id).Sum(m => m.Amount);
                return total;
            }
        }

        public decimal GetTotalMaaserGiven()
        {
            using (var ctx = new MaaserContext(_connString))
            {
                return ctx.MaaserGiven.Where(m => m.UserId == _user.Id).Sum(m => m.Amount);
            }
        }

        public decimal GetStillOwe()
        {
            return GetTotalEarned() - GetTotalMaaserGiven() * 10;
        }

        private User GetUser(string username)
        {
            using (var ctx = new MaaserContext(_connString))
            {
                return ctx.Users.FirstOrDefault(u => u.UserName == username);
            }
        }
    }
}
