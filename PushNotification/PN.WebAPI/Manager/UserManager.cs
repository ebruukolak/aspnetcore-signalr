using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PN.WebAPI.DAL;
using PN.WebAPI.Entities;

namespace PN.WebAPI.Manager
{
   public class UserManager : IUserManager
   {
      private IUserAccess _userAccess;
      private const int staticExpireTime = 1;

      public UserManager(IUserAccess userAccess)
      {
         _userAccess = userAccess;
      }

      public User Authenticate(string username, string password)
      {
         if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            return null;
         var user = GetUser(username, password);
         if (user == null)
            return null;
         else
         {
            user.lastlogindate = DateTime.Now;
            _userAccess.UpdateUser(user);
            user.password = null;           
         }
         return user;
      }

      public User GetUser(string username, string password)
      {
         return _userAccess.GetUser(username, password);
      }


      public List<User> GetLogonUsers()
      {
         var users = _userAccess.GetAllUsers();
         int userExpire = default(int);
         List<User> logonUsers = new List<User>();
         foreach (var item in users)
         {
            userExpire = (item.lastlogindate - DateTime.Now.Date).Days;
            if (staticExpireTime > userExpire)
               logonUsers.Add(item);
         }
         return logonUsers;
      }
   }
}
