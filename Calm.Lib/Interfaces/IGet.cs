using Calm.Dtb;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calm.Lib
{
    public interface IGet
    {
        Task<User> Login(string username, string password);
    }
}