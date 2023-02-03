using M3Practice13.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice13.Models
{
    public class MessageLog : BaseViewModel
    {
        public int ClientID { get; set; }
        public DateTime RecordTime { get; set; }
        public string Message { get; set; }
        public string Operator { get; set; }

        private bool isReaded;
        public bool IsReaded 
        {
            get => isReaded;
            set => Set(ref isReaded, value);
        }

        public MessageLog()
        {
        }

    }
}
