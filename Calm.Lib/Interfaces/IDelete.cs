using System.Threading.Tasks;

namespace Calm.Lib
{
    public interface IDelete
    {
        /// <summary>
        /// removes a user
        /// </summary>
        /// <param name="username">username of user</param>
        Task RemoveUser(string username, string password);
        /// <summary>
        /// removes a gathering
        /// </summary>
        /// <param name="username">username of user running request</param>
        /// <param name="title">title of gathering</param>
        Task RemoveGathering(string username, string password, string title);
        /// <summary>
        /// removes a user of lower rank than the one logged in
        /// </summary>
        /// <param name="username">username of logged in user</param>
        /// <param name="subjectUser">username of user to remove</param>
        Task RemoveUser(string username, string password, string subjectUser);
    }
}