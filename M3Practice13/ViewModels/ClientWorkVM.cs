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
                Set(ref selectedClient, value);
                Worker.SelectedClientInfo = value;
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

            Service.ChangeWorkingMode += SetWorkingMode;
            Service.NewClientToAdd += AddNewClientInfo;
            Service.OpenAccount += AddNewAccount;
            Service.CloseAccount += CloseAccount;

            AddNewClientCommand = new Command(OnAddNewClientCommandExecute,
                                              CanAddNewClientCommandExecute);
            DeleteClientCommand = new Command(OnDeleteClientCommandExecute,
                                              CanDeleteClientCommandExecute);
            ClientInfoCommand = new Command(OnClientInfoCommandExecute,
                                            CanClientInfoCommandExecute);
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
            Data.WriteData(Clients);
        }

        private void CloseAccount(MessageLog messageLog)
        {
            SelectedClient.Journal.Add(messageLog);
            Data.WriteData(Clients);
        }

        #region Команды

        #region Добавить клиента
        public ICommand AddNewClientCommand { get; }

        private void OnAddNewClientCommandExecute(object p)
        {
            Service.ChangeWorkMode("AddClient");
        }

        private bool CanAddNewClientCommandExecute(object p)
            => Worker is Manager;
        #endregion

        #region Удалить клиента
        public ICommand DeleteClientCommand { get; }

        private void OnDeleteClientCommandExecute(object p)
        {
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
            Service.ChangeWorkMode("ClientInfo", Worker);
        }

        private bool CanClientInfoCommandExecute(object p) => p != null;

        #endregion

        #endregion
    }
}
