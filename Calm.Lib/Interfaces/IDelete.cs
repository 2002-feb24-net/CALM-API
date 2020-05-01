using System.Threading.Tasks;

namespace Calm.Lib
{
    public interface IDelete
    {
        Task RemoveUser(string username, string password);
    }
}