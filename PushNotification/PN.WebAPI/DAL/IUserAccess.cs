using PN.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PN.WebAPI.DAL
{
   public interface IUserAccess
   {
      User GetUser(string username, string password);
      List<User> GetAllUsers();
      void UpdateUser(User user);
      User GetUserByID(int id);
   }
}
