using AspNetCore.SignalR.Demo.Services;
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
        private IMessageService _messageService;

        public MessageHub(ILoggerFactory loggerFactory, IMessageService messageService)
        {
            _logger = loggerFactory.CreateLogger<MessageHub>();
            _messageService = messageService;
        }

        public override async Task OnConnectedAsync()
        {
            //create a new group
            var groupName = await _messageService.CreateGroupName(Context.ConnectionId);

            //add to group
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

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
            var groupName = await _messageService.GetGroupName(Context.ConnectionId);

            var message = new MessageViewModel
            {
                Id = Guid.NewGuid(),
                Message = messageText,
                TimeStamp = DateTimeOffset.UtcNow
            };

            //  send to specific group
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message.Id, message.Message, message.TimeStamp);
        }
    }
}
