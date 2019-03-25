using System.Threading.Tasks;
using OfflineSync.DomainModel.Utilities;

namespace OfflineSync.Tests
{
    class SyncAPIUtilityTest : ISyncAPIUtility
    {
        public Task<T> Get<T>(string route)
        {

        }

        public Task<U> Post<T, U>(T data, string route)
        {
            throw new System.NotImplementedException();
        }
    }
}
