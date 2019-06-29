using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PN.WebAPI.Entities;

namespace PN.WebAPI.DAL
{
   public class ActiveUserAccess : IActiveUserAccess
   {

      public void AddActiveUsers(ActiveUser activeUser)
      {
         using (var context = new EFContext())
         {
            var au = context.Entry(activeUser);
            au.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            context.SaveChanges();
         }
      }

      public void DeleteActiveUsers(int id)
      {
         using (var context = new EFContext())
         {
            var au = context.activeUsers.FirstOrDefault(x => x.id == id);
            if (au != null)
            {
               context.Remove(au);
               context.SaveChanges();
            }

         }
      }
      public void UpdateActiveUsers(ActiveUser activeUser)
      {
         using (var context = new EFContext())
         {
            var au = context.Entry(activeUser);
            au.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
         }
      }
      public List<ActiveUser> GetAllActiveUsers()
      {
         using (var context = new EFContext())
         {
            return context.Set<ActiveUser>().ToList();
         }
      }

      public ActiveUser GetActiveUser(int userid)
      {
         using (var context = new EFContext())
         {
            return context.Set<ActiveUser>().FirstOrDefault(x => x.userid ==userid  );
         }
      }
   }
}
