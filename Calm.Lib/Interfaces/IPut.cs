using System.Threading.Tasks;

namespace Calm.Lib
{
    public interface IPut
    {
        Task SetUser(string username, string password, UserItem value);
    }
}