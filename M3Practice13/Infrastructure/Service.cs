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
        /// <summary>
        /// Смена режима работы главного окна
        /// </summary>
        public static event Action<UserControl?> MainWindowChange;
        public static void MainWindowChangeRequest(string request = null, Worker worker = null)
        {
            switch (request)
            {
                case "AddClient":
                    MainWindowChange?.Invoke(new AddNewClientView());
                    break;
                case "ClientInfo":
                    MainWindowChange?.Invoke(new ClientOperationsView(worker));
                    break;
                default:
                    MainWindowChange?.Invoke(null);
                    break;
            }
        }

        /// <summary>
        /// Смена вторичного окна работы с клиентом
        /// </summary>
        public static event Action<UserControl?> ClientWorkWindowChange;
        public static void ClientWorkWindowChangeRequest(string request = null, Account account = null)
        {
            switch (request)
            {
                case "Fill":
                    ClientWorkWindowChange?.Invoke(new BalanceWorkingView(account));
                    break;
                case "Transaction":
                    ClientWorkWindowChange?.Invoke(new AccountTransactionView(account));
                    break;
                default:
                    ClientWorkWindowChange?.Invoke(null);
                    break;
            }
        }

        /// <summary>
        /// Смена режима работы
        /// </summary>
        public static event Action<Worker> ChangeRole;
        public static void ChangeRoleRequest(Worker worker) => ChangeRole?.Invoke(worker);

        /// <summary>
        /// Режим работы с системой
        /// </summary>
        public static event Func<string> StatusRequest;
        public static string GetWorkerSatus()
        {
            return StatusRequest?.Invoke();
        }

        /// <summary>
        /// Все счета в БД
        /// </summary>
        public static event Func<ObservableCollection<Account>> AllAccounts;
        public static ObservableCollection<Account> GetAllAccountRequest()
        {
            return AllAccounts?.Invoke();
        }

        /// <summary>
        /// Сохранение информации в БД
        /// </summary>
        public static event Action SaveDataBase;
        public static void SaveDataBaseRequest()
        {
            SaveDataBase?.Invoke();
        }

        /// <summary>
        /// Счет текущего клиента
        /// </summary>
        public static event Action<Account> AccountSelectionChange;
        public static void AccountChanged(Account account)
        {
            AccountSelectionChange?.Invoke(account);
        }

        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        public static event Action<ClientInfo> AddNewClient;
        public static void AddNewClientRequest(Client client)
        {
            client.Id = Data.GetNextID();

            ClientInfo newClientInfo = new ClientInfo();
            newClientInfo.Client = client;
            AddNewClient?.Invoke(newClientInfo);
        }

        /// <summary>
        /// Открыть новый счет
        /// </summary>
        public static event Action<Account, MessageLog> OpenAccount;
        public static void OpenAccountRequest(int clientId)
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

        /// <summary>
        /// Закрыть счет
        /// </summary>
        public static event Action<MessageLog> CloseAccount;
        public static void CloseAccountRequest(Account account)
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

        /// <summary>
        /// Пополнение счета
        /// </summary>
        public static event Action<MessageLog> AccountFill;
        public static void AccountFillRequest(Account account, double ammount)
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

        /// <summary>
        /// Перевод между счетами
        /// </summary>
        public static event Action<MessageLog, MessageLog> TransactionBetweenAccounts;
        public static void TransactionBetweenAccountsRequest(Account accountToWithDraw, Account accountToFill, double ammount)
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
