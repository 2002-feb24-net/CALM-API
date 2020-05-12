using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calm.Dtb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calm.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        [HttpPost]
        public ActionResult SeedData([FromServices] CalmContext context)
        {
            Seeder.Seed(context);

            return Ok();
        }
    }
}