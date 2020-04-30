using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calm.Dtb;
using Calm.Lib;
using CoOp19.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Calm.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IGet get { get; set; }
        private IPost post { get; set; }

        public UsersController([FromServices]IGet get, [FromServices]IPost post)
        {
            this.get = get;
            this.post = post;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserItem>>> Get()
        {
            return await TryTask<IEnumerable<UserItem>>.Run(async() => Ok(await get.Users()));
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserItem>> Post([FromBody] UserItem value)
        {
            return await TryTask<UserItem>.Run(async () => Ok(await post.User(value)));
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
