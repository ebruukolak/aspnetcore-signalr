using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using PN.WebAPI.Entities;
using PN.WebAPI.Helpers;
using PN.WebAPI.Manager;

namespace PN.WebAPI.Controllers
{
   [Route("api/[controller]")]
   public class AccountController : Controller
   {
      IUserManager _userManager;
      IHubContext<MessageHub> _messageHub;
      private readonly AppSettings _appSettings;

      public AccountController(IUserManager userManager, IHubContext<MessageHub> messageHub, IOptions<AppSettings> appSettings)
      {
         _userManager = userManager;
         _messageHub = messageHub;
         _appSettings = appSettings.Value;
      }

      [HttpPost("Logon")]
      public IActionResult Logon([FromBody] User u)
      {
         var user = _userManager.Authenticate(u.username, u.password);
         if (user == null)
            return NotFound("Username or password is incorrect");
         var token = TokenHelper.GenerateToken(user, _appSettings.Secret);

         ModifiedActiveUser(new ActiveUser
         {
            token = token,
            userid = user.id
         });
         return Ok(new { Token = token,user });
      }

      [Authorize]
      [HttpPost("GetLogonUsers")]
      public IActionResult GetLogonUsers()
      {
         var users = _userManager.GetLogonUsers();
         if (users == null)
            return NotFound("There is no logon user.");
         else
            return Ok(users);
      }

       [Authorize]
      [HttpPost("SendMessage")]
      public IActionResult SendMessage()
      {
         var result = _messageHub.Clients.All.SendAsync("SendMessage", "Merhaba ben burdayım");
         if (result.IsCompletedSuccessfully)
            return Ok("Success");
         return NotFound("There is some error");
      }

      public void ModifiedActiveUser(ActiveUser activeUser)
      {
         if (activeUser != null)
         {
            activeUser.lastlogindate = DateTime.Now;
            if (_userManager.GetActiveUser(activeUser.userid) != null)
            {
               _userManager.UpdateActiveUser(activeUser);
            }
            else
            {
               _userManager.AddActiveUser(activeUser);
            }
         }
       }

     


   }
}
