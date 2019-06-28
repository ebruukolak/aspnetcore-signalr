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
   }
}
