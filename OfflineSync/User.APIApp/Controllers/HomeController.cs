using OfflineSync.DomainModel.Models;
using System.Net.Http;
using System.Web.Http;

namespace User.APIApp.Controllers
{
    public class HomeController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetData(APIModel model)
        {
            return null;
        }
    }
}