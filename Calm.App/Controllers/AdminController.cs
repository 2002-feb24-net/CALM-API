using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calm.Lib;
using CoOp19.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calm.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IGet Get { get; set; }
        private IPost Post { get; set; }
        private IPut Put { get; set; }
        private IDelete Delete { get; set; }
        public AdminController([FromServices]IGet get, [FromServices]IPost post, [FromServices]IPut put, [FromServices]IDelete delete)
        {
            this.Get = get;
            this.Post = post;
            this.Put = put;
            this.Delete = delete;
        }

        [HttpPost("{Username}/{Password}")]
        public async Task<ActionResult<UserItem>> PostUser(string Username, string Password, [FromBody] UserItem value)
        {
            return await TryTask.Run<UserItem>(async () => Ok(await Post.AdminUser(Username, Password, value)));
        }

        [HttpPut("{Username}/{Password}/{subjectUser}")]
        public async Task<ActionResult<UserItem>> SwapUserStatus(string Username, string Password, string subjectUser)
        {
            return await TryTask.Run(async () =>
            {
                await Put.SwapUserStatus(Username, Password, subjectUser);
                return Ok();
            });
        }
    }
}