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
    internal class ClientOperationsVM : BaseViewModel
    {
        private Worker worker;

        public Worker Worker
        {
            get => worker;
            set => Set(ref worker, value);
        }

        public ClientOperationsVM(Worker worker)
        {
            Worker = worker;

            OpenAccountCommand = new Command(OnOpenAccountCommandExecute);
        }

        #region Команды
        public ICommand OpenAccountCommand { get; }

        private void OnOpenAccountCommandExecute(object p)
        {


            Service.OpenNewAccount(Worker.SelectedClientInfo.Client.Id,
                                   (Worker.GetType()).Name);
        }

        #endregion
    }
}
