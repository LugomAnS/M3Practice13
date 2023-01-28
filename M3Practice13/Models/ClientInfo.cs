using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice13.Models
{
    public class ClientInfo
    {
        public Client Client { get; set; }

        public ObservableCollection<Account> ClientAccounts { get; set; } = new ObservableCollection<Account>();

        public ClientInfo() 
        {
        }
    }
}
