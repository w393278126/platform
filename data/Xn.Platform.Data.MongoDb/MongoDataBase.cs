using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Xn.Platform.Data.MongoDb.Configuration;

namespace Xn.Platform.Data.MongoDb
{
    public class MongoDataBase
    {
        public static string PluBetMongo = "plubet";
        public static string LiveMongo = "livedb";
        public static string MailMongo = "maildb";
        public static string CollectMongo = "collectproplog";

        public static readonly Dictionary<string, MongoGroup> ConfigDict = MongoConfigurationSection
            .GetSection()
            .ToMongoGroups()
            .ToDictionary(x => x.DatabaseName);

        private static readonly ConcurrentDictionary<string, MongoServer> Servers = new ConcurrentDictionary<string, MongoServer>();


        internal static MongoServer GetServer(string database)
        {
            return Servers.GetOrAdd(database, key =>
            {
                var connectionString = ConfigDict[database].ConnectionString;
                return new MongoClient(connectionString).GetServer();
            });
        }

        public static MongoCollection<T> GetCollection<T>(string database)
        {
            return GetServer(database).GetDatabase(database).GetCollection<T>(typeof(T).Name);
        }
    }
}
