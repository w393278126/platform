using  Xn.Platform.Abstractions.Redis.Configuration;
using Xn.Platform.Core.Extensions;
using Xn.Platform.Data.Redis;
using ServiceStack.Redis;
using ServiceStack.Text;
using System;
using System.Threading.Tasks;

namespace Xn.Platform.Abstractions.Redis
{
    public static class DateTimeHelper
    {
        private static long _ticksDiff;

        static DateTimeHelper()
        {
            ResetDiff();

            Task.Run(RefreshAsync);
        }

        private static async Task RefreshAsync()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromMinutes(1)).ConfigureAwait(false);
                ResetDiff();
            }
        }

        private static void ResetDiff()
        {
            var redisTime = new long[2];
            using (var redis = Server.GetRedisClient(RedisServer.Time, true))
            {
                var multiDataList = ((RedisNativeClient)redis).Time();
                var servertime = DateTime.Now;
                for (var i = 0; i < 2; i++)
                {
                    redisTime[i] = multiDataList[i].FromUtf8Bytes().AsLong();
                }
                _ticksDiff = redisTime[0] * 1000000 + redisTime[1] - servertime.Ticks / 10;
            }
        }

        public static long GetRedisTimestampInMicroseconds()
        {
            return DateTime.Now.Ticks / 10 + _ticksDiff;
        }

        public static long GetRedisTimestampInMilliseconds()
        {
            return GetRedisTimestampInMicroseconds() / 1000;
        }

        public static long GetRedisTimestamp()
        {
            return GetRedisTimestampInMicroseconds() / 1000000;
        }

        public static DateTime GetRedisDateTime()
        {
            return GetRedisTimestamp().UnixTimeStampToDateTime();
        }

    }
}
