using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PN.WebAPI.Entities;
using PN.WebAPI.Hubs;
using PN.WebAPI.Manager;

namespace PN.WebAPI.Controllers
{
   [Route("api/[controller]")]
   public class AccountController : Controller
   {
      IUserManager _userManager;
      IHubContext<MessageHub> _messageHub;
      public AccountController(IUserManager userManager, IHubContext<MessageHub> messageHub)
      {
         _userManager = userManager;
         _messageHub = messageHub;
      }

      [HttpPost("Logon")]
      public IActionResult Logon([FromBody] User u)
      {
         var user = _userManager.Authenticate(u.username, u.password);
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

      [HttpPost("SendMessage")]
      public IActionResult SendMessage()
      {
         var result = _messageHub.Clients.All.SendAsync("SendMessage", "Merhaba ben burdayım");
         if (result.IsCompletedSuccessfully)
            return Ok("success");
         return NotFound("There is some error");
      }
   }
}
