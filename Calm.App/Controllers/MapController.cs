using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calm.Lib;
using Calm.Lib.Items;
using CoOp19.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calm.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private IGet Get { get; set; }
        private IPost Post { get; set; }
        private IPut Put { get; set; }
        private IDelete Delete { get; set; }
        public MapController([FromServices]IGet get, [FromServices]IPost post, [FromServices]IPut put, [FromServices]IDelete delete)
        {
            Get = get;
            Post = post;
            Put = put;
            Delete = delete;
        }

        /// <summary>
        /// returns a list of all existing citys
        /// </summary>
        /// <returns>list of map</returns>
        /// <response code="200">request was successfull</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetMaps()
        {
            return await TryTask.Run<IEnumerable<string>>(async () => Ok(await Get.CityList()));
        }

        /// <summary>
        /// returns a list of all Users within a city
        /// </summary>
        /// <returns>list of users</returns>
        /// <response code="200">request was successfull</response>
        /// <response code="404">a parameter in the request header could not be found</response>
        [HttpGet("user/{city}")]
        public async Task<ActionResult<IEnumerable<GatheringItemOut>>> GetMapUsers(string city)
        {
            return await TryTask.Run<IEnumerable<GatheringItemOut>>(async () => Ok(await Get.CityListUsers(city)));
        }

        /// <summary>
        /// returns a list of all gatherings within a city
        /// </summary>
        /// <returns>list of gatherings</returns>
        /// <response code="200">request was successfull</response>
        /// <response code="404">a parameter in the request header could not be found</response>
        [HttpGet("gathering/{city}")]
        public async Task<ActionResult<IEnumerable<UserItem>>> GetMapGatherings(string city)
        {
            return await TryTask.Run<IEnumerable<UserItem>>(async () => Ok(await Get.CityListGatherings(city)));
        }
    }
}
