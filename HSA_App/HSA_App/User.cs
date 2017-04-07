
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSA_App
{
    public class User
    {
        public long ID { get; set; }
        public Int64 AccountNumber { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Birthday { get; set; }
        public String HashedPassword { get; set; }
        public String UserName { get; set; }
    }
    /*
    public string Summary
    {
        get { return String.Format("Date: {0}, Magnitude: {1}", datetime.Substring(0, 10), magnitude); }
    }

    public override string ToString()
    {
        return String.Format("{0}, {1}, {2}, {3}", lat, lng, magnitude, depth);
    }*/


    public class Rootobject
    {
        public User users { get; set; }
    }
}
