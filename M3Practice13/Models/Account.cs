using M3Practice13.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice13.Models
{
    public class Account : BaseViewModel
    {
        public int CLientID { get; set; }
        public string Number { get; set; }

        private double balance;
        public double Balance 
        { 
            get => balance;
            set => Set(ref balance, value); 
        }
        public DateTime CreationDate { get; set; }

        private DateTime? closingTime;
        public DateTime? ClosingTime 
        {
            get => closingTime;
            set => Set(ref closingTime, value);
        }

    }
}
