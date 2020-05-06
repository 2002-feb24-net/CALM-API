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

        /// <summary>
        /// removes a gathering item
        /// </summary>
        /// <param name="username">username of the organiser</param>
        /// <param name="password"></param>
        /// <param name="title">title of gathering to remove</param>
        /// <response code="204">request was successfull</response>
        /// <response code="401">Invalid login</response>
        /// <response code="403">Invalid credentials</response>
        /// <response code="404">a parameter in the request header could not be found</response>
        [HttpDelete("{username}/{password}/{title}")]
        public async Task<ActionResult> DeleteGathering(string username, string password, string title)
        {
            return await TryTask.Run(async () =>
            {
                await Delete.RemoveGathering(username, password, title);
                return NoContent();
            });
        }

        /// <summary>
        /// lists all gatherings with the ones who signed up
        /// </summary>
        /// <returns>list of gatherings</returns>
        /// <response code="200">request was successfull</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GatheringItemOut>>> ListGatherings()
        {
            return await TryTask.Run<IEnumerable<GatheringItemOut>>(async () => Ok(await Get.ListGatherings()));
        }

        /// <summary>
        /// adds a new gathering
        /// </summary>
        /// <param name="username">username of an existing admin to own the gathering</param>
        /// <param name="password"></param>
        /// <param name="input">info of the gathering</param>
        /// <response code="204">request was successfull</response>
        /// <response code="400">Invalid input in json input</response>
        /// <response code="401">Invalid login</response>
        /// <response code="403">Invalid credentials</response>
        /// <response code="409">A value in the body conflicts with an existing item</response>
        [HttpPost("{username}/{password}")]
        public async Task<ActionResult> AddGathering(string username, string password, [FromBody] GatheringItemIn input)
        {
            return await TryTask.Run(async () =>
            {
                await Post.AddGathering(username, password, input);
                return NoContent();
            });
        }

        /// <summary>
        /// adds a user to the gathering
        /// </summary>
        /// <param name="username">username of individual to add</param>
        /// <param name="password"></param>
        /// <param name="title">title of gathering</param>
        /// <response code="204">request was successfull</response>
        /// <response code="401">Invalid login</response>
        /// <response code="404">a parameter in the request header could not be found</response>
        [HttpPost("{username}/{password}/{title}")]
        public async Task<ActionResult> Enroll(string username, string password, string title)
        {
            return await TryTask.Run(async () =>
            {
                await Post.Enter(username, password, title);
                return NoContent();
            });
        }
    }
}