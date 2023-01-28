using M3Practice13.Models;
using M3Practice13.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace M3Practice13.ViewModels
{
    class ClientWorkVM : BaseViewModel
    {
        public Worker RoleType { get; }

        public UserControl WorkingMode { get; set; }

        #region Выбранный клиент
        private ClientInfo selectedClient;

        public ClientInfo SelectedClient
        {
            get => selectedClient;
            set => Set(ref selectedClient, value);
        }
        #endregion


        public ClientWorkVM(Worker roleType)
        {
            RoleType = roleType;
            RoleType.ClientsInfo = Data.GetData();

            AddNewClientCommand = new Command(OnAddNewClientCommandExecute,
                                              CanAddNewClientCommandExecute);
            DeleteClientCommand = new Command(OnDeleteClientCommandExecute,
                                              CanDeleteClientCommandExecute);
        }

        #region Команды

        #region Добавить клиента
        public ICommand AddNewClientCommand { get; }

        private void OnAddNewClientCommandExecute(object p)
        {

        }

        private bool CanAddNewClientCommandExecute(object p)
            => RoleType is Manager;
        #endregion

        #region Удалить клиента
        public ICommand DeleteClientCommand { get; }

        private void OnDeleteClientCommandExecute(object p)
        {

        }

        private bool CanDeleteClientCommandExecute(object p)
        {
            if (p != null && RoleType is Manager) return true;

            return false;
        }

        #endregion

        #endregion
    }
}
