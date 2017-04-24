using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSA_REST.Models
{
    public class Transaction
    {
        public Int64 AccountNumber { get; set; }

        public String Type { get; set; }
        public Double Amount { get; set; }
        public String Date { get; set; }
    }
}
