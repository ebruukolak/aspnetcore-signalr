using PN.WebAPI.DAL;
using PN.WebAPI.Entities;
using System;
using Xunit;

namespace PN.Test
{
   public class UserAccessTest
   {
      IUserAccess _userAccess;
      public UserAccessTest(IUserAccess userAccess)
      {
         _userAccess = userAccess;
      }
      
      [Fact]
      public void should_get_user()
      {
         User user = _userAccess.GetUser("ebru", "12345");
         Assert.NotNull(user);
      }
   }
}
