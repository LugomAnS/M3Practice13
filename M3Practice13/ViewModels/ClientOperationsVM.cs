using M3Practice13.Controler;
using M3Practice13.Models;
using M3Practice13.ViewModels.Base;
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

        #region Выбранный счет
        private Account selectedAccount;

        public Account SelectedAccount
        {
            get => selectedAccount;
            set => Set(ref selectedAccount, value);
        }

        #endregion

        public ClientOperationsVM(Worker worker)
        {
            Worker = worker;

            OpenAccountCommand = new Command(OnOpenAccountCommandExecute);
            CloseAccountCommand = new Command(OnCloseAccountCommandExecute,
                                              CanCloseAccountCommandExecute);
        }

        #region Команды

        #region Открыть счет
        public ICommand OpenAccountCommand { get; }

        private void OnOpenAccountCommandExecute(object p)
        {
            Service.OpenNewAccount(Worker.SelectedClientInfo.Client.Id,
                                   (Worker.GetType()).Name);
        }
        #endregion

        #region Закрыть счет
        public ICommand CloseAccountCommand { get; }

        private void OnCloseAccountCommandExecute(object p)
        {
            Service.ClosingAccount(p as Account,
                                   (Worker.GetType().Name));
        }

        private bool CanCloseAccountCommandExecute(object p)
        {
            if (p != null && ((Account)p).ClosingTime == null && Worker is Manager)
            {
                return true;
            }

            return false;
        }

        #endregion

        #endregion
    }
}
