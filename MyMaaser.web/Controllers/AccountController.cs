using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyMaaser.data;

namespace MyMaaser.web.Controllers
{
    public class AccountController : Controller
    {
        private string _connString;
        public AccountController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user, string password)
        {
            UsersRepository rep = new UsersRepository(_connString);
            rep.AddUser(user, password);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            UsersRepository rep = new UsersRepository(_connString);
            User user = rep.Login(username, password);
            if(user == null)
            {
                return RedirectToAction("Login");
            }
            var claims = new List<Claim>
            {
                new Claim("user", username)
            };
            HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(
                claims, "Cookies", "user", "role"))).Wait();
            return RedirectToAction("index", "home");
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("login", "account");
        }
    }
}