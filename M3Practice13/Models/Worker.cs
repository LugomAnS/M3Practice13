using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice13.Models
{
    public abstract class Worker
    {
        public ObservableCollection<ClientInfo> ClientsInfo { get; set; } = new ObservableCollection<ClientInfo>();
    }
}
