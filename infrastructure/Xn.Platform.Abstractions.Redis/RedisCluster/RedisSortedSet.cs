using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using  Xn.Platform.Abstractions.Redis.Configuration;
using sr = StackExchange.Redis;

namespace Xn.Platform.Abstractions.Redis.RedisCluster
{
    public class RedisSortedSet : RedisStructure, IRedisSortedSet
    {
        public RedisSortedSet(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
        }

        public bool Add(string keySuffix, string value, double score, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.SortedSetAdd(InitRedisKey(keySuffix), value, score, (sr.CommandFlags)commandFlags);
        }

        public long Add(string keySuffix, IDictionary<string, double> values, CommandFlags commandFlags = CommandFlags.None)
        {
            var sendValues = values.Select(x => new SortedSetEntry(x.Key, x.Value)).ToArray();
            return Client.SortedSetAdd(InitRedisKey(keySuffix), sendValues, (sr.CommandFlags)commandFlags);
        }

        public long CombineAndStore(SetOperation operation, string destination, string[] keys, double[] weights = null, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None)
        {
            throw new NotImplementedException();
        }

        public double Decrement(string keySuffix, string member, double value, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.SortedSetDecrement(InitRedisKey(keySuffix), member, value, (sr.CommandFlags)commandFlags);
        }

        public double SortedSetIncrementMineZero(string keySuffix, string value, long scores, CommandFlags commandFlags = CommandFlags.None)
        {
            return (double)Client.ScriptEvaluate(Script.SortedSetDecrementLimitZero, new[] { InitRedisKey(keySuffix) }, new RedisValue[] { value, scores }, (sr.CommandFlags)commandFlags);
        }

        public double DecrementLimitByMin(string keySuffix, string member, long value, CommandFlags commandFlags = CommandFlags.None)
        {
            var left = Client.SortedSetDecrement(InitRedisKey(keySuffix), member, value, (sr.CommandFlags)commandFlags);
            if (left < 0)
            {
                Client.SortedSetIncrement(InitRedisKey(keySuffix), member, value, (sr.CommandFlags)commandFlags);
            }
            return left;
        }

        public double Increment(string keySuffix, string member, double value, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.SortedSetIncrement(InitRedisKey(keySuffix), member, value, (sr.CommandFlags)commandFlags);
        }

        public long Length(string keySuffix, double min = double.NegativeInfinity, double max = double.PositiveInfinity, Exclude exclude = Exclude.None, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.SortedSetLength(InitRedisKey(keySuffix), min, max, (sr.Exclude)exclude, (sr.CommandFlags)commandFlags);
        }

        public string[] RangeByRank(string keySuffix, long start = 0, long stop = -1, Order order = Order.Ascending, CommandFlags commandFlags = CommandFlags.None)
        {
            var r = Client.SortedSetRangeByRank(InitRedisKey(keySuffix), start, stop, (sr.Order)order, (sr.CommandFlags)commandFlags);
            return r.Select(x => x.ToString()).ToArray();
        }

        public SortedSetEntry[] RangeByRankWithScores(string keySuffix, long start = 0, long stop = -1, Order order = Order.Ascending, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.SortedSetRangeByRankWithScores(InitRedisKey(keySuffix), start, stop, (sr.Order)order, (sr.CommandFlags)commandFlags);
        }
        public SortedSetEntry[] GetPageEntity(string keySuffix, int pageIndex, int pageSize, int extraNumber = 0)
        {
            return RangeByRankWithScores(keySuffix, pageIndex * pageSize, ((pageIndex + 1) * pageSize) - 1 + extraNumber, Order.Descending);
        }

        /// <summary>
        /// 返回区间内 排名 分数 member  item1 = member, item2 = 分数, items3 = 排名
        /// </summary>
        /// <returns>item1 = member, item2 = 分数, items3 = 排名</returns>
        public Tuple<string, double, double>[] RangeByRankWithScoresAndRank(string keySuffix, long start = 0, long stop = -1, Order order = Order.Ascending, CommandFlags commandFlags = CommandFlags.None)
        {
            SortedSetEntry[] rs;
            long startIndex = 0;
            if (start >= 0)
            {
                rs = Client.SortedSetRangeByRankWithScores(InitRedisKey(keySuffix), start, stop, (sr.Order)order, (sr.CommandFlags)commandFlags);
                startIndex = start;
            }
            else
            {
                var length = Client.SortedSetLength(InitRedisKey(keySuffix), flags: (sr.CommandFlags)commandFlags);
                rs = Client.SortedSetRangeByRankWithScores(InitRedisKey(keySuffix), start, stop, (sr.Order)order, (sr.CommandFlags)commandFlags);
                startIndex = length + start;
            }

            var result = new List<Tuple<string, double, double>>();
            var i = 0;
            foreach (var entry in rs)
            {
                result.Add(new Tuple<string, double, double>(entry.Element, entry.Score, startIndex + i));
                i++;
            }
            return result.ToArray();
        }

        public string[] RangeByScore(string keySuffix, double start = Double.NegativeInfinity, double stop = Double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1, CommandFlags commandFlags = CommandFlags.None)
        {
            var r = Client.SortedSetRangeByScore(InitRedisKey(keySuffix), start, stop, (sr.Exclude)exclude, (sr.Order)order, skip, take, (sr.CommandFlags)commandFlags);
            return r.Select(x => x.ToString()).ToArray();
        }

        public IDictionary<string, double> RangeByScoreWithScores(string keySuffix, double start = Double.NegativeInfinity, double stop = Double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1, CommandFlags commandFlags = CommandFlags.None)
        {
            var hashEntries = Client.SortedSetRangeByScoreWithScores(InitRedisKey(keySuffix), start, stop, (sr.Exclude)exclude, (sr.Order)order, skip, take, (sr.CommandFlags)commandFlags);
            var result = new Dictionary<string, double>();
            foreach (var hashEntry in hashEntries)
            {
                result[hashEntry.Element] = hashEntry.Score;
            }
            return result;
        }

        public string[] RangeByValue(string keySuffix, string min, string max, Exclude exclude = Exclude.None, long skip = 0, long take = -1, CommandFlags commandFlags = CommandFlags.None)
        {
            var r = Client.SortedSetRangeByValue(InitRedisKey(keySuffix), min, max, (sr.Exclude)exclude, skip, take, (sr.CommandFlags)commandFlags);
            return r.Select(x => x.ToString()).ToArray();
        }

        public long? Rank(string keySuffix, string member, Order order = Order.Ascending, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.SortedSetRank(InitRedisKey(keySuffix), member, (sr.Order)order, (sr.CommandFlags)commandFlags);
        }

        public bool Remove(string keySuffix, string member, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.SortedSetRemove(InitRedisKey(keySuffix), member, (sr.CommandFlags)commandFlags);
        }

        public long Remove(string keySuffix, string[] members, CommandFlags commandFlags = CommandFlags.None)
        {
            var redisValues = members.Select(x => (RedisValue)x).ToArray();
            return Client.SortedSetRemove(InitRedisKey(keySuffix), redisValues, (sr.CommandFlags)commandFlags);
        }

        public long RemoveRangeByRank(string keySuffix, long start, long stop, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.SortedSetRemoveRangeByRank(InitRedisKey(keySuffix), start, stop, (sr.CommandFlags)commandFlags);
        }

        public long RemoveRangeByScore(string keySuffix, double start, double stop, Exclude exclude = Exclude.None, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.SortedSetRemoveRangeByScore(InitRedisKey(keySuffix), start, stop, (sr.Exclude)exclude, (sr.CommandFlags)commandFlags);
        }

        public long RemoveRangeByScore(string keySuffix, string min, string max, Exclude exclude = Exclude.None, CommandFlags commandFlags = CommandFlags.None)
        {
            return Client.SortedSetRemoveRangeByValue(InitRedisKey(keySuffix), min, max, (sr.Exclude)exclude, (sr.CommandFlags)commandFlags);
        }

        public IEnumerable<SortedSetEntry> Scan(string keySuffix, RedisValue pattern = new RedisValue(), int pageSize = 10, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
        {
            return Client.SortedSetScan(InitRedisKey(keySuffix), pattern, pageSize, cursor, pageOffset, (sr.CommandFlags)flags);
        }

        public bool ExistMember(string keySuffix, string member, CommandFlags commandFlags = CommandFlags.None)
        {
            var result = Client.SortedSetScore(InitRedisKey(keySuffix), member, (sr.CommandFlags)commandFlags);
            return result.HasValue;
        }
        public double Score(string keySuffix, string member, CommandFlags commandFlags = CommandFlags.None)
        {
            var result = Client.SortedSetScore(InitRedisKey(keySuffix), member, (sr.CommandFlags)commandFlags);
            if (!result.HasValue)
            {
                result = 0;
            }
            return result.Value;
        }

        /// <summary>
        /// 返回排名和分数 item1：分数  item2：排名 
        /// </summary>
        /// <param name="keySuffix"></param>
        /// <param name="member"></param>
        /// <param name="rankOrder"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public Tuple<double, long> Get(string keySuffix, string member, Order rankOrder = Order.Ascending, CommandFlags commandFlags = CommandFlags.None)
        {
            var score = Client.SortedSetScore(InitRedisKey(keySuffix), member, (sr.CommandFlags)commandFlags);
            var rank = Client.SortedSetRank(InitRedisKey(keySuffix), member, (sr.Order)rankOrder, (sr.CommandFlags)commandFlags);
            if (score == null || rank == null)
            {
                return null;
            }
            return new Tuple<double, long>(score.Value, rank.Value);
        }

        public double IncrementLimitByMax(string keySuffix, string member, double value, double max, CommandFlags commandFlags = CommandFlags.None)
        {
            return (double)Client.ScriptEvaluate(Script.SortedSetIncrementLimitByMax, new[] { InitRedisKey(keySuffix) }, new RedisValue[] { member, value, max }, (sr.CommandFlags)commandFlags);
        }

        public double IncrementLimitByMin(string keySuffix, string member, double value, double min, CommandFlags commandFlags = CommandFlags.None)
        {
            return (double)Client.ScriptEvaluate(Script.SortedSetIncrementLimitByMin, new[] { InitRedisKey(keySuffix) }, new RedisValue[] { member, value, min }, (sr.CommandFlags)commandFlags);
        }
    }
}
