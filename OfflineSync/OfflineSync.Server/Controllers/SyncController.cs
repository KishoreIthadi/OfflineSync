using OfflineSync.DomainModel.Enums;
using OfflineSync.DomainModel.Models;
using OfflineSync.Server.DB;
using OfflineSync.Server.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace OfflineSync.Server.Controllers
{
    public class SyncController : ApiController
    {
        public SyncController()
        {
        }

        [HttpGet]
        public HttpResponseMessage GetDeviceID()
        {
            try
            {
                using (SQLContext<tblSyncDevice> db = new SQLContext<tblSyncDevice>())
                {
                    string newID = Guid.NewGuid().ToString();

                    while (db.Set<tblSyncDevice>().Any(m => m.DeviceID == newID))
                    {
                        newID = Guid.NewGuid().ToString();
                    }

                    var rec = db.dbSet.Add(new tblSyncDevice
                    {
                        DeviceID = newID
                    });

                    db.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, newID);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetData(APIModel model)
        {
            try
            {
                if (model.FailedTrasationData != null && model.SyncType != SyncType.SyncServerToClient)
                {
                    InvokeDBMethod(model.ServerTableName, model.ServerAssemblyName,
                              "UpdateFailedTransactions", model);
                }

                if (model.AutoSync &&
                   (model.SyncType == SyncType.SyncTwoWay ||
                    model.SyncType == SyncType.SyncServerToClient))
                {
                    if (model.LastSyncDate == null)
                    {
                        model.Data = InvokeDBMethod(model.ServerTableName, model.ServerAssemblyName, "GetData", null);
                    }
                    else
                    {
                        model.Data = InvokeDBMethod(model.ServerTableName, model.ServerAssemblyName,
                            "GetDataByLastSyncDate", model.LastSyncDate);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, model);
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
                //model.Data = InvokeDBMethod(model.ServerTableName, model.ServerAssemblyName, "InsertUpdate", model);

                InvokeDBMethod(model.ServerTableName, model.ServerAssemblyName, "InsertUpdate", model);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public object InvokeDBMethod(string serverTableName, string serverAssemblyName,
            string methodName, object param)
        {
            Assembly assembly = Assembly.Load(serverAssemblyName);
            Type classType = typeof(SQLServerDBOperations);

            object obj = Activator.CreateInstance(classType);

            MethodInfo method = classType.GetMethod(methodName);

            if (method.IsGenericMethod)
            {
                Type tableType = assembly.GetType(serverAssemblyName + "." + serverTableName);
                method = method.MakeGenericMethod(tableType);
            }

            object result = null;

            if (param == null)
            {
                result = method.Invoke(obj, null);
            }
            else
            {
                result = method.Invoke(obj, new object[] { param });
            }

            return result;
        }
    }
}