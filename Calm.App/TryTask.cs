using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CoOp19.App
{
    public class TryTask
    {
        public static async Task<ActionResult<T>> Run<T>(Func<Task<ActionResult<T>>> Task) where T : class
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

        public static async Task<ActionResult> Run(Func<Task<ActionResult>> Task)
        {
            ActionResult output;

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
