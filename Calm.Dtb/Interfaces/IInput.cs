using System.Threading.Tasks;

namespace Calm.Dtb
{
    public interface IInput
    {
        Task<T> Add<T>(T item) where T : class;
        Task Set<T>(T item, int id) where T : class;

        Task Remove<T>(T item) where T : class;
    }
}