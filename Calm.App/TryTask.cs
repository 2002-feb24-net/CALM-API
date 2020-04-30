using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CoOp19.App
{
    public class TryTask<T> where T : class
    {
        public static async Task<ActionResult<T>> Run(Func<Task<ActionResult<T>>> Task)
        {
            ActionResult<T> output;

            try
            {
                output = await Task();
            }
            catch (Exception E)
            {
                return new ObjectResult(new { message = "Unhandled Error In Server", error = E }) { StatusCode = 500 };
            }

            return output;
        }
    }
}
