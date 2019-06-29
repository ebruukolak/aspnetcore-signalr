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
      public async Task<IActionResult> Logon(LogonViewModel logonViewModel)
      {
         using (HttpClient client = new HttpClient())
         {
            var userData = JsonConvert.SerializeObject(new User { username = logonViewModel.username, password = logonViewModel.password });
            HttpContent content = new StringContent(userData, null, "application/json");

            var result = await client.PostAsync("http://localhost:2321/api/account/Logon", content);
            if (result.IsSuccessStatusCode)
               return RedirectToAction("Index");
            return View();
         }
      }
      public IActionResult Index()
      {
         return View();
      }
      public async Task<IActionResult> LogonUsers()
      {
         using (HttpClient client = new HttpClient())
         {
            var userData = await client.PostAsync("http://localhost:2321/api/account/GetLogonUsers", null);

            if (userData.IsSuccessStatusCode)
            {
               var users = JsonConvert.DeserializeObject<IEnumerable<User>>(userData.Content.ReadAsStringAsync().Result);
               if (users.Count() > 0)
                  return View(users);
               else
               {
                  ViewBag.Message = "Tehere is no logon users";
                  return View(null);
               }     
            }
            ViewBag.Message = "Tehere is no logon users";
            return View(null);          
         }
      }
     
   }
}
