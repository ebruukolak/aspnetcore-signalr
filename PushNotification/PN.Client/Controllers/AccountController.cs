using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PN.Client.Models;
using PN.WebAPI.Entities;

namespace PN.Client.Controllers
{
   public class AccountController : Controller
   {
      [AllowAnonymous]
      public ActionResult Logon()
      {
         return View();
      }

      [HttpPost]
      public async Task<IActionResult> Logon(User user)
      {
         using (HttpClient client = new HttpClient())
         {
            var userData = JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(userData, null, "application/json");

            await client.PostAsync("http://localhost:2321/api/account/Logon", content);

            return RedirectToAction("Index");
         }
      }    
      public async Task<IActionResult> LogonUsers()
      {
         using (HttpClient client = new HttpClient())
         {
           var userData= await client.PostAsync("http://localhost:2321/api/account/GetLogonUsers",null);
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(userData.Content.ReadAsStringAsync().Result);
            return View(users);
         }
      }
      public IActionResult Index()
      {
         return View();
      }
   }
}
