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
    public class UserController : ApiController
    {
        // GET: api/Person
        public IEnumerable<string> Get()
        {
            return new string[] { "Person1", "Person2" };
        }

        /*
        public ArrayList Get()
        {

        }
        */

        // GET: api/Person/5
        public User Get(string id)
        {

            UserPer up = new UserPer();
            User user = up.getUser(id);

            return user;
        }

        // POST: api/Person
        public void Post([FromBody]User value)
        {
            UserPer pp = new UserPer();
            long id;
            id = pp.saveUser(value);
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
