using System.Collections.Generic;

namespace TwoWaySyncServer.DB
{
    public class SQLServerDBOperations<T> : IDBOperations<T>
    {
        public string _connString;

        public SQLServerDBOperations(string connString)
        {
            _connString = connString;
        }

        public List<T> GetData()
        {
            return null;
        }
    }
}