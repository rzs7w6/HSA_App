using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HSA_REST.Models;
using System.Collections;

namespace HSA_REST.Controllers
{
    public class ReceiptController : ApiController
    {
        // GET: api/user
        public IEnumerable<string> Get()
        {
            return new string[] { "Person1", "Person2" };
        }

        //public bool isUserDuplicate(string uname)
        //{

        //}

        /*
        public ArrayList Get()
        {

        }
        */

        // GET: api/user/"username"
        public Receipt Get(string recName)
        {
            ReceiptPer up = new ReceiptPer();
            Receipt receipt = up.getReceipt(recName);

            return receipt;
        }

        // POST: api/Receipt
        public void Post([FromBody]Receipt value)
        {
            ReceiptPer pp = new ReceiptPer();
            long id;
            id = pp.saveReceipt(value);
        }


        // PUT: api/Person/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Person/5
        public void Delete(int id)
        {
        }


    }
}
