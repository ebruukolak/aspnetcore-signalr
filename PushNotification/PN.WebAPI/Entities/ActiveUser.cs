using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PN.WebAPI.Entities
{
   public class ActiveUser
   {
      [Key]
      public int id { get; set; }
      public string sessionid { get; set; }
      public int userid { get; set; }
      public DateTime lastlogindate { get; set; }
         
   }
}
