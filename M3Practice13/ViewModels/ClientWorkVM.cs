using M3Practice13.Controler;
using M3Practice13.Models;
using M3Practice13.ViewModels.Base;
using M3Practice13.Views;
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

        private UserControl? workingMode;
        public UserControl? WorkingMode 
        { 
            get => workingMode;
            set => Set(ref workingMode, value);
        }

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

            Service.ChangeWorkingMode += SetWorkingMode;
            Service.NewClientToAdd += AddNewClientInfo;

            AddNewClientCommand = new Command(OnAddNewClientCommandExecute,
                                              CanAddNewClientCommandExecute);
            DeleteClientCommand = new Command(OnDeleteClientCommandExecute,
                                              CanDeleteClientCommandExecute);
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
            RoleType.ClientsInfo.Add(clientInfo);
            Data.WriteData(RoleType.ClientsInfo);
        }

        #region Команды

        #region Добавить клиента
        public ICommand AddNewClientCommand { get; }

        private void OnAddNewClientCommandExecute(object p)
        {
            Service.ChangeWorkMode("AddClient");
        }

        private bool CanAddNewClientCommandExecute(object p)
            => RoleType is Manager;
        #endregion

        #region Удалить клиента
        public ICommand DeleteClientCommand { get; }

        private void OnDeleteClientCommandExecute(object p)
        {
            RoleType.ClientsInfo.Remove(SelectedClient);
            Data.WriteData(RoleType.ClientsInfo);
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
