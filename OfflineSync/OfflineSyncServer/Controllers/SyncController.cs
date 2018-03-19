using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using OfflineSync.DomainModel;
using OfflineSyncServer.DB;

namespace OfflineSyncServer.Controllers
{
    public class SyncController : ApiController
    {
        public object InvokeDBMethod(string serverTableName, string serverAssemblyName, string methodName, object param)
        {
            Assembly assembly = Assembly.Load(serverAssemblyName);

            Type tableType = assembly.GetType(serverAssemblyName + "." + serverTableName);
            Type genericClass = typeof(SQLServerDBOperations<>);
            Type constructedClass = genericClass.MakeGenericType(tableType);

            object obj = Activator.CreateInstance(constructedClass);

            MethodInfo method;
            object result = null;

            if (param == null)
            {
                method = constructedClass.GetMethod(methodName);
                result = method.Invoke(obj, null);
            }
            else
            {
                method = constructedClass.GetMethod(methodName);
                result = method.Invoke(obj, new object[] { param });
            }

            return result;
        }

        [HttpGet]
        public HttpResponseMessage Get(string serverTableName, string serverAssemblyName,
                            DateTime? lastSyncDate = null)
        {
            try
            {
                APIModel returnObj = new APIModel();

                if (lastSyncDate == null)
                {
                    returnObj.Data = InvokeDBMethod(serverTableName, serverAssemblyName, "GetData", null);
                }
                else
                {
                    returnObj.Data = InvokeDBMethod(serverTableName, serverAssemblyName, "GetDataByLastSyncDate",
                                                   lastSyncDate);
                }

                return Request.CreateResponse(HttpStatusCode.OK, returnObj);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage PostData(APIModel model)
        {
            try
            {
                // List<T> tObj = JsonConvert.DeserializeObject<List<T>>(model.Data.ToString());

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}