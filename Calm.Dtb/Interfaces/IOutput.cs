using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Calm.Dtb
{
    public interface IOutput
    {
        Task<IEnumerable<T>> Get<T>() where T : class;
        Task<T> Get<T>(int id) where T : class;
        Task<List<T>> GetFilter<T>(Func<T, bool> Aplies) where T : class;
        Task<T> GetFind<T>(Expression<Func<T, bool>> Aplies) where T : class;
    }
}