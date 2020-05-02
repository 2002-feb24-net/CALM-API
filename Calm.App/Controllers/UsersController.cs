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
        private IGet Get { get; set; }
        private IPost Post { get; set; }
        private IPut Put { get; set; }
        private IDelete Delete { get; set; }
        public UsersController([FromServices]IGet get, [FromServices]IPost post, [FromServices]IPut put, [FromServices]IDelete delete)
        {
            Get = get;
            Post = post;
            Put = put;
            Delete = delete;
        }

        [HttpGet("{Username}/{Password}")]
        public async Task<ActionResult<IEnumerable<UserItem>>> Login(string Username, string Password)
        {
            return await TryTask.Run<IEnumerable<UserItem>>(async() => Ok(await Get.Login(Username,Password)));
        }

        [HttpPost]
        public async Task<ActionResult<UserItem>> PostUser([FromBody] UserItem value)
        {
            return await TryTask.Run<UserItem>(async () => Ok(await Post.User(value)));
        }

        [HttpPut("{Username}/{Password}")]
        public async Task<ActionResult> SetUser(string Username, string Password, [FromBody] UserItem value)
        {
            return await TryTask.Run(async () =>
            {
                await Put.SetUser(Username, Password, value);
                return Ok();
            });
        }

        [HttpDelete("{Username}/{Password}")]
        public async Task<ActionResult> Remove(string Username, string Password)
        {
            return await TryTask.Run((Func<Task<ActionResult>>)(async () =>
            {
                await this.Delete.RemoveUser(Username, Password);
                return base.Ok();
            }));
        }
    }
}
