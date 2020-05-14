using System.Threading.Tasks;

namespace Calm.Dtb
{
    public interface IInput
    {
        /// <summary>
        /// adds an item to the database
        /// </summary>
        /// <typeparam name="T">type of data</typeparam>
        /// <param name="item">input item</param>
        /// <returns>added item with updated ids</returns>
        Task<T> Add<T>(T item) where T : class;
        /// <summary>
        /// sets an existing item to have different data
        /// </summary>
        /// <typeparam name="T">item type</typeparam>
        /// <param name="item">item to effect</param>
        /// <param name="id">primary key of item</param>
        /// <returns>item after it is effected</returns>
        Task Set<T>(T item, int id) where T : class;
        /// <summary>
        /// removes an item from the database
        /// </summary>
        /// <typeparam name="T">item type</typeparam>
        /// <param name="item">item to remove</param>
        Task Remove<T>(T item) where T : class;
    }
}