﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HSA_REST.Models;

namespace HSA_REST.Controllers
{
    public class BalanceController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public Balance Get(Int64 username)
        {
            BalancePer up = new BalancePer();
            Balance balance = up.getBalance(username);

            return balance;
        }


        // POST: api/user
        public void Post([FromBody]Balance value)
        {
            BalancePer pp = new BalancePer();
            long id;
            id = pp.saveBalance(value);
        }

        // PUT api/<controller>/5
        public void Put([FromBody]Balance value)
        {
            BalancePer pp = new BalancePer();
            pp.setBalance(value);
        }


        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}