using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PN.WebAPI.Entities;
using PN.WebAPI.Manager;

namespace PN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
      IUserManager _userManager;
      public AccountController(IUserManager userManager)
      {
         _userManager = userManager;
      }

      [HttpPost("Logon")]
      public IActionResult Logon([FromBody] User u)
      {
         var user = _userManager.Authenticate(u.username , u.password);
         if (user == null)
            return NotFound("Username or password is incorrect");
         return Ok(user);
      }

      [HttpPost("GetLogonUsers")]
      public IActionResult GetLogonUsers()
      {
         var users = _userManager.GetLogonUsers();
         if (users == null)
            return NotFound("There is no logon user.");
         else
            return Ok(users);
      }
   }
}
