using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HSA_REST.Models;

namespace HSA_REST.Controllers
{
    public class TransactionController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1T", "value2T" };
        }
        
        // GET api/<controller>/5
        public Double Get(Int64 username)
        {
            TransactionPer up = new TransactionPer();
            Double transaction = up.getTransaction(username);

            return transaction;
        }
        

        // POST: api/user
        public void Post([FromBody]Transaction value)
        {
            TransactionPer pp = new TransactionPer();
            long id;
            id = pp.saveTransaction(value);
        }

        /*
        // PUT api/<controller>/5
        public void Put([FromBody]Balance value)
        {
            BalancePer pp = new BalancePer();
            pp.setBalance(value);
        }
        */

        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}