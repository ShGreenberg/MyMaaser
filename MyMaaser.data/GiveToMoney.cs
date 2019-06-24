using System;
using System.Collections.Generic;
using System.Text;

namespace MyMaaser.data
{
    public class GiveToMoney
    {
        public int MaaserGivenId { get; set; }
        public int MoneyId { get; set; }
        public int UserId { get; set; }

        public MaaserGiven MaaserGiven { get; set; }
        public MoneyEarned Money { get; set; }

        // public int UserId { get; set; }
    }
}
