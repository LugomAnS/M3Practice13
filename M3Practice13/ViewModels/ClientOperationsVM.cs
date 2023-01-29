using M3Practice13.Controler;
using M3Practice13.Models;
using M3Practice13.ViewModels.Base;
using M3Practice13.Views;
using System;
using System.Windows.Controls;
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

        #region Работа с переводами
        private UserControl? balanceWork;

        public UserControl? BalanceWork
        {
            get => balanceWork;
            set => Set(ref balanceWork, value);
        }
        #endregion

        #region Выбранный счет
        private Account selectedAccount;

        public Account SelectedAccount
        {
            get => selectedAccount;
            set
            {
                Set(ref selectedAccount, value);
                Service.AccountChanged(selectedAccount);
                if (value == null)
                {
                    Service.ChangeBalanceWorkingMode(null);
                }
            }
        }

        #endregion

        public ClientOperationsVM(Worker worker)
        {
            Worker = worker;

            Service.ChangeBalanceWork += BalanceWorkSet;

            OpenAccountCommand = new Command(OnOpenAccountCommandExecute);
            CloseAccountCommand = new Command(OnCloseAccountCommandExecute,
                                              CanCloseAccountCommandExecute);
            AccountReplenishCommand = new Command(OnAccountReplenishCommandExecute,
                                                  CanAccountReplenishCommandExecute);
        }

        private void BalanceWorkSet(UserControl? userControl)
        {
            BalanceWork = userControl;
        }

        #region Команды

        #region Открыть счет
        public ICommand OpenAccountCommand { get; }

        private void OnOpenAccountCommandExecute(object p)
        {
            Service.OpenNewAccount(Worker.SelectedClientInfo.Client.Id);
        }
        #endregion

        #region Закрыть счет
        public ICommand CloseAccountCommand { get; }

        private void OnCloseAccountCommandExecute(object p)
        {
            Service.ClosingAccount(p as Account);
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

        #region Пополнение счета
        public ICommand AccountReplenishCommand { get; }

        private void OnAccountReplenishCommandExecute(object p)
        {
            Service.ChangeBalanceWorkingMode("Fill", SelectedAccount);
        }

        private bool CanAccountReplenishCommandExecute(object p) => p != null;
        #endregion

        #endregion
    }
}
