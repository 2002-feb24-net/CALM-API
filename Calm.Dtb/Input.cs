using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Calm.Dtb
{
    public class Input : IInput
    {
        private readonly CalmContext context;

        public Input(CalmContext cont)
        {
            context = cont;
        }

        public async Task<T> Add<T>(T item) where T : class
        {
            var querry = context.Set<T>();
            EntityEntry<T> output;
            try
            {
                output = await querry.AddAsync(item);
            }
            catch (Exception E)
            {
                throw new Exception("400",
                    new Exception($"New item is invalid: {E.Message}"));
            }
            await context.SaveChangesAsync();
            return output.Entity;
        }

        public async Task Set<T>(T item, int id) where T : class
        {
            T output = await context.Set<T>().FindAsync(id);
            if (output != null)
            {
                try
                {
                    output = item;
                    await context.SaveChangesAsync();
                }
                catch (Exception E)
                {
                    throw new Exception("400",
                        new Exception($"New item is invalid: {E.Message}"));
                }
            }
            else
            {
                throw new Exception("500",
                    new Exception("querried item does not exist"));
            }
        }

        public async Task Remove<T>(T item) where T : class
        {
            context.Set<T>().Remove(item);
            await context.SaveChangesAsync();
        }
    }
}
