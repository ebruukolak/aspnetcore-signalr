using Microsoft.AspNetCore.SignalR;
using PN.WebAPI.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PN.WebAPI
{
    public class MessageHub:Hub
    {
      //public override Task OnConnectedAsync()
      //{
      //   return Clients.Client(Context.ConnectionId).SendAsync("SetConnectionId", Context.ConnectionId);
      //}

     
    }
}
