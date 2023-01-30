using M3Practice13.Controler;
using M3Practice13.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice13.Models
{
    public class ClientInfo : BaseViewModel
    {
        public Client Client { get; set; }

        public ObservableCollection<Account> ClientAccounts { get; set; } = new ObservableCollection<Account>();

        private ObservableCollection<MessageLog> journal = new ObservableCollection<MessageLog>();

        public ObservableCollection<MessageLog> Journal 
        {
            get => journal;
            set
            {
                journal = value;
                NewMessagesRefresh();
                OnPropertyChanged();
            }
        }

        private int? unreadedMessages;

        public int? UnreadedMessages
        {
            get => unreadedMessages;
            set 
            {
                unreadedMessages = value;
                OnPropertyChanged();
            }

        }

        public ClientInfo() 
        {
        }

        public void NewMessagesRefresh()
        {
            int count = 0;
            if (journal != null && journal.Count > 0)
            {
                count = (from m in Journal
                             where m.IsReaded == false
                             select m).Count();
            }
            if (count > 0)
            {
                UnreadedMessages = count;
            }
            else
            {
                UnreadedMessages = null;
            }
            Service.SaveDataBaseRequest();
        }
    }
}
