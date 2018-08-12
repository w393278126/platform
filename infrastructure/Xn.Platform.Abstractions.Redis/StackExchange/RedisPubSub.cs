using  Xn.Platform.Abstractions.Redis.Configuration;
using StackExchange.Redis;

namespace Xn.Platform.Abstractions.Redis.StackExchange
{
    public class RedisPubSub : IRedisPubSub
    {
        public long Publish(string channel, string message, CommandFlags commandFlags = CommandFlags.None)
        {
            var settings = RedisServer.ConfigDict[RedisServer.Chat].MasterSettings[0];
            var connection = settings.GetConnection();
            ISubscriber sub = connection.GetSubscriber();
            var result = sub.Publish(channel, message);
            return result;
        }
    }
}
