using M3Practice13.Controler;
using M3Practice13.Models;
using M3Practice13.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace M3Practice13.ViewModels
{
    internal class AccountTransactionVM : BaseViewModel
    {
        #region Счет с которым работаем
        private Account currentAccount;

        public Account CurrentAccount
        {
            get => currentAccount;
            set => Set(ref currentAccount, value);
        }
        #endregion

        #region Сумма для списания
        private string ammountTransaction;

        public string AmmountTransaction
        {
            get => ammountTransaction;
            set => Set(ref ammountTransaction, value);
        }
        #endregion

        #region Счета других клиентов
        private ObservableCollection<Account> allAccounts;

        public ObservableCollection<Account> AllAccounts
        {
            get => allAccounts;
            set => Set(ref allAccounts, value);
        }

        #endregion

        #region Счет для перевода
        private Account accountTransaction;

        public Account AccountTransaction 
        {
            get => accountTransaction;
            set => Set(ref accountTransaction, value);
        }
        #endregion

        public AccountTransactionVM(Account account)
        {
            CurrentAccount = account;

            Service.AccountSelectionChange += AccountChanged;

            AllAccounts = AccountFiltering();

            CancelCommand = new Command(OnCancelCommandExecute);
            TransactionCommand = new Command(OnTransactionCommandExecute,
                                             CanTransactionCommandExecute);
        }

        private void AccountChanged(Account account) => CurrentAccount = account;

        private ObservableCollection<Account> AccountFiltering()
        {
            ObservableCollection<Account> allaccount = new ObservableCollection<Account>();

            foreach (var item in Service.GetAllAccount())
            {
                if (item.ClosingTime == null && item.CLientID != CurrentAccount.CLientID)
                {
                    allaccount.Add(item);
                }
            }
            return allaccount;
        }

        #region Команды

        #region Перевод
        public ICommand TransactionCommand { get; }

        private void OnTransactionCommandExecute(object p)
        {
            Service.AccountTransaction(CurrentAccount, AccountTransaction, double.Parse(AmmountTransaction));
        }
        private bool CanTransactionCommandExecute(object p)
        {
            if (CurrentAccount != null && CurrentAccount.ClosingTime == null
                && double.TryParse(AmmountTransaction, out double result)
                && CurrentAccount.Balance >= result)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region Отмена переводов
        public ICommand CancelCommand { get; }

        private void OnCancelCommandExecute(object p)
            => Service.ChangeBalanceWorkingMode(null);
        #endregion

        #endregion
    }
}
