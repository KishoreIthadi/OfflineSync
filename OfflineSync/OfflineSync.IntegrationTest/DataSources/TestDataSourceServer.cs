using System;
using User.Server.SQLModels;

namespace OfflineSync.IntegrationTest.DataSources
{
    static class TestDataSourceServer
    {
        public static tblTestACTS RecordOne = new tblTestACTS
        {
            StringType = "Record One",
            IntType = 592,
            FloatType = 100.9399393233f,
            DateType = DateTime.Now
        };
    }
}
