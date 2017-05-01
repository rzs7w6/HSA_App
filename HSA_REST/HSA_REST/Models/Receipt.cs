using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSA_REST.Models
{
    public class Receipt
    {
        public long ID { get; set; }
        public Int64 AccountNumber { get; set; }
        public float Total { get; set; }
        public string Date { get; set; }
        public byte[] Image { get; set; }
        

    }
}