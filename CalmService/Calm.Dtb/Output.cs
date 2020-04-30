using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calm.Dtb
{
    public class Output : IOutput
    {
        private readonly CalmContext context;

        public Output(CalmContext cont)
        {
            context = cont;
        }

        public async Task<IEnumerable<T>> Get<T>() where T : class
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> Get<T>(Func<T, bool> Aplies) where T : class
        {
            return new List<T>(
                from item in await context.Set<T>().ToListAsync()
                where Aplies(item)
                select item);
        }

        public async Task<T> Get<T>(int id) where T : class
        {
            var output = await context.Set<T>().FindAsync(id);
            if (output == null)
            {
                throw new Exception("Output",
                    new Exception($"Requested item at Id:{id} In:{context.Set<T>().GetType()} does not exist"));
            }
            return await context.Set<T>().FindAsync(id);
        }
    }
}
