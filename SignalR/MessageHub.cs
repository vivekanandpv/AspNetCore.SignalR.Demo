using AspNetCore.SignalR.Demo.ViewModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.SignalR.Demo.SignalR
{
    public class MessageHub : Hub
    {
        public async Task SendMessageAsync(string messageText)
        {
            var message = new MessageViewModel
            {
                Id = Guid.NewGuid(),
                Message = messageText,
                TimeStamp = DateTimeOffset.UtcNow
            };

            //  send all
            await Clients.All.SendAsync("ReceiveMessage", message.Id, message.Message, message.TimeStamp);
        }
    }
}
