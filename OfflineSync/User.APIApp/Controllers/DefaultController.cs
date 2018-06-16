using System;
using System.Linq;
using System.Web.Http;

namespace User.APIApp.Controllers
{
    public class DefaultController : ApiController
    {
        public DefaultController()
        {
            using (TestDBContext db = new TestDBContext())
            {
                db.tblTestACTSs.Add(
                    new tblTestACTS
                    {
                        IsSynced = false,
                        Name = "aaa",
                        SyncCreatedAt = DateTime.UtcNow,
                        SyncModifiedAt = DateTime.UtcNow,
                        TransactionID = "asdf",
                        VersionID = "qwert"
                    }
                 );

                db.SaveChanges();

                db.tblTestACTSs.ToList();
            }
        }
    }
}
