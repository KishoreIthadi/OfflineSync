using System;
using System.Reflection;
using System.Web.Http;
using OfflineSync.DomainModel;
using OfflineSyncServer.DB;

namespace OfflineSyncServer.Controllers
{
    public class SyncController : ApiController
    {
        public SyncController()
        {
        }

        [HttpGet]
        public APIModel Get(string serverTableName, string serverAssemblyName,
                            DateTime? lastSyncDate = null)
        {
            Assembly assembly = Assembly.Load(serverAssemblyName);

            Type tableType = assembly.GetType(serverAssemblyName + "." + serverTableName);

            Type genericClass = typeof(SQLServerDBOperations<>);

            Type constructedClass = genericClass.MakeGenericType(tableType);

            object obj = Activator.CreateInstance(constructedClass);

            MethodInfo method;

            object result = null;

            if (lastSyncDate == null)
            {
                method = constructedClass.GetMethod("GetData");
                result = method.Invoke(obj, null);
            }
            else
            {
                method = constructedClass.GetMethod("GetDataByLastSyncDate");
                result = method.Invoke(obj, new object[] { lastSyncDate });
            }

            APIModel returnObj = new APIModel();
            returnObj.Data = result;

            return returnObj;
        }

        // POST: api/Sync
        //[HttpPost]
        //public APIModel Post(APIModel model)
        //{
        //    var x = model.ModelName.Split(',');

        //    var an = new AssemblyName(x[1]);
        //    var assem = Assembly.Load(an);

        //    Type tableType = assem.GetType(x[0]);
        //    Type genericClass = typeof(DBOperations<>);
        //    Type constructedClass = genericClass.MakeGenericType(tableType);
        //    object obj = Activator.CreateInstance(constructedClass);

        //    MethodInfo method = constructedClass.GetMethod("PostData");
        //    //MethodInfo method = constructedClass.GetMethod("PostData");

        //    var result = method.Invoke(obj, new object[] { model });

        //    var returnObj = new APIModel();
        //    returnObj.Data = result;

        //    return returnObj;
        //}
    }
}