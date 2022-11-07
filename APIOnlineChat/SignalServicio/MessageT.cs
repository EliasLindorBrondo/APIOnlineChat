using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIOnlineChat.SignalServicio
{
    public class MessageT
    {
        public string Message { get; set; }
        public int OrigenId { get; set; }
        public int DestinoId { get; set; }
    }
}
