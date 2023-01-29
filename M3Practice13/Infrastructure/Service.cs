using M3Practice13.Models;
using M3Practice13.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace M3Practice13.Controler
{
    public static class Service
    {
        public static event Func<string> StatusRequest;
        public static event Func<ObservableCollection<Account>> AllAccounts;

        public static event Action<Account> AccountSelectionChange;
        public static event Action<Worker> ChangeRole;
        public static event Action<UserControl?> ChangeWorkingMode;
        public static event Action<ClientInfo> NewClientToAdd;
        public static event Action<Account, MessageLog> OpenAccount;
        public static event Action<MessageLog> CloseAccount;
        public static event Action<UserControl?> ChangeBalanceWork;
        public static event Action<MessageLog> AccountFill;
        public static event Action<MessageLog, MessageLog> TransactionBetweenAccounts;
       

        public static string GetWorkerSatus()
        {
            return StatusRequest?.Invoke();
        }

        public static ObservableCollection<Account> GetAllAccount()
        {
            return AllAccounts?.Invoke();
        }

        public static void NewUserRole(Worker worker) => ChangeRole?.Invoke(worker);

        public static void ChangeWorkMode(string request=null, Worker worker=null)
        {
            switch (request)
            {
                case "AddClient":
                    ChangeWorkingMode?.Invoke(new AddNewClientView());
                    break;
                case "ClientInfo":
                    ChangeWorkingMode?.Invoke(new ClientOperationsView(worker));
                    break;
                default:
                    ChangeWorkingMode?.Invoke(null);
                    break;
            }
        }

        public static void ChangeBalanceWorkingMode(string request=null, Account account=null)
        {
            switch (request)
            {
                case "Fill":
                    ChangeBalanceWork?.Invoke(new BalanceWorkingView(account));
                    break;
                case "Transaction":
                    ChangeBalanceWork?.Invoke(new AccountTransactionView(account));
                    break;
                default:
                    ChangeBalanceWork?.Invoke(null);
                    break;
            }
        }

        public static void AddNewClient(Client client)
        {
            client.Id = Data.GetNextID();
            
            ClientInfo newClientInfo = new ClientInfo();
            newClientInfo.Client = client;
            NewClientToAdd?.Invoke(newClientInfo);
        }

        public static void OpenNewAccount(int clientId)
        {
            Random r = new Random();
            Account newAccount = new Account
               {
                   CLientID = clientId,
                   Balance = 0.00,
                   CreationDate = DateTime.Now,
                   Number = (r.Next(100, 1000)).ToString(),
               };

            MessageLog messageLog = new MessageLog
            {
                ClientID = clientId,
                RecordTime = DateTime.Now,
                IsReaded = false,
                Operator = GetWorkerSatus(),
                Message = $"Открыт счет {newAccount.Number}"
            };

            OpenAccount?.Invoke(newAccount, messageLog);
        }

        public static void ClosingAccount(Account account)
        {
            account.ClosingTime = DateTime.Now;
            MessageLog messageLog = new MessageLog
            {
                ClientID = account.CLientID,
                RecordTime = DateTime.Now,
                IsReaded = false,
                Operator = GetWorkerSatus(),
                Message = $"Произведено закрытие счета {account.Number}"
            };

            CloseAccount?.Invoke(messageLog);
        }

        public static void AccountFilling(Account account, double ammount)
        {
            account.Balance += ammount;

            MessageLog messageLog = new MessageLog
            {
                ClientID = account.CLientID,
                RecordTime = DateTime.Now,
                IsReaded = false,
                Operator = GetWorkerSatus(),
                Message = $"Пополнение счета {account.Number} на сумму {ammount}"
            };

            AccountFill?.Invoke(messageLog);
        }

        public static void AccountChanged(Account account)
        {
            AccountSelectionChange?.Invoke(account);
        }

        public static void AccountTransaction(Account accountToWithDraw, Account accountToFill, double ammount)
        {
            accountToWithDraw.Balance -= ammount;
            MessageLog messageWithdraw = new MessageLog
            {
                ClientID = accountToWithDraw.CLientID,
                RecordTime = DateTime.Now,
                IsReaded = false,
                Operator = GetWorkerSatus(),
                Message = $"Списание со счета {accountToWithDraw.Number} на счет {accountToFill.Number} суммы {ammount}"
            };

            accountToFill.Balance += ammount;
            MessageLog messageToFill = new MessageLog
            {
                ClientID = accountToFill.CLientID,
                RecordTime = DateTime.Now,
                IsReaded = false,
                Operator = GetWorkerSatus(),
                Message = $"Поступление на счет {accountToFill.Number} со счета {accountToWithDraw.Number} суммы {ammount}"
            };

            TransactionBetweenAccounts?.Invoke(messageWithdraw, messageToFill);
        }

    }
}
