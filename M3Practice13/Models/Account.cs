using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice13.Models
{
    public class Account 
    {
        public int CLientID { get; set; }
        public string Number { get; set; }
        public double Balance { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ClosingTime { get; set; }

    }
}
