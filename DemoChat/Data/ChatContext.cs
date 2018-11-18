using DemoChat.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoChat.Data
{
    public class ChatContext
    {
        public ConcurrentBag<Message> Messages { get; set; }

        public ChatContext()
        {
            Messages = new ConcurrentBag<Message>
        {
            new Message { Text="test text 1", Sign = "user1", When = DateTime.Now },
        };
        }
    }

}
