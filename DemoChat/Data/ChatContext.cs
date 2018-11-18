using DemoChat.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoChat.Data
{
    public class ChatContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public ChatContext(DbContextOptions options) : base(options) { }
    }

}
