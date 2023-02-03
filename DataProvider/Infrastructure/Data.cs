using M3Practice13.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice13
{
    public class Data
    {
        private const string CLIENTINFO_PATH = "ClientDB.json";
        private const string CLIENTID_PATH = "ID.json";
        private static int ClientID { get; set; }

        static Data()
        {
            if (!File.Exists(CLIENTINFO_PATH))
            {
                int client_index = 1;

                var test_data = Enumerable.Range(1, 10)
                    .Select(client =>
                     new ClientInfo
                     {
                         Client = new Client
                         {
                             Id = client_index,
                             Name = $"Name {client_index}",
                             Surname = $"Surname {client_index}",
                             Patronymic = $"Patronymic {client_index++}",
                             Passport = $"1111 111111"
                         },
                         ClientAccounts = new ObservableCollection<Account>(),
                         Journal = new ObservableCollection<MessageLog>()

                     });
                ObservableCollection<ClientInfo> clients = new ObservableCollection<ClientInfo>();
                
                foreach (ClientInfo item in test_data)
                {
                    item.ClientAccounts.Add(new Account
                    {
                        CLientID = item.Client.Id,
                        Number = $"{item.Client.Id}{item.Client.Id}{item.Client.Id}",
                        Balance = 200.0,
                        CreationDate = DateTime.Now
                    });
                    item.Journal.Add(new MessageLog
                    {
                        ClientID = item.Client.Id,
                        RecordTime = DateTime.Now,
                        Message = $"Открыт счет {item.Client.Id}{item.Client.Id}{item.Client.Id}",
                        Operator = $"Admin",
                        IsReaded = true
                    });

                    clients.Add(item);
                }

                WriteData(clients);
            }


            if (File.Exists(CLIENTID_PATH))
            {
                ClientID = ReadClientID();
            }
            else
            {
                ClientID = 10;
                SaveClientID(ClientID);
            }
            
        }

        #region Работа с ID клиента (чтение/запись/новое значение)
        /// <summary>
        /// Получение следующего номера ID клиента из Базы данных
        /// </summary>
        /// <returns>значение ID</returns>
        public static int GetNextID()
        {
            ClientID++;
            SaveClientID(ClientID);
            return ClientID;
        }

        private static void SaveClientID(int id)
        {
            File.Delete(CLIENTID_PATH);
            File.WriteAllText(CLIENTID_PATH, JsonConvert.SerializeObject(id));
        }

        private static int ReadClientID()
        {
            string json = File.ReadAllText(CLIENTID_PATH);

            return JsonConvert.DeserializeObject<int>(json);
        }

        #endregion


        #region Работа с записью/чтение БД клиентов

        public static ObservableCollection<ClientInfo> GetData()
        {
            string json = File.ReadAllText(CLIENTINFO_PATH);

            return JsonConvert.DeserializeObject<ObservableCollection<ClientInfo>>(json);
        }

        public static void WriteData(ObservableCollection<ClientInfo> clientDB)
        {
            File.Delete(CLIENTINFO_PATH);
            File.WriteAllText(CLIENTINFO_PATH, JsonConvert.SerializeObject(clientDB));
        }

        #endregion
    }
}
