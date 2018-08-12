using Xn.Platform.Data.Redis;
using ServiceStack.Redis;
using ServiceStack.Text;
using System.Linq;

namespace Xn.Platform.Abstractions.Redis
{
    /// <summary>
    /// todo
    /// 
    /// GetRangeFromSortedList
    /// GetSortedItemsFromList
    /// BLPOP key [key ...] timeout BlockingRemoveStartFromLists
    /// BRPOP key [key ...] timeout BlockingDequeueItemFromList\BlockingPopItemFromList\BlockingPopItemFromLists
    /// RPOPLPUSH source destination PopAndPushItemBetweenLists
    /// </summary>
    public class RedisList : RedisStructure, IRedisList
    {
        public RedisList(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
        }

        public string GetByIndex(string keySuffix, long index, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //LIndex
                return redis.GetItemFromList(InitRedisKey(keySuffix), (int)index);
            } 
        }

        public long InsertAfter(string keySuffix, string pivot, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new System.NotImplementedException();
        }

        public long InsertBefore(string keySuffix, string pivot, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new System.NotImplementedException();
        }

        public string LeftPop(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //LPop
                return redis.RemoveStartFromList(InitRedisKey(keySuffix));
            }
        }

        public long LeftPush(string keySuffix, string value, When when = When.Always, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //LPush
                var result = ((RedisNativeClient)redis).LPush(InitRedisKey(keySuffix), value.ToUtf8Bytes());
                //或者
                //redis.EnqueueItemOnList(Key, value);
                return result;
            }
        }

        public long LeftPush(string keySuffix, string[] values, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //LPush
                redis.PrependRangeToList(InitRedisKey(keySuffix), values.ToList());
            }
            return 1;
        }

        public long Length(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //LLen
                return redis.GetListCount(InitRedisKey(keySuffix));
            }
        }

        public string[] Range(string keySuffix, long start = 0, long stop = -1, CommandFlags commandFlags = CommandFlags.None)
        {
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                if (start == 0 && stop == -1)
                {
                    //LRange 0 -1
                    return redis.GetAllItemsFromList(key).ToArray();
                }
                //LRange
                return redis.GetRangeFromList(key, (int)start, (int)stop).ToArray();
            }
        }

        public long Remove(string keySuffix, string value, long count = 0, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //LRem
                return redis.RemoveItemFromList(InitRedisKey(keySuffix), value, (int)count);
            }
        }

        public string RightPop(string keySuffix, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //RPop
                return redis.RemoveEndFromList(InitRedisKey(keySuffix));
                //或者
                //return redis.DequeueItemFromList(Key);
                //return redis.PopItemFromList(Key);
            }
        }

        public string RightPopLeftPush(string keySuffix, string destination, CommandFlags flags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //BRPopLPush
                return redis.BlockingPopAndPushItemBetweenLists(InitRedisKey(keySuffix), destination, null);
            }
        }

        public long RightPush(string keySuffix, string value, When when = When.Always, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //RPush
                var result = ((RedisNativeClient)redis).RPush(InitRedisKey(keySuffix), value.ToUtf8Bytes());
                return result;
                //或者
                //redis.PushItemToList(Key, value);
            }
        }

        public long RightPush(string keySuffix, string[] values, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //RPush
                redis.AddRangeToList(InitRedisKey(keySuffix), values.ToList());
            }
            return 1;
        }

        public void SetByIndex(string keySuffix, int index, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //LSet
                redis.SetItemInList(InitRedisKey(keySuffix), index, value);
            }
        }

        public void Trim(string keySuffix, long start, long stop, CommandFlags commandFlags = CommandFlags.None)
        {
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                if (start == 0 && stop == -1)
                {
                    //LTrim
                    redis.RemoveAllFromList(key);
                }
                //LTrim
                redis.TrimList(key, (int)start, (int)stop);
            }
        }

        /// <summary>
        /// LeftPushTrim
        /// </summary>
        public void LeftPushTrim(string keySuffix, string value, int count)
        {
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                redis.PrependItemToList(key, value);
                //Trim
                redis.TrimList(key, 0, count);
            }
        }
    }
}
