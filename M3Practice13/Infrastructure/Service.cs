using M3Practice13.Models;
using M3Practice13.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace M3Practice13.Controler
{
    public static class Service
    {
        /// <summary>
        /// Возвращает значение новой рабочей роли сотрудника
        /// </summary>
        public static event Action<Worker> ChangeRole;
        public static event Action<UserControl?> ChangeWorkingMode;
        public static event Action<ClientInfo> NewClientToAdd;
       

        public static void NewUserRole(Worker worker) => ChangeRole?.Invoke(worker);

        public static void ChangeWorkMode(string request=null)
        {
            switch (request)
            {
                case "AddClient":
                    ChangeWorkingMode?.Invoke(new AddNewClientView());
                    break;
                default:
                    ChangeWorkingMode?.Invoke(null);
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
    }
}
