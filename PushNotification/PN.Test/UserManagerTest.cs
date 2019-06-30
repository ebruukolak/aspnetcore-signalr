using PN.WebAPI.Entities;
using PN.WebAPI.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PN.Test
{
   public class UserManagerTest
   {
      IUserManager _userManager;
      public UserManagerTest(IUserManager userManager)
      {
         _userManager = userManager;
      }

      [Fact]
      public void should_authenticate()
      {
         User user = _userManager.Authenticate("ebru", "12345");
         Assert.NotNull(user);
      }
      [Fact]
      public void should_get_logon_users()
      {
         var users = _userManager.GetLogonUsers();
         Assert.NotNull(users);
      }
      [Fact]
      public void should_update_activeuser()
      {
         _userManager.UpdateActiveUser(new ActiveUser { token = "51513213212312", lastlogindate = DateTime.Now });
      }
      [Fact]
      public void should_get_activeuser()
      {
         var activeUser = _userManager.GetActiveUser(1);
         Assert.NotNull(activeUser);
      }

      [Fact]
      public void should_get_user_by_id()
      {
         var user = _userManager.GetUserById(1);
         Assert.NotNull(user);
      }
   }
}
