using System.Threading.Tasks;

namespace Calm.Dtb
{
    public interface IInput
    {
        Task<T> Add<T>(T item) where T : class;
    }
}