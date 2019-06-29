﻿using PN.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PN.WebAPI.Manager
{
    public interface IUserManager
    {
      User Authenticate(string username, string password);
      List<User> GetLogonUsers();
      void AddActiveUser(ActiveUser activeUser);
      void UpdateActiveUser(ActiveUser activeUser);
      ActiveUser GetActiveUser(string sessionid);
   }
}
