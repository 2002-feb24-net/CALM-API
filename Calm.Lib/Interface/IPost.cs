using Calm.Dtb;
using System.Threading.Tasks;

namespace Calm.Lib
{
    public interface IPost
    {
        Task<UserItem> User(UserItem item);
    }
}