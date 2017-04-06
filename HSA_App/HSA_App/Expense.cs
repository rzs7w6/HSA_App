using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;

using static System.DateTime;
using static System.String;

namespace HSA_App
{
    public class Receipt
    {
        public double Total { get; set; } = 15.0;
        public DateTime TimeStamp { get; set; } = UtcNow;
        public string Photo { get; set; } = Empty;
    }
}
