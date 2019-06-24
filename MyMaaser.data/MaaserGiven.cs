using System;
using System.Collections.Generic;
using System.Text;

namespace MyMaaser.data
{
    public class MaaserGiven
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string ToWhere { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        //maybe just have many to many cause givemaaser with more than one money and vice versa
        //
        public IEnumerable<GiveToMoney> GiveToMoney { get; set; }
    }
}
