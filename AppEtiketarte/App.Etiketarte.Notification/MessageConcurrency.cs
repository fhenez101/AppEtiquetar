using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Notification
{
    public class MessageConcurrency : Message
    {
        public object Original { get; set; }
        public object Current { get; set; }
    }
}