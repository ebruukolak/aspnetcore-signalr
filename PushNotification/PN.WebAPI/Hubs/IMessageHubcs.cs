using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PN.WebAPI.Hubs
{
    public interface IMessageHub
    {
      Task SendMessage(string message);
    }
}
