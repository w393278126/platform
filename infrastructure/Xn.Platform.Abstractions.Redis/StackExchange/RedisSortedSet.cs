using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;
using sr = StackExchange.Redis;
namespace Xn.Platform.Abstractions.Redis.StackExchange
{
    public class RedisSortedSet : RedisStructure, IRedisSortedSet
    {
        public RedisSortedSet(string[] serverKeyPrefixFormat) : base(serverKeyPrefixFormat)
        {
        }
        public bool Add(string keySuffix, string value, double score, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.SortedSetAdd(Key, value, score, (sr.CommandFlags)commandFlags);
        }
        public long Add(string keySuffix, IDictionary<string, double> values, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            var sendValues = values.Select(x => new SortedSetEntry(x.Key, x.Value)).ToArray();
            return command.SortedSetAdd(Key, sendValues, (sr.CommandFlags)commandFlags);
        }
        public long CombineAndStore(SetOperation operation, string destination, string[] keys,
            double[] weights = null, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None)
        {
            var command = RedisGroup.GetCommand(true, destination);
            var redisKeys = keys.Select(x => (RedisKey)x).ToArray();
            return command.SortedSetCombineAndStore((sr.SetOperation)operation, destination, redisKeys, weights, (sr.Aggregate)aggregate, (sr.CommandFlags)flags);
        }
        public double Decrement(string keySuffix, string member, double value, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.SortedSetDecrement(Key, member, value, (sr.CommandFlags)commandFlags);
        }
        public double Increment(string keySuffix, string member, double value, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.SortedSetIncrement(Key, member, value, (sr.CommandFlags)commandFlags);
        }
        public long Length(string keySuffix, double min = double.NegativeInfinity, double max = double.PositiveInfinity, Exclude exclude = Exclude.None, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.SortedSetLength(Key, min, max, (sr.Exclude)exclude, (sr.CommandFlags)commandFlags);
        }
        public string[] RangeByRank(string keySuffix, long start = 0, long stop = -1, Order order = Order.Ascending, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            var r = command.SortedSetRangeByRank(Key, start, stop, (sr.Order)order, (sr.CommandFlags)commandFlags);
            return r.Select(x => x.ToString()).ToArray();
        }
        public SortedSetEntry[] RangeByRankWithScores(string keySuffix, long start = 0, long stop = -1, Order order = Order.Ascending, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            var result = command.SortedSetRangeByRankWithScores(Key, start, stop, (sr.Order)order, (sr.CommandFlags)commandFlags);
            return result;
        }
        public Tuple<string, double, double>[] RangeByRankWithScoresAndRank(string keySuffix, long start = 0, long stop = -1, Order order = Order.Ascending, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            SortedSetEntry[] rs;
            long startIndex = 0;
            if (start >= 0)
            {
                rs = command.SortedSetRangeByRankWithScores(Key, start, stop, (sr.Order)order, (sr.CommandFlags)commandFlags);
                startIndex = start;
            }
            else
            {
                var length = command.SortedSetLength(Key, flags: (sr.CommandFlags)commandFlags);
                rs = command.SortedSetRangeByRankWithScores(Key, start, stop, (sr.Order)order, (sr.CommandFlags)commandFlags);
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
        public string[] RangeByScore(string keySuffix, double start = double.NegativeInfinity, double stop = double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            var r = command.SortedSetRangeByScore(Key, start, stop, (sr.Exclude)exclude, (sr.Order)order, skip, take, (sr.CommandFlags)commandFlags);
            return r.Select(x => x.ToString()).ToArray();
        }
        public IDictionary<string, double> RangeByScoreWithScores(string keySuffix, double start = double.NegativeInfinity, double stop = double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            var hashEntries = command.SortedSetRangeByScoreWithScores(Key, start, stop, (sr.Exclude)exclude, (sr.Order)order, skip, take, (sr.CommandFlags)commandFlags);
            var result = new Dictionary<string, double>();
            foreach (var hashEntry in hashEntries)
            {
                result[hashEntry.Element] = hashEntry.Score;
            }
            return result;
        }
        public string[] RangeByValue(string keySuffix, string min, string max, Exclude exclude = Exclude.None, long skip = 0, long take = -1, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            var r = command.SortedSetRangeByValue(Key, min, max, (sr.Exclude)exclude, skip, take, (sr.CommandFlags)commandFlags);
            return r.Select(x => x.ToString()).ToArray();
        }
        public long? Rank(string keySuffix, string member, Order order = Order.Ascending, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            return command.SortedSetRank(Key, member, (sr.Order)order, (sr.CommandFlags)commandFlags);
        }
        public bool Remove(string keySuffix, string member, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.SortedSetRemove(Key, member, (sr.CommandFlags)commandFlags);
        }
        public long Remove(string keySuffix, string[] members, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            var redisValues = members.Select(x => (RedisValue)x).ToArray();
            return command.SortedSetRemove(Key, redisValues, (sr.CommandFlags)commandFlags);
        }
        public long RemoveRangeByRank(string keySuffix, long start, long stop, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.SortedSetRemoveRangeByRank(Key, start, stop, (sr.CommandFlags)commandFlags);
        }
        public long RemoveRangeByScore(string keySuffix, double start, double stop, Exclude exclude = Exclude.None, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.SortedSetRemoveRangeByScore(Key, start, stop, (sr.Exclude)exclude, (sr.CommandFlags)commandFlags);
        }
        public long RemoveRangeByScore(string keySuffix, string min, string max, Exclude exclude = Exclude.None, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return command.SortedSetRemoveRangeByValue(Key, min, max, (sr.Exclude)exclude, (sr.CommandFlags)commandFlags);
        }

        public IEnumerable<SortedSetEntry> Scan(string keySuffix, RedisValue pattern = default(RedisValue), int pageSize = 10, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            return command.SortedSetScan(Key, pattern, pageSize, cursor, pageOffset, (sr.CommandFlags)flags);
        }
        public double Score(string keySuffix, string member, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            var result = command.SortedSetScore(Key, member, (sr.CommandFlags)commandFlags);
            if (!result.HasValue)
            {
                result = 0;
            }
            return result.Value;
        }
        public Tuple<double, long> Get(string keySuffix, string member, Order rankOrder = Order.Ascending, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(false, Key);
            var score = command.SortedSetScore(Key, member, (sr.CommandFlags)commandFlags);
            var rank = command.SortedSetRank(Key, member, (sr.Order)rankOrder, (sr.CommandFlags)commandFlags);
            if (score == null || rank == null)
            {
                return null;
            }
            return new Tuple<double, long>(score.Value, rank.Value);
        }
        public double IncrementLimitByMax(string keySuffix, string member, double value, double max, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return (double)command.ScriptEvaluate(Script.SortedSetIncrementLimitByMax, new[] { Key }, new RedisValue[] { member, value, max }, (sr.CommandFlags)commandFlags);
        }
        public double IncrementLimitByMin(string keySuffix, string member, double value, double min, CommandFlags commandFlags = CommandFlags.None)
        {
            InitRedisKey(keySuffix);
            var command = RedisGroup.GetCommand(true, Key);
            return (double)command.ScriptEvaluate(Script.SortedSetIncrementLimitByMin, new[] { Key }, new RedisValue[] { member, value, min }, (sr.CommandFlags)commandFlags);
        }
    }
}
