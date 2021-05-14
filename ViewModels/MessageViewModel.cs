using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.SignalR.Demo.ViewModels
{
    public class MessageViewModel
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
