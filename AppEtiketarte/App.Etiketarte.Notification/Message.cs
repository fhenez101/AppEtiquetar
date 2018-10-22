using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Notification
{
    public class Message
    {
        public int Id { get; set; }
        public MessageLevel Level { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}