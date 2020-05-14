using System.Threading.Tasks;
using Calm.Lib;
using CoOp19.App;
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

        /// <summary>
        /// Adds a user, allows you to make them an admin
        /// </summary>
        /// <param name="Username">username of an existing admin</param>
        /// <param name="Password"></param>
        /// <param name="value">user to add</param>
        /// <returns>the user added</returns>
        /// <response code="200">request was successfull</response>
        /// <response code="400">Invalid input in json input</response>
        /// <response code="401">Invalid login</response>
        /// <response code="403">Invalid credentials</response>
        /// <response code="409">A value in the body conflicts with an existing item</response>
        [HttpPost("{Username}/{Password}")]
        public async Task<ActionResult<UserItem>> PostUser(string Username, string Password, [FromBody] UserItem value)
        {
            return await TryTask.Run<UserItem>(async () => Ok(await Post.AdminUser(Username, Password, value)));
        }

        /// <summary>
        /// swaps the status of the user, only the superadmin can revoke
        /// </summary>
        /// <param name="Username">username of an existing user</param>
        /// <param name="Password"></param>
        /// <param name="subjectUser">username to effect</param>
        /// <returns>effected user</returns>
        /// <response code="204">request was successfull</response>
        /// <response code="401">Invalid login</response>
        /// <response code="403">Invalid credentials</response>
        /// <response code="404">a parameter in the request header could not be found</response>
        [HttpPut("{Username}/{Password}/{subjectUser}")]
        public async Task<ActionResult<UserItem>> SwapUserStatus(string Username, string Password, string subjectUser)
        {
            return await TryTask.Run(async () =>
            {
                await Put.SwapUserStatus(Username, Password, subjectUser);
                return NoContent();
            });
        }

        /// <summary>
        /// removes a user from the database without their password, must use login credentials of a higher ranking user
        /// </summary>
        /// <param name="Username">username of an existing user</param>
        /// <param name="Password"></param>
        /// <param name="subjectUser">username to effect</param>
        /// <returns>effected user</returns>
        /// <response code="204">request was successfull</response>
        /// <response code="401">Invalid login</response>
        /// <response code="403">Invalid credentials</response>
        /// <response code="404">a parameter in the request header could not be found</response>
        [HttpDelete("{Username}/{Password}/{subjectUser}")]
        public async Task<ActionResult<UserItem>> RemoveUser(string Username, string Password, string subjectUser)
        {
            return await TryTask.Run(async () =>
            {
                await Delete.RemoveUser(Username, Password, subjectUser);
                return NoContent();
            });
        }
    }
}