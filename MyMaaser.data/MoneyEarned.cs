using System;
using System.Collections.Generic;

namespace MyMaaser.data
{
    public class MoneyEarned
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string RecievedFrom { get; set; }

        public decimal AmountLeft { get; set; }
        public bool PaidUp { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public IEnumerable<GiveToMoney> GiveToMoney { get; set; }
    }
}
