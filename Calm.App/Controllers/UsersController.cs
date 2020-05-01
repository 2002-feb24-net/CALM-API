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
        private IPut put { get; set; }
        private IDelete delete { get; set; }
        public UsersController([FromServices]IGet get, [FromServices]IPost post, [FromServices]IPut put, [FromServices]IDelete delete)
        {
            this.get = get;
            this.post = post;
            this.put = put;
            this.delete = delete;
        }

        [HttpGet("{Username}/{Password}")]
        public async Task<ActionResult<IEnumerable<UserItem>>> Login(string Username, string Password)
        {
            return await TryTask.Run<IEnumerable<UserItem>>(async() => Ok(await get.Login(Username,Password)));
        }

        [HttpPost]
        public async Task<ActionResult<UserItem>> PostUser([FromBody] UserItem value)
        {
            return await TryTask.Run<UserItem>(async () => Ok(await post.User(value)));
        }

        [HttpPut("{Username}/{Password}")]
        public async Task<ActionResult> SetUser(string Username, string Password, [FromBody] UserItem value)
        {
            return await TryTask.Run(async () =>
            {
                await put.SetUser(Username, Password, value);
                return Ok();
            });
        }

        [HttpDelete("{Username}/{Password}")]
        public async Task<ActionResult> Delete(string Username, string Password)
        {
            return await TryTask.Run(async () =>
            {
                await delete.RemoveUser(Username, Password);
                return Ok();
            });
        }
    }
}
