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
      private IActiveUserAccess _activeUserAccess;
      private const int staticExpireTime = 1;

      public UserManager(IUserAccess userAccess, IActiveUserAccess activeUserAccess)
      {
         _userAccess = userAccess;
         _activeUserAccess = activeUserAccess;
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
         var activeUsers = _activeUserAccess.GetAllActiveUsers();
         int userExpire = default(int);
         List<User> logonUsers = new List<User>();
         foreach (var item in activeUsers)
         {
            if (item.lastlogindate.Date != DateTime.Now.Date)
            {
               userExpire = (DateTime.Now.Date - item.lastlogindate.Date).Days;
               if (userExpire > staticExpireTime)
               {
                  var user = _userAccess.GetUserByID(item.userid);
                  logonUsers.Add(user);
               }
               else
               {
                  _activeUserAccess.DeleteActiveUsers(item.id);
               }

            }
            else
            {
               var user = _userAccess.GetUserByID(item.userid);
               logonUsers.Add(user);
            }
         }
         return logonUsers;
      }

      public void AddActiveUser(ActiveUser activeUser)
      {
         _activeUserAccess.AddActiveUsers(activeUser);
      }

      public void UpdateActiveUser(ActiveUser activeUser)
      {
         _activeUserAccess.UpdateActiveUsers(activeUser);
      }

      public ActiveUser GetActiveUser(int userid)
      {
         if (userid>0)
            return _activeUserAccess.GetActiveUser(userid);
         return null;
      }

      public User GetUserById(int id)
      {
         if (id > 0)
            return _userAccess.GetUserByID(id);
         return null;
      }
   }
}
