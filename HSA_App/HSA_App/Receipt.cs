
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HSA_App
{
    public class ReceiptRest
    {
        public long ID { get; set; }
        public Int64 AccountNumber { get; set; }
        public float Total { get; set; }
        public string Date { get; set; }
        public byte[] Image { get; set; }


    }

    public class RootobjectRest
    {
        public User users { get; set; }
    }

}