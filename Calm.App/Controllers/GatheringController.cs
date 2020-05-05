using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calm.Dtb;
using Calm.Lib;
using Calm.Lib.Items;
using CoOp19.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calm.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatheringController : ControllerBase
    {
        private IGet Get { get; set; }
        private IPost Post { get; set; }
        private IPut Put { get; set; }
        private IDelete Delete { get; set; }
        public GatheringController([FromServices]IGet get, [FromServices]IPost post, [FromServices]IPut put, [FromServices]IDelete delete)
        {
            Get = get;
            Post = post;
            Put = put;
            Delete = delete;
        }

        [HttpDelete("{username}/{password}/{title}")]
        public async Task<ActionResult> DeleteGathering(string username, string password, string title)
        {
            return await TryTask.Run(async () =>
            {
                await Delete.RemoveGathering(username, password, title);
                return Ok();
            });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GatheringItemOut>>> ListGatherings()
        {
            return await TryTask.Run<IEnumerable<GatheringItemOut>>(async () => Ok(await Get.ListGatherings()));
        }

        [HttpPost("{username}/{password}")]
        public async Task<ActionResult> AddGathering(string username, string password, [FromBody] GatheringItemIn input)
        {
            return await TryTask.Run(async () =>
            {
                await Post.AddGathering(username, password, input);
                return Ok();
            });
        }

        [HttpGet("{username}/{password}/{title}")]
        public async Task<ActionResult> Enroll(string username, string password, string title)
        {
            return await TryTask.Run(async () =>
            {
                await Post.Enter(username, password, title);
                return Ok();
            });
        }
    }
}