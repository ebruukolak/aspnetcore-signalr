using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PN.WebAPI.Entities;

namespace PN.WebAPI.DAL
{
   public class UserAccess : IUserAccess
   {

      public User GetUser(string username,string password)
      {
         using(var context=new EFContext())
         {
            return context.Set<User>().FirstOrDefault(x=>x.username==username && x.password==password);
         }
      }
   }
}
