using System.Collections.Generic;

namespace Xn.Platform.Data.MongoDb.Configuration
{
    public class MongoGroup
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }

        public MongoGroup(string databaseName, MongoSettings[] mongoSettings)
        {
            DatabaseName = databaseName;
            ConnectionString = mongoSettings[0].ConnectionString;
        }
    }
}
