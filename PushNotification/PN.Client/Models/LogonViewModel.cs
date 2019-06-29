using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PN.Client.Models
{
   public class LogonViewModel
   {
      public int id { get; set; }
      [Required(ErrorMessage = "Please,enter username")]
      [Display(Name = "User Name")]
      public string username { get; set; }
      [DataType(DataType.Password)]
      [Required(ErrorMessage = "Please,enter password")]     
      [Display(Name = "Password")]
      public string password { get; set; }

   }
}
