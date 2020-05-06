using Calm.Lib.Items;
using System.Threading.Tasks;

namespace Calm.Lib
{
    public interface IPut
    {
        /// <summary>
        /// edits a user
        /// </summary>
        /// <param name="username">username of user to edit</param>
        /// <param name="value">new data</param>
        Task SetUser(string username, string password, UserItem value);
        /// <summary>
        /// swaps the admin status of a user, requires superadmin to revoke
        /// </summary>
        /// <param name="username">username of admin</param>
        /// <param name="subjectUser">username of user to effect</param>
        Task SwapUserStatus(string username, string password, string subjectUser);
        /// <summary>
        /// edits the info of a gathering
        /// </summary>
        /// <param name="username">username of organizer</param>
        /// <param name="formerTitle">title of gathering</param>
        /// <param name="gathering">new info</param>
        Task EditGatheringInfo(string username, string password, string formerTitle, GatheringItemIn gathering);
    }
}