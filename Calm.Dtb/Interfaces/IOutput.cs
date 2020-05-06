using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Calm.Dtb
{
    public interface IOutput
    {
        /// <summary>
        /// retrieves the entire table of items as a list
        /// </summary>
        /// <typeparam name="T">itemtype of table</typeparam>
        /// <returns>list of table items</returns>
        Task<IEnumerable<T>> Get<T>() where T : class;
        /// <summary>
        /// retrieves an item with a specified primary key
        /// </summary>
        /// <typeparam name="T">itemtype of table</typeparam>
        /// <param name="id">primary key</param>
        /// <returns>a single table item</returns>
        Task<T> Get<T>(int id) where T : class;
        /// <summary>
        /// retrieves a list of table items within a certain filter
        /// </summary>
        /// <typeparam name="T">itemtype of table</typeparam>
        /// <param name="Aplies">filter to apply</param>
        /// <returns>list of table items</returns>
        Task<List<T>> GetFilter<T>(Func<T, bool> Aplies) where T : class;
        /// <summary>
        /// finds the first item on a table that fits a filter
        /// </summary>
        /// <typeparam name="T">itemtype of table</typeparam>
        /// <param name="Aplies">filter to apply</param>
        /// <returns>a single table item</returns>
        Task<T> GetFind<T>(Expression<Func<T, bool>> Aplies) where T : class;
    }
}