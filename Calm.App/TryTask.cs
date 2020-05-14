using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CoOp19.App
{
    public static class TryTask
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
                return Handle(E);
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
                return Handle(E);
            }

            return output;
        }

        public static ObjectResult Handle(Exception e)
        {
            try
            {
                int code = int.Parse(e.Message);
                return new ObjectResult(new { message = e.InnerException.Message }) { StatusCode = code };
            }
            catch (Exception)
            {
                return new ObjectResult(new { message = "Unhandled Error In Server", error = e }) { StatusCode = 500 };
            }
        }
    }
}
