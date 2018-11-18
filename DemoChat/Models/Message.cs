using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoChat.Models
{
    public class Message
    {
        public string Text { set; get; }
        public DateTime When { set; get; }
        public string Sign { set; get; }
    }
}
