using PN.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PN.WebAPI.DAL
{
   public interface IActiveUserAccess
   {
      List<ActiveUser> GetAllActiveUsers();
      void AddActiveUsers(ActiveUser activeUser);
      void UpdateActiveUsers(ActiveUser activeUser);
      void DeleteActiveUsers(int userid);
      ActiveUser GetActiveUser(int userid);
   }
}
