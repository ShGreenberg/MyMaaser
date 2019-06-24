using MyMaaser.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMaaser.web.Models
{
    public class IndexViewModel
    {
        public List<MoneyEarned> MoneyEarned { get; set; }
        public List<MaaserGiven> MaaserGiven { get; set; }
        public decimal TotalEarned { get; set; }
        public decimal TotalMaaserGiven { get; set; }
        public decimal TotalOwe { get; set; }
    }
}
