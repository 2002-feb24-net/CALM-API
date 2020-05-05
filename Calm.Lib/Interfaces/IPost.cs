using Calm.Lib.Items;
using System.Threading.Tasks;

namespace Calm.Lib
{
    public interface IPost
    {
        Task<UserItem> User(UserItem item);
        Task<object> AdminUser(string username, string password, UserItem value);
        Task AddGathering(string username, string password, GatheringItemIn gathering);
        Task Enter(string username, string password, string title);
    }
}