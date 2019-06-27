using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PN.WebAPI.Entities
{
   public class User
   {
      public int id { get; set; }
      public string username { get; set; }
      public string password { get; set; }
   }
}
