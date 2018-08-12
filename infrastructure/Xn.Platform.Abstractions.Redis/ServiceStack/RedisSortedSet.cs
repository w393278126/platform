using Xn.Platform.Core.Extensions;
using Xn.Platform.Data.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace Xn.Platform.Abstractions.Redis
{
    public class RedisSortedSet : RedisStructure, IRedisSortedSet
    {
        public RedisSortedSet(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
        }

        public bool Add(string keySuffix, string value, double score, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //ZAdd
                return redis.AddItemToSortedSet(InitRedisKey(keySuffix), value, score);
            }
        }

        public long Add(string keySuffix, IDictionary<string, double> values, CommandFlags commandFlags = CommandFlags.None)
        {
            var count = 0;
            if (values.Count == 0)
                return count;
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                foreach (var value in values)
                {
                    //ZAdd
                    var result = redis.AddItemToSortedSet(key, value.Key, value.Value);
                    if (result)
                        count++;
                }
            }
            return count;
        }

        public long CombineAndStore(SetOperation operation, string destination, string[] keys, double[] weights = null,
            Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                if (operation == SetOperation.Intersect)
                {
                    //ZInterStore
                    return redis.StoreIntersectFromSortedSets(destination, keys);
                }
                if (operation == SetOperation.Union)
                {
                    //ZUnionStore
                    return redis.StoreUnionFromSortedSets(destination, keys);
                }
            }
            return 1;
        }

        public double Decrement(string keySuffix, string member, double value, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //ZIncrBy
                return redis.IncrementItemInSortedSet(InitRedisKey(keySuffix), member, value * -1);
            }
        }

        public double Increment(string keySuffix, string member, double value, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //ZIncrBy
                return redis.IncrementItemInSortedSet(InitRedisKey(keySuffix), member, value);
            }
        }

        public long Length(string keySuffix, double min = Double.NegativeInfinity, double max = Double.PositiveInfinity,
            Exclude exclude = Exclude.None, CommandFlags commandFlags = CommandFlags.None)
        {
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                if (double.IsNegativeInfinity(min) && double.IsPositiveInfinity(max))
                {
                    //ZCard
                    return redis.GetSortedSetCount(key);
                }
                //ZCount
                return redis.GetSortedSetCount(key, min, max);
            }
        }

        public string[] RangeByRank(string keySuffix, long start = 0, long stop = -1, Order order = Order.Ascending,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                if (start == 0 && stop == -1)
                {
                    if (order == Order.Ascending)
                    {
                        //ZRange
                        return redis.GetAllItemsFromSortedSet(key).ToArray();
                    }
                    //ZRevRange
                    return redis.GetAllItemsFromSortedSetDesc(key).ToArray();
                }
                if (order == Order.Ascending)
                {
                    //ZRange
                    return redis.GetRangeFromSortedSet(key, (int)start, (int)stop).ToArray();
                }
                //ZRevRange
                return redis.GetRangeFromSortedSetDesc(key, (int)start, (int)stop).ToArray();
            }
        }

        public string[] GetPageItems(string keySuffix, int pageIndex, int pageSize)
        {
            return RangeByRank(keySuffix, pageIndex * pageSize, ((pageIndex + 1) * pageSize) - 1, Order.Descending);
        }

        public SortedSetEntry[] RangeByRankWithScores(string keySuffix, long start = 0, long stop = -1, Order order = Order.Ascending,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var results = new List<SortedSetEntry>();
            IDictionary<string, double> entities;
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                if (start == 0 && stop == -1)
                {
                    if (order == Order.Ascending)
                    {
                        //ZRange
                        entities = redis.GetAllWithScoresFromSortedSet(key);
                    }
                    else
                    {
                        //ZRevRange
                        entities = redis.GetRangeWithScoresFromSortedSetDesc(key, 0, -1);
                    }
                }
                else if (order == Order.Ascending)
                {
                    //ZRange
                    entities = redis.GetRangeWithScoresFromSortedSet(key, (int)start, (int)stop);
                }
                else
                {
                    //ZRevRange
                    entities = redis.GetRangeWithScoresFromSortedSetDesc(key, (int)start, (int)stop);
                }
            }
            foreach (var entity in entities)
            {
                results.Add(new SortedSetEntry(entity.Key, entity.Value));
            }
            return results.ToArray();
        }

        public SortedSetEntry[] GetPageEntity(string keySuffix, int pageIndex, int pageSize)
        {
            return RangeByRankWithScores(keySuffix, pageIndex * pageSize, ((pageIndex + 1) * pageSize) - 1, Order.Descending);
        }

        public Tuple<string, double, double>[] RangeByRankWithScoresAndRank(string keySuffix, long start = 0, long stop = -1, Order order = Order.Ascending,
            CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public string[] RangeByScore(string keySuffix, double start = Double.NegativeInfinity, double stop = Double.PositiveInfinity,
            Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                if (skip == 0 && take == -1)
                {
                    if (order == Order.Ascending)
                    {
                        //ZRangeByScore
                        return redis.GetRangeFromSortedSetByLowestScore(key, start, stop).ToArray();
                    }
                    //ZRevRangeByScore
                    return redis.GetRangeFromSortedSetByHighestScore(key, start, stop).ToArray();
                }
                if (order == Order.Ascending)
                {
                    //ZRangeByScore
                    return redis.GetRangeFromSortedSetByLowestScore(key, start, stop, (int)skip, (int)take).ToArray();
                }
                //ZRevRangeByScore
                return redis.GetRangeFromSortedSetByHighestScore(key, start, stop, (int)skip, (int)take).ToArray();
            }
        }

        public IDictionary<string, double> RangeByScoreWithScores(string keySuffix, double start = Double.NegativeInfinity,
            double stop = Double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0,
            long take = -1, CommandFlags commandFlags = CommandFlags.None)
        {
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                if (skip == 0 && take == -1)
                {
                    if (order == Order.Ascending)
                    {
                        //ZRangeByScoreWithScores
                        return redis.GetRangeWithScoresFromSortedSetByLowestScore(key, start, stop);
                    }
                    //ZRevRangeByScoreWithScores
                    return redis.GetRangeWithScoresFromSortedSetByHighestScore(key, start, stop);
                }
                if (order == Order.Ascending)
                {
                    //ZRangeByScoreWithScores
                    return redis.GetRangeWithScoresFromSortedSetByLowestScore(key, start, stop, (int)skip, (int)take);
                }
                //ZRevRangeByScoreWithScores
                return redis.GetRangeWithScoresFromSortedSetByHighestScore(key, start, stop, (int)skip, (int)take);
            }
        }

        public string[] RangeByValue(string keySuffix, string min, string max, Exclude exclude = Exclude.None, long skip = 0,
            long take = -1, CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public long? Rank(string keySuffix, string member, Order order = Order.Ascending, CommandFlags commandFlags = CommandFlags.None)
        {
            var key = InitRedisKey(keySuffix);
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                if (order == Order.Ascending)
                {
                    //ZRank
                    return redis.GetItemIndexInSortedSet(key, member);
                }
                //ZRevRank
                return redis.GetItemIndexInSortedSetDesc(key, member);
            }
        }

        public bool Remove(string keySuffix, string member, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //ZRem
                return redis.RemoveItemFromSortedSet(InitRedisKey(keySuffix), member);
            }
        }

        public long Remove(string keySuffix, string[] members, CommandFlags commandFlags = CommandFlags.None)
        {
            var key = InitRedisKey(keySuffix);
            var count = 0;
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                foreach (var member in members)
                {
                    //ZRem
                    var result = redis.RemoveItemFromSortedSet(key, member);
                    if (result)
                        count++;
                }
            }
            return count;
        }

        public long RemoveRangeByRank(string keySuffix, long start, long stop, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //ZRemRangeByRank
                return redis.RemoveRangeFromSortedSet(InitRedisKey(keySuffix), (int)start, (int)stop);
            }
        }

        public long RemoveRangeByScore(string keySuffix, double start, double stop, Exclude exclude = Exclude.None,
            CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, true))
            {
                //ZRemRangeByScore
                return redis.RemoveRangeFromSortedSetByScore(InitRedisKey(keySuffix), start, stop);
            }
        }

        public long RemoveRangeByScore(string keySuffix, string min, string max, Exclude exclude = Exclude.None,
            CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SortedSetEntry> Scan(string keySuffix, RedisValue pattern = new RedisValue(), int pageSize = 10, long cursor = 0,
            int pageOffset = 0, CommandFlags flags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public double Score(string keySuffix, string member, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //ZScore
                var result = redis.GetItemScoreInSortedSet(InitRedisKey(keySuffix), member);
                if (double.IsNaN(result))
                {
                    return 0;
                }
                return result;
            }
        }

        public bool Exist(string keySuffix, string member, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                //ZScore
                var result = redis.GetItemScoreInSortedSet(InitRedisKey(keySuffix), member);
            
                return !double.IsNaN(result);
            }
        }

        public Tuple<double, long> Get(string keySuffix, string member, Order rankOrder = Order.Ascending,
            CommandFlags commandFlags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public double IncrementLimitByMax(string keySuffix, string member, double value, double max, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                return redis.SortedSetIncrementLimitByMax(InitRedisKey(keySuffix), member, value, max).AsDouble();
            }
        }

        public double IncrementLimitByMin(string keySuffix, string member, double value, double min, CommandFlags commandFlags = CommandFlags.None)
        {
            using (var redis = Server.GetRedisClient(ServerName, false))
            {
                return redis.SortedSetIncrementLimitByMin(InitRedisKey(keySuffix), member, value, min).AsDouble();
            }
        }
    }
}
