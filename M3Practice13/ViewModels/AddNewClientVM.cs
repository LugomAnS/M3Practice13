using M3Practice13.Controler;
using M3Practice13.Models;
using M3Practice13.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace M3Practice13.ViewModels
{
    internal class AddNewClientVM : BaseViewModel
    {
        public Client NewClient { get; set; }

        public AddNewClientVM()
        {
            NewClient = new Client();

            CancelCommand = new Command(OnCancelCommandExecute);
            SaveNewClientCommand = new Command(OnSaveNewClientCommandExecute,
                                               CanSaveNewClientCommandExecute);
        }

        #region Команды

        #region Отмена добавления
        public ICommand CancelCommand { get; }

        private void OnCancelCommandExecute(object p)
        {
            Service.MainWindowChangeRequest(null);
        }
        #endregion

        #region Сохранение нового клиента
        public ICommand SaveNewClientCommand { get; }

        private void OnSaveNewClientCommandExecute(object p)
        {
            Service.AddNewClientRequest(NewClient);
            Service.MainWindowChangeRequest(null);
        }

        private bool CanSaveNewClientCommandExecute(object p)
        {
            if (NewClient.Name != null
                && NewClient.Surname != null
                && NewClient.Patronymic != null
                && NewClient.Passport != null)
            {
                return true;
            }

            return false;
        }

        #endregion

        #endregion
    }
}
