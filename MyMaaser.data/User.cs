using System;
using System.Collections.Generic;
using System.Text;

namespace MyMaaser.data
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }

        public IEnumerable<MoneyEarned> MoneyEarned { get; set; }
        public IEnumerable<MaaserGiven> MaaserGiven { get; set; }
    }
}
