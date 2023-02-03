using M3Practice13.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice13.Models
{
    public abstract class Worker : BaseViewModel
    {
        private ClientInfo selectedClientInfo;
       
        public ClientInfo SelectedClientInfo 
        {
            get => selectedClientInfo;
            set
            {
                Set(ref selectedClientInfo, value);
                OnPropertyChanged(nameof(ClientName));
                OnPropertyChanged(nameof(ClientSurname));
                OnPropertyChanged(nameof(ClientPatronymic));
                OnPropertyChanged(nameof(ClientPassport));
            }
        }
   
        public string ClientName
        {
            get => SelectedClientInfo.Client.Name;
        }

        public string ClientSurname
        {
            get => SelectedClientInfo.Client.Surname;
        }

        public string ClientPatronymic
        {
            get => SelectedClientInfo.Client.Patronymic;
        }

        public abstract string ClientPassport { get; }

    }
}
