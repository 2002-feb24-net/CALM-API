using System.Collections.Generic;
using System.Threading.Tasks;
using Calm.Lib;
using CoOp19.App;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// returns a list of all existing users
        /// </summary>
        /// <returns>list of users</returns>
        /// <response code="200">request was successfull</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserItem>>> UserList()
        {
            return await TryTask.Run<IEnumerable<UserItem>>(async () => Ok(await Get.UserList()));
        }

        /// <summary>
        /// returns all user data associated with the user, intended to be used to assure input login details are correct
        /// </summary>
        /// <param name="Username">username of user</param>
        /// <param name="Password"></param>
        /// <returns>all user data</returns>
        /// <response code="200">request was successfull</response>
        /// <response code="401">Invalid login</response>
        /// <response code="403">Invalid credentials</response>
        [HttpGet("{Username}/{Password}")]
        public async Task<ActionResult<UserItem>> Login(string Username, string Password)
        {
            return await TryTask.Run<UserItem>(async() => Ok(await Get.Login(Username,Password)));
        }

        /// <summary>
        /// adds a user to the database
        /// </summary>
        /// <param name="value">user info to add</param>
        /// <returns>all userdata after adding user</returns>
        /// <response code="200">request was successfull</response>
        /// <response code="400">Invalid input in json input</response>
        /// <response code="409">A value in the body conflicts with an existing item</response>
        [HttpPost]
        public async Task<ActionResult<UserItem>> PostUser([FromBody] UserItem value)
        {
            return await TryTask.Run<UserItem>(async () => Ok(await Post.User(value)));
        }

        /// <summary>
        /// changes a users information
        /// </summary>
        /// <param name="Username">username of user to change</param>
        /// <param name="Password"></param>
        /// <param name="value">new data to set</param>
        /// <response code="204">request was successfull</response>
        /// <response code="400">Invalid input in json input</response>
        /// <response code="401">Invalid login</response>
        /// <response code="403">Invalid credentials</response>
        /// <response code="409">A value in the body conflicts with an existing item</response>
        [HttpPut("{Username}/{Password}")]
        public async Task<ActionResult> SetUser(string Username, string Password, [FromBody] UserItem value)
        {
            return await TryTask.Run(async () =>
            {
                await Put.SetUser(Username, Password, value);
                return NoContent();
            });
        }

        /// <summary>
        /// remove a user
        /// </summary>
        /// <param name="Username">username of user to remove</param>
        /// <param name="Password"></param>
        /// <response code="204">request was successfull</response>
        /// <response code="401">Invalid login</response>
        /// <response code="403">Invalid credentials</response>
        [HttpDelete("{Username}/{Password}")]
        public async Task<ActionResult> Remove(string Username, string Password)
        {
            return await TryTask.Run(async () =>
            {
                await Delete.RemoveUser(Username, Password);
                return NoContent();
            });
        }
    }
}
