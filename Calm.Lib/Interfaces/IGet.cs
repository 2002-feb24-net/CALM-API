using Calm.Dtb;
using Calm.Lib.Items;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calm.Lib
{
    public interface IGet
    {
        /// <summary>
        /// retieves the user data given login credentials
        /// </summary>
        /// <param name="username">username to login</param>
        /// <returns>all user data</returns>
        Task<UserItem> Login(string username, string password);
        /// <summary>
        /// lists all releasable user data
        /// </summary>
        /// <returns>list of users</returns>
        Task<IEnumerable<UserItem>> UserList();
        /// <summary>
        /// lists all gatherings
        /// </summary>
        /// <returns>list of gatherings</returns>
        Task<IEnumerable<GatheringItemOut>> ListGatherings();
    }
}