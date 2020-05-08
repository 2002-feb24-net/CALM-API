using Calm.Lib.Items;
using System.Threading.Tasks;

namespace Calm.Lib
{
    public interface IPost
    {
        /// <summary>
        /// adds a user to the database
        /// </summary>
        /// <param name="item">user item to add</param>
        /// <returns>user data after adding</returns>
        Task<UserItem> User(UserItem item);
        /// <summary>
        /// adds a user to the database with admin permissions
        /// </summary>
        /// <param name="username">username of existing admin</param>
        /// <param name="value">user to add</param>
        /// <returns>user data after adding</returns>
        Task<UserItem> AdminUser(string username, string password, UserItem value);
        /// <summary>
        /// adds a new gathering
        /// </summary>
        /// <param name="username">username of admin to own the gathering</param>
        /// <param name="gathering">gathering data</param>
        Task AddGathering(string username, string password, GatheringItemIn gathering);
        /// <summary>
        /// enters a user to the applicants of a gathering
        /// </summary>
        /// <param name="username">username of user</param>
        /// <param name="title">title of gathering</param>
        Task Enter(string username, string password, string title);
    }
}