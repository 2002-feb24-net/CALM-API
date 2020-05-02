using Calm.Dtb;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calm.Lib
{
    public interface IGet
    {
        Task<UserItem> Login(string username, string password);
        Task<IEnumerable<UserItem>> UserList();
    }
}