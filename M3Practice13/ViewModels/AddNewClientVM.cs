using DataFormat;
using M3Practice13.Controler;
using M3Practice13.Models;
using M3Practice13.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            try
            {
                if (String.IsNullOrWhiteSpace(NewClient.Surname)) throw new ClientFormatException("Фамилия не может быть пустой");
                if (String.IsNullOrWhiteSpace(NewClient.Name)) throw new ClientFormatException("Необходимо заполнить имя");
                if (String.IsNullOrWhiteSpace(NewClient.Patronymic)) throw new ClientFormatException("Заполните отчество");

                if (String.IsNullOrWhiteSpace(NewClient.Passport)) 
                    throw new ClientFormatException("Реквизиты паспорта обязательны для заполнения");
                if (!NewClient.Passport.All(c => Char.IsDigit(c)))
                    throw new ClientFormatException("Паспорт может содержать только цифры"); 

                Service.AddNewClientRequest(NewClient);
                Service.MainWindowChangeRequest(null);
            }
            catch (ClientFormatException e)
            {
                MessageBox.Show(e.Message,
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
   
        }

        private bool CanSaveNewClientCommandExecute(object p) => true;
        //{
        //    if (NewClient.Name != null
        //        && NewClient.Surname != null
        //        && NewClient.Patronymic != null
        //        && NewClient.Passport != null)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        #endregion

        #endregion
    }
}
