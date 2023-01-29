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
    internal class BalanceWorkingVM : BaseViewModel
    {
        #region Аккаунт клиента с которым работаем
        private Account currentAccount;
        public Account CurrentAccount
        {
            get => currentAccount;
            set => Set(ref currentAccount, value);
        }
        #endregion

        #region Сумма для пополнения
        private string ammountToFill;
        public string AmmountToFill
        {
            get => ammountToFill;
            set => Set(ref ammountToFill, value);
        }
        #endregion

        public BalanceWorkingVM(Account account)
        {
            CurrentAccount = account;
            CancelCommand = new Command(OnCancelCommandExecute);
            AccountFillComand = new Command(OnAccountFillComandExecute,
                                            CanAccountFillComandExecute);

            Service.AccountSelectionChange += ChangeSelectedAccount;
        }

        public void ChangeSelectedAccount(Account account)
        {
            CurrentAccount = account;
        }

        #region Команды

        #region Пополнение выбранного счета
        public ICommand AccountFillComand { get; }

        private void OnAccountFillComandExecute(object p)
        {
            Service.AccountFilling(CurrentAccount, double.Parse((string)p));
        }
        private bool CanAccountFillComandExecute(object p)
            => double.TryParse((string)p, out double test) && test >= 0.0;


        #endregion

        #region Отмена перевода
        public ICommand CancelCommand { get; }

        private void OnCancelCommandExecute(object p)
        {
            Service.ChangeBalanceWorkingMode(null);
        }
        #endregion

        #endregion
    }
}
