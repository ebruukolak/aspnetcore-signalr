using PN.WebAPI.DAL;
using PN.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PN.Test
{
   public class ActiveUserAccessTest
   {
      IActiveUserAccess _activeUserAccess;
      public ActiveUserAccessTest(IActiveUserAccess activeUserAccess)
      {
         _activeUserAccess = activeUserAccess;
      }
      [Fact]
      public void should_add_activeuser()
      {
         var activeUser = new ActiveUser
         {
            userid = 1,
            token = "45465465465455465465",
            lastlogindate = DateTime.Now,
         };
         _activeUserAccess.AddActiveUsers(activeUser);
      }
      [Fact]
      public void should_delete_activeuser()
      {
         _activeUserAccess.DeleteActiveUsers(2);
      }
      [Fact]
      public void should_update_activeuser()
      {
         _activeUserAccess.UpdateActiveUsers(new ActiveUser { token="51513213212312",lastlogindate=DateTime.Now});
      }
      [Fact]
      public void should_get_all_activeuser()
      {
         var activeUsers=_activeUserAccess.GetAllActiveUsers();
         Assert.NotNull(activeUsers);
      }
      [Fact]
      public void should_get_activeuser()
      {
         var activeUser = _activeUserAccess.GetActiveUser(1);
         Assert.NotNull(activeUser);
      }

   }
}
