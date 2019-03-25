using System.Threading.Tasks;

namespace OfflineSync.DomainModel.Utilities
{
    public interface ISyncAPIUtility
    {
        Task<T> Get<T>(string route);
        Task<U> Post<T, U>(T data, string route);
    }
}