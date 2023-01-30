using M3Practice13.Controler;
using M3Practice13.Models;
using M3Practice13.ViewModels.Base;
using M3Practice13.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace M3Practice13.ViewModels
{
    class ClientWorkVM : BaseViewModel
    {
        public Worker Worker { get; set; }

        #region Режим работы
        private UserControl? workingMode;
        public UserControl? WorkingMode 
        { 
            get => workingMode;
            set => Set(ref workingMode, value);
        }
        #endregion

        #region Выбранный клиент
        private ClientInfo selectedClient;

        public ClientInfo SelectedClient
        {
            get => selectedClient;
            set
            {
                if (value != null)
                {
                    Set(ref selectedClient, value);
                    Worker.SelectedClientInfo = value;
                }
            }
        }
        #endregion

        #region Список клиентов
        private ObservableCollection<ClientInfo> clients;

        public ObservableCollection<ClientInfo> Clients
        {
            get => clients;
            set => Set(ref clients, value);
        }
        #endregion

        public ClientWorkVM(Worker roleType)
        {
            Worker = roleType;
            Clients = Data.GetData();

            Service.MainWindowChange += SetWorkingMode;
            Service.AddNewClient += AddNewClientInfo;
            Service.OpenAccount += AddNewAccount;
            Service.CloseAccount += MessageRecieve;
            Service.AccountFill += MessageRecieve;
            Service.StatusRequest += WorkerStatus;
            Service.AllAccounts += AllAccounts;
            Service.TransactionBetweenAccounts += TransactionReceive;
            Service.SaveDataBase += SaveDataBase;

            AddNewClientCommand = new Command(OnAddNewClientCommandExecute,
                                              CanAddNewClientCommandExecute);
            DeleteClientCommand = new Command(OnDeleteClientCommandExecute,
                                              CanDeleteClientCommandExecute);
            ClientInfoCommand = new Command(OnClientInfoCommandExecute,
                                            CanClientInfoCommandExecute);
        }

        private void SaveDataBase() => Data.WriteData(Clients);

        public string WorkerStatus() => (Worker.GetType()).Name;

        private ObservableCollection<Account> AllAccounts()
        {
            ObservableCollection<Account> accounts = new ObservableCollection<Account>();

            foreach (var cinfo in Clients)
            {
                foreach (var item in cinfo.ClientAccounts)
                {
                    accounts.Add(item);
                }
            }

            return accounts;
        }

        /// <summary>
        /// Установка View для режима работы
        /// </summary>
        /// <param name="userControl">View для установки</param>
        private void SetWorkingMode(UserControl? userControl)
        {
            WorkingMode = userControl;
        }

        /// <summary>
        /// Сохранение новых сведений о клиенте в БД
        /// </summary>
        /// <param name="clientInfo">Информация о клиенте</param>
        private void AddNewClientInfo(ClientInfo clientInfo)
        {
            Clients.Add(clientInfo);
            Data.WriteData(Clients);
        }

        private void AddNewAccount(Account account, MessageLog messageLog)
        {
            SelectedClient.ClientAccounts.Add(account);
            SelectedClient.Journal.Add(messageLog);
            SelectedClient.NewMessagesRefresh();
            Data.WriteData(Clients);
        }

        private void MessageRecieve(MessageLog messageLog)
        {
            SelectedClient.Journal.Add(messageLog);
            SelectedClient.NewMessagesRefresh();
            Data.WriteData(Clients);
        }

        private void TransactionReceive(MessageLog messageWithdraw, MessageLog messageFill)
        {
            SelectedClient.Journal.Add(messageWithdraw);

            var clientFill = Clients.First(ci => ci.Client.Id == messageFill.ClientID);

            clientFill.Journal.Add(messageFill);

            SelectedClient.NewMessagesRefresh();
            clientFill.NewMessagesRefresh();

            Data.WriteData(Clients);
        }

        #region Команды

        #region Добавить клиента
        public ICommand AddNewClientCommand { get; }

        private void OnAddNewClientCommandExecute(object p)
        {
            Service.MainWindowChangeRequest("AddClient");
        }

        private bool CanAddNewClientCommandExecute(object p)
            => Worker is Manager;
        #endregion

        #region Удалить клиента
        public ICommand DeleteClientCommand { get; }

        private void OnDeleteClientCommandExecute(object p)
        {
            Service.MainWindowChangeRequest(null);
            Clients.Remove(Worker.SelectedClientInfo);
            Data.WriteData(Clients);
        }

        private bool CanDeleteClientCommandExecute(object p)
        {
            if (p != null && Worker is Manager) return true;

            return false;
        }

        #endregion

        #region Работа с клиентом
        public ICommand ClientInfoCommand { get; }

        private void OnClientInfoCommandExecute(object p)
        {
            Service.MainWindowChangeRequest("ClientInfo", Worker);
        }

        private bool CanClientInfoCommandExecute(object p) => p != null;

        #endregion

        #endregion
    }
}
