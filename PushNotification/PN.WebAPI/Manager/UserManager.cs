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


      public UserManager(IUserAccess userAccess)
      {
         _userAccess = userAccess;
      }

      //private List<User> users = new List<User>
      //  {
      //      new User { Id = 1, FirstName = "Ebru", LastName = "Kolak", Username = "ebrukolak", Password = "12345" }
      //  };
      public User Authenticate(string username, string password)
      {
         if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            return null;
         var user = GetUser(username,password);
         if (user == null)
            return null;
         user.password = null;

         return user;
      }

      public User GetUser(string username,string password)
      {
         return _userAccess.GetUser(username, password);
      }
   }
}
