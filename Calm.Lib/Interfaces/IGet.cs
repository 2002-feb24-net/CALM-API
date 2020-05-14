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
        /// <summary>
        /// querrys the database for all gatherings divideing by event boolean
        /// </summary>
        /// <param name="v">true for events false for support groups</param>
        /// <returns>list of gatherings</returns>
        Task<object> ListGatherings(bool v);
        /// <summary>
        /// list all citys
        /// </summary>
        /// <returns>list of citys</returns>
        Task<IEnumerable<string>> CityList();
        /// <summary>
        /// querys all users for their city
        /// </summary>
        /// <param name="city">name of city</param>
        /// <returns>list of users</returns>
        Task<IEnumerable<UserItem>> CityListUsers(string city);
        /// <summary>
        /// querys all gatherings for their city
        /// </summary>
        /// <param name="city">name of city</param>
        /// <returns>list of gatherings</returns>
        Task<IEnumerable<GatheringItemOut>> CityListGatherings(string city);
    }
}