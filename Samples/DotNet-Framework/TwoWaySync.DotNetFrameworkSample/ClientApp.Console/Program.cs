using SQLite;
using System;
using System.Threading;
using TwoWaySync.DomainModel;
using TwoWaySyncClient;

namespace ClientApp.Console
{
    public class TestTable : ISyncBaseModel
    {
        public Guid ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:52058/api/";
            string DBPath = @"Test.db";

            SQLiteConnection conn = new SQLiteConnection(DBPath);
            conn.CreateTable<TestTable>();

            conn.Insert(new TestTable()
            {
                ID = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                IsActive = true,
                ModifiedAt = DateTime.Now,
                Name = Guid.NewGuid().ToString()
            });

            Sync<TestTable> sync = new Sync<TestTable>(DBPath, url, null, DBTypeEnum.SQLServer);
            sync.StartSyncAsync();

            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}