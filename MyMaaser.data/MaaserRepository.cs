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
                //maaserGiven.GiveToMoney = AddGiveToMoney(maaserGiven);
                List<int> moneyIds = AddGiveToMoney(maaserGiven);
                maaserGiven.UserId = _user.Id;
                ctx.MaaserGiven.Add(maaserGiven);
                ctx.SaveChanges();
                maaserGiven.GiveToMoney = moneyIds.Select(i => new GiveToMoney
                { MaaserGivenId = maaserGiven.Id, UserId = _user.Id, MoneyId = i }).ToList();
                ctx.MaaserGiven.Attach(maaserGiven);
                ctx.Entry(maaserGiven).State = EntityState.Modified;
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

        //private List<GiveToMoney> AddGiveToMoney(MaaserGiven maaserGiven)
        private List<int> AddGiveToMoney(MaaserGiven maaserGiven)
        {
            //List<GiveToMoney> hi = new List<GiveToMoney>();
            List<int> moneyIds = new List<int>();
            using (var ctx = new MaaserContext(_connString))
            {
                IEnumerable<MoneyEarned> money = ctx.MoneyEarned;
                MoneyEarned m = money.FirstOrDefault(mo => mo.PaidUp == false);
                //decimal amount = maaserGiven.Amount;
                decimal amount = maaserGiven.Amount * 10;
                if(m == null)
                {
                    return null;
                }

                while (m.PaidUp == false || amount != 0)
                {
                    if (amount > 0)
                    {
                        if(m.AmountLeft == 0)
                        {
                            break;
                        }
                        m.AmountLeft = m.AmountLeft - amount;
                        var id = maaserGiven.Id;
                        moneyIds.Add(m.Id);
                       // hi.Add(new GiveToMoney { MaaserGivenId = maaserGiven.Id, MoneyId = m.Id, UserId = _user.Id });
                        //ctx.GiveToMoney.Add(new GiveToMoney { MaaserGivenId = id, MoneyId = m.Id , UserId = _user.Id});
                        if (m.AmountLeft < 0)
                        {
                            amount = 0 - m.AmountLeft;
                            m.PaidUp = true;
                            ctx.MoneyEarned.Attach(m);
                            ctx.Entry(m).State = EntityState.Modified;
                            //hi.Add(new GiveToMoney { MaaserGivenId = maaserGiven.Id, MoneyId = m.Id });
                            //ctx.GiveToMoney.Add(new GiveToMoney { MaaserGivenId = maaserGiven.Id, MoneyId = m.Id });
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
                            
                            //hi.Add(new GiveToMoney { MaaserGivenId = maaserGiven.Id, MoneyId = m.Id });
                            //ctx.GiveToMoney.Add(new GiveToMoney { MaaserGivenId = maaserGiven.Id, MoneyId = m.Id });
                        }
                        else
                        {
                            ctx.SaveChanges();
                            break;
                        }
                    }
                    ctx.SaveChanges();
                }

                #region tried
                //while (m.PaidUp == false || m.AmountLeft != 0)
                //{
                //    if (m.AmountLeft > 0)//if comes back and still more than 0....
                //    {
                //        // m.AmountLeft = m.AmountLeft - m.Amount;
                //        // amount = 0;
                //        m.AmountLeft = m.AmountLeft - maaserGiven.Amount;
                //        if (m.AmountLeft < 0)
                //        {
                //            amount = m.AmountLeft;
                //            m.PaidUp = true;
                //            ctx.MoneyEarned.Attach(m);
                //            ctx.Entry(m).State = EntityState.Modified;
                //            hi.Add(new GiveToMoney { MaaserGivenId = maaserGiven.Id, MoneyId = m.Id });

                //           // m.AmountLeft = 0;
                //            m = money.FirstOrDefault(mo => mo.PaidUp == false);
                //            if(m == null)
                //            {
                //                break;
                //            }
                //        }
                //        else if (m.AmountLeft == 0)
                //        {
                //            m.PaidUp = true;
                //            ctx.MoneyEarned.Attach(m);
                //            ctx.Entry(m).State = EntityState.Modified;
                //            hi.Add(new GiveToMoney { MaaserGivenId = maaserGiven.Id, MoneyId = m.Id });

                //            // break;
                //        }
                //    }
                //    // if (mg.AmountLeft < 0)
                //    //{
                //    //    amount = mg.AmountLeft;
                //    //    mg.PaidUp = true;
                //    //    mg.AmountLeft = 0;
                //    //    mg = money.FirstOrDefault(mo => mo.PaidUp == false);
                //    //}
                //    //else if (mg.AmountLeft == 0)
                //    //{
                //    //    mg.PaidUp = true;
                //    //}
                //    //hi.Add(new GiveToMoney { MaaserGivenId = maaserGiven.Id, MoneyId = m.Id });
                //}
                #endregion

                // ctx.SaveChanges();
                //List<GiveToMoney> hi = moneyIds.Select(i => new GiveToMoney
                //{ MaaserGivenId = maaserGiven.Id, UserId = _user.Id, MoneyId = i }).ToList();
                // return hi;
                return moneyIds;
            }
        }

        public List<MoneyEarned> GetMoneyEarned()
        {
            using(var ctx = new MaaserContext(_connString))
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
            using(var ctx = new MaaserContext(_connString))
            {
                //var x = ctx.MoneyEarned.FromSql("SELECT SUM(M.AMOUNT) FROM MONEYEARNED m where m.userid = @id",
                //    new SqlParameter("@id", _user.Id)).ToList();
                //ctx.Database.SqlQuery
                var total = ctx.MoneyEarned.Where(m => m.UserId == _user.Id).Sum(m => m.Amount);
                return total;
               //return ctx.Database.ExecuteSqlCommand("SELECT SUM(M.AMOUNT) FROM MONEYEARNED m where m.userid = @id",
               //    new SqlParameter("@id", _user.Id));
            }
        }

        public decimal GetTotalMaaserGiven()
        {
            using (var ctx = new MaaserContext(_connString))
            {
                return ctx.MaaserGiven.Where(m => m.UserId == _user.Id).Sum(m => m.Amount);

                //return ctx.Database.ExecuteSqlCommand("SELECT SUM(M.AMOUNT) FROM MaaserGiven m where m.UserId = @id ",
                //    new SqlParameter("@id", _user.Id));
            }
        }

        public decimal GetStillOwe()
        {
            return GetTotalEarned() - GetTotalMaaserGiven()*10;
        }

        private User GetUser(string username)
        {
            using(var ctx = new MaaserContext(_connString))
            {
                return ctx.Users.FirstOrDefault(u => u.UserName == username);
            }
        }
    }
}
