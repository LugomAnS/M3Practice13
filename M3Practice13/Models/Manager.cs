using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice13.Models
{
    public class Manager : Worker
    {
        public override string ClientPassport
        {
            get => SelectedClientInfo.Client.Passport;
        }
    }
}
