using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyMaaser.data;
using MyMaaser.web.Models;

namespace MyMaaser.web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private string _connString;
        public HomeController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult Index()
        {
            MaaserRepository rep = new MaaserRepository(_connString, User.Identity.Name);
            IndexViewModel vm = new IndexViewModel
            {
                MaaserGiven = rep.GetMaaserGiven(),
                MoneyEarned = rep.GetMoneyEarned(),
                TotalEarned = rep.GetTotalEarned(),
                TotalMaaserGiven = rep.GetTotalMaaserGiven(),
                TotalOwe = rep.GetStillOwe()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddMoney(MoneyEarned money)
        {
            if(money.Amount < 0)
            {
                return Json(new { money = "error" });
            }
            MaaserRepository rep = new MaaserRepository(_connString, User.Identity.Name);
            rep.AddAmount(money);
            var total = rep.GetStillOwe();
            return Json(new { money,  total });
            //return Json(money);
        }

        [HttpPost]
        public IActionResult AddMaaserGiven(MaaserGiven maaserGiven)
        {
            MaaserRepository rep = new MaaserRepository(_connString, User.Identity.Name);
            rep.AddMaaserGiven(maaserGiven);
            //MaaserGiven x = new MaaserGiven();
            //x = maaserGiven;
            return Json(maaserGiven.Id);
        }

        public IActionResult LastMaaserGiven(int id)
        {
            MaaserRepository rep = new MaaserRepository(_connString, User.Identity.Name);
            var x = rep.GetLastMaaserGiven(id);
            return Json(x);
        }
        public IActionResult ViewMaaserGiven()
        {
            MaaserRepository rep = new MaaserRepository(_connString, User.Identity.Name);
            return View(rep.GetMaaserGiven());
        }
       
    }
}
