using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HSA_App
{
	public class Transaction
	{
		public Int64 AccountNumber { get; set; }
		public String Type { get; set; }
		public double Amount { get; set; }
		public String Date { get; set; }
	}

	public class RootobjectTrans
	{
		public Transaction transaction { get; set; } 
	}
	
}