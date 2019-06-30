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
      [Fact]
      public void should_get_all_users()
      {
         var users = _userAccess.GetAllUsers();
         Assert.NotNull(users);
      }
      [Fact]
      public void should_update_user()
      {
         var user = new User
         {
            username = "ebru",
            password = "123",
            lastlogindate = DateTime.Now
         };
         _userAccess.UpdateUser(user);
      }

      [Fact]
      public void should_get_user_by_id()
      {
         var user= _userAccess.GetUserByID(1);
         Assert.NotNull(user);
      }
   }
}
