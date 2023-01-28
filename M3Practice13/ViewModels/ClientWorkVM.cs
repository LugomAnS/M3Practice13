using M3Practice13.Models;
using M3Practice13.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice13.ViewModels
{
    class ClientWorkVM : BaseViewModel
    {
        public Worker RoleType { get; }

        

        public ClientWorkVM(Worker roleType)
        {
            RoleType = roleType;
            RoleType.ClientsInfo = Data.GetData();
        }
    }
}
