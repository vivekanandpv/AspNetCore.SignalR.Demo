using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.SignalR.Demo.Services
{
    public interface IMessageService
    {
        Task<string> CreateGroupName(string connectionId);
        Task<string> GetGroupName(string connectionId);
    }

    public class MessageService : IMessageService
    {
        private readonly IDictionary<string, string> _store 
            = new Dictionary<string, string>();

        public Task<string> CreateGroupName(string connectionId)
        {
            var groupName = Guid.NewGuid().ToString();
            _store.Add(groupName, connectionId);

            return Task.FromResult(groupName);
        }

        public Task<string> GetGroupName(string connectionId)
        {
            var groupName = _store.FirstOrDefault(s => s.Value == connectionId);

            if (groupName.Key != null)
            {
                return Task.FromResult(groupName.Key);
            } else
            {
                throw new ArgumentException("Could not find the group");
            }
        }
    }
}
