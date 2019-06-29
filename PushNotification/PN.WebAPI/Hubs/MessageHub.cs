using Microsoft.AspNetCore.SignalR;
using PN.WebAPI.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PN.WebAPI
{
    public class MessageHub:Hub,IMessageHub
    {
      public override Task OnConnectedAsync()
      {
         return Clients.Client(Context.ConnectionId).SendAsync("SetConnectionId", Context.ConnectionId);
      }

      public Task SendMessage(string message)
      {
         return Clients.All.SendAsync("SendMessage", message);
      }
    }
}
