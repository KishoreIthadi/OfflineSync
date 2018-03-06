using System.Web.Http;
using OfflineSync.DomainModel;

namespace OfflineSyncServer.Controllers
{
    public class SyncController : ApiController
    {
        public SyncController()
        {

        }

        // GET: api/Sync
        [HttpGet]
        public string[] Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/Sync
        [HttpPost]
        public APIModel Post(APIModel model)
        {
            // model.Data
            // model.LastSyncDate
            // model.Type

            // Depending on the type we have to come up with generic update/insert logic logic

            return model;
        }
    }
}