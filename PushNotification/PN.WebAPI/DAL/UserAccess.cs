using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PN.WebAPI.Entities;

namespace PN.WebAPI.DAL
{
   public class UserAccess : IUserAccess
   {
      public User GetUser(string username, string password)
      {
         using (var context = new EFContext())
         {
            return context.Set<User>().FirstOrDefault(x => x.username == username && x.password == password);
         }
      }
      public List<User> GetAllUsers()
      {
         using(var context=new EFContext())
         {
            return context.Set<User>().ToList();
         }
      }
      public void UpdateUser(User user)
      {
         using (var context = new EFContext())
         {
            var updatedEntity = context.Entry(user);
            updatedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
         }
      }

      public User GetUserByID(int id)
      {
         using (var context = new EFContext())
         {
            return context.Set<User>().FirstOrDefault(x => x.id == id);
         }
      }
   }
}
