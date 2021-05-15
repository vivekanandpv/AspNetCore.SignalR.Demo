using AspNetCore.SignalR.Demo.ViewModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.SignalR.Demo.SignalR
{
    public class MessageHub : Hub
    {
        private ILogger<MessageHub> _logger;

        public MessageHub(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MessageHub>();
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("ReceiveMessage", Guid.NewGuid(), "Welcome to SignalR");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogInformation("Client disconnected");
            await base.OnDisconnectedAsync(exception);
        }

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
