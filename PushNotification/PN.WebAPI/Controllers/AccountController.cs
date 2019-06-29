using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PN.WebAPI.Entities;
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
      public User Logon([FromBody] User u)
      {
         var user = _userManager.Authenticate(u.username, u.password);
         return user;
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

      [HttpPost("AddActiveUser")]
      public IActionResult AddActiveUser([FromBody] ActiveUser activeUser)
      {
         if (ModelState.IsValid)
         {
            _userManager.AddActiveUser(activeUser);
            return Ok(HttpStatusCode.Created);
         }
         else
            return Ok(HttpStatusCode.BadRequest);
      }

      [HttpPost("UpdateActiveUser")]
      public IActionResult UpdateActiveUser([FromBody] ActiveUser activeUser)
      {
         if (ModelState.IsValid)
         {
            _userManager.UpdateActiveUser(activeUser);
            return Ok(HttpStatusCode.Created);
         }
         else
            return Ok(HttpStatusCode.BadRequest);
      }

     // [HttpGet("GetBySessionId")]
      //public ActiveUser GetBySessionId(string sessionid)
      //{
      //   var activeUser = _userManager.GetBySessionId(sessionid);
      //   if (activeUser == null)
      //      return null;
      //   else
      //      return activeUser;
      //}

      [HttpPost("ModifiedActiveUser")]
      public IActionResult ModifiedActiveUser([FromBody] ActiveUser activeUser)
      {
         if (activeUser != null)
         {
            activeUser.lastlogindate = DateTime.Now;
           if(_userManager.GetActiveUser(activeUser.sessionid) != null)
            {
               _userManager.UpdateActiveUser(activeUser);
               return Ok(HttpStatusCode.Accepted);
            }
            else
            {
               _userManager.AddActiveUser(activeUser);
               return Ok(HttpStatusCode.Created);
            }
         }
         return Ok(HttpStatusCode.NotFound);
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
