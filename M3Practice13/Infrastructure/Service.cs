using M3Practice13.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice13.Controler
{
    public static class Service
    {
        public static event Action<Worker> ChangeRole;

        public static void NewUserRole(Worker worker) => ChangeRole?.Invoke(worker);
    }
}
