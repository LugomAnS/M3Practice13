using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice13.Models
{
    public class MessageLog
    {
        public int ClientID { get; set; }
        public DateTime RecordTime { get; set; }
        public string Message { get; set; }
        public string Operator { get; set; }
        public bool IsReaded { get; set; }

        public MessageLog()
        {
        }

    }
}
