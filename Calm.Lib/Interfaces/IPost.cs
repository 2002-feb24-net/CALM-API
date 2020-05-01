using System.Threading.Tasks;

namespace Calm.Lib
{
    public interface IPost
    {
        Task<UserItem> User(UserItem item);
        Task<object> AdminUser(string username, string password, UserItem value);
    }
}