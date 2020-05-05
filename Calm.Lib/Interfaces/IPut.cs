using Calm.Lib.Items;
using System.Threading.Tasks;

namespace Calm.Lib
{
    public interface IPut
    {
        Task SetUser(string username, string password, UserItem value);
        Task SwapUserStatus(string username, string password, string subjectUser);
        Task EditGatheringInfo(string username, string password, string formerTitle, GatheringItemIn gathering);
    }
}