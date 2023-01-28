using M3Practice13.Models;
using M3Practice13.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ClientOperationsVM(Worker worker)
        {
            Worker = worker;
        }
    }
}
