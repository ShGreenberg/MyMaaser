using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMaaser.data
{
    public class UsersRepository
    {
        private string _connString;
        public UsersRepository(string connString)
        {
            _connString = connString;
        }

        public string AddUser(User user, string password)
        {
            user.HashedPassword = PasswordHelper.HashPassword(password);
            using (var ctx = new MaaserContext(_connString))
            {
                if (ctx.Users.FirstOrDefault(u => u.UserName == user.UserName) != null)
                {
                    return "Username Already Used";
                }
                ctx.Users.Add(user);
                ctx.SaveChanges();
                return "";
            }
        }

        public User GetUser(string username)
        {
            using (var ctx = new MaaserContext(_connString))
            {
                return ctx.Users.FirstOrDefault(u => u.UserName == username);
            }
        }


        public User Login(string username, string password)
        {
            User user = GetUserByUsername(username);
            if (user == null)
            {
                return null;
            }
            if (PasswordHelper.PasswordMatch(password, user.HashedPassword))
            {
                return user;
            }
            return null;
        }

        private User GetUserByUsername(string username)
        {
            using (var ctx = new MaaserContext(_connString))
            {
                return ctx.Users.FirstOrDefault(u => u.UserName == username);
            }
        }
    }
}
