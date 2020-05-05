using Calm.Dtb;
using Calm.Lib.Items;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calm.Lib
{
    public interface IGet
    {
        Task<UserItem> Login(string username, string password);
        Task<IEnumerable<UserItem>> UserList();
        Task<IEnumerable<GatheringItemOut>> ListGatherings();
    }
}