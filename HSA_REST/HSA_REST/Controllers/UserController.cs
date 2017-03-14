using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HSA_REST.Models;


namespace HSA_REST.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/Person
        public IEnumerable<string> Get()
        {
            return new string[] { "Person1", "Person2" };
        }

        // GET: api/Person/5
        public User Get(int id)
        {
            User person = new Models.User();
            person.ID = id;
            person.LastName = "Smith";
            person.FirstName = "Same";
            person.HashedPassword = "sdfwer234234defsdfsre33r";
            person.Birthday = DateTime.Parse("5/5/1880");
            person.UserName = "big man";
            return person;
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
