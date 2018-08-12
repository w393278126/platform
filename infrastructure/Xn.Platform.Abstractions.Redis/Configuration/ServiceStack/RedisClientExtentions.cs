using System.Collections.Generic;
using  Xn.Platform.Abstractions.Redis;
using Xn.Platform.Core.Extensions;
using ServiceStack.Redis;

namespace Xn.Platform.Data.Redis
{
    public static class RedisClientExtentions
    {
        private static readonly IDictionary<string, Dictionary<string, string>> LuaBodySha = new Dictionary<string, Dictionary<string, string>>();

        public static string GetString(this IRedisClient client)
        {
            return string.Format("{0}:{1}", client.Host, client.Port);
        }

        public static string GetSha1(this IRedisClient client, string luaBody)
        {
            var key = client.GetString();
            var currentClient = LuaBodySha.GetValue(key);
            string sha;
            if (!currentClient.TryGetValue(luaBody, out sha))
            {
                sha = client.CalculateSha1(luaBody);
                if (!client.HasLuaScript(sha))
                {
                    sha = client.LoadLuaScript(luaBody);
                }
                currentClient[luaBody] = sha;
            }
            return sha;
        }

        public static int RemoveKeysByPattern(this IRedisClient client, string pattern)
        {
            return (int)client.ExecLuaAsInt(Script.RemoveKeysByPatternScript, pattern);
        }

        private const string RemoveEntries = "return redis.call('HDEL',KEYS[1], unpack(ARGV))";

        public static int RemoveEntriesFromHash(this IRedisClient client, string hashId, params string[] fields)
        {
            if (fields == null || fields.Length < 1)
                return 0;
            var sha1 = client.GetSha1(RemoveEntries);
            return (int) client.ExecLuaShaAsInt(sha1, new[] {hashId}, fields);
        }

        private const string RemoveSetItems = "return redis.call('SREM',KEYS[1], unpack(ARGV))";

        public static int RemoveItemsFromSet(this IRedisClient client, string setId, params string[] items)
        {
            if (items == null || items.Length < 1)
                return 0;
            var sha1 = client.GetSha1(RemoveSetItems);
            return (int)client.ExecLuaShaAsInt(sha1, new[] {setId}, items);
        }

        const string slow_log_command_text =
@"local logs = redis.call('SLOWLOG','GET',50)
local results = {}
if #logs > 0 then
	for i=1,#logs do
		local log = logs[i]
		local result = i..'. @'..log[2]..' cost '..(log[3]/1000)..'ms command:'..log[4][1]
		if #log[4] > 1 then
			result = result..' args:'
			for j=2,#(log[4]) do
				result = result..log[4][j]..','  
			end
		end
		table.insert(results,result)
	end
end
return results";

        public static List<string> GetSlowLog(this IRedisClient client)
        {
            var sha1 = client.GetSha1(slow_log_command_text);
            return client.ExecLuaShaAsList(sha1);
        }

        /*
            local hash_key = ARGV[1]
            local get_count = ARGV[2]
            local hash_all = redis.call('HGETALL',hash_key)
            local keys = {}
            for i = 1, #hash_all, 2 do
	            table.insert(keys,{hash_all[i],hash_all[i+1]})
            end
            table.sort(keys,function(a,b) return tonumber(a[2])> tonumber(b[2]) end)
            if #hash_all < get_count * 2 then
	            get_count = #hash_all / 2
            end
            local result = {}
            for i = 1, get_count do
	            table.insert(result, keys[i][1])
	            table.insert(result, keys[i][2])
            end
            return result
        */
        const string get_descending_hash_text = "local hash_key = ARGV[1] local get_count = ARGV[2] local hash_all = redis.call('HGETALL',hash_key) local keys = {} for i = 1, #hash_all, 2 do table.insert(keys,{hash_all[i],hash_all[i+1]}) end table.sort(keys,function(a,b) return tonumber(a[2])> tonumber(b[2]) end) if #hash_all < get_count * 2 then get_count = #hash_all / 2 end local result = {} for i = 1, get_count do table.insert(result, keys[i][1]) table.insert(result, keys[i][2]) end return result";

        /// <summary>
        /// 按value倒序获取一个hash中的对象
        /// </summary>
        /// <param name="client"></param>
        /// <param name="hashKey"></param>
        /// <param name="count">倒数N个</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDescendingValueFromHash(this IRedisClient client, string hashKey, int count)
        {
            var sha1 = client.GetSha1(get_descending_hash_text);
            var result = client.ExecLuaShaAsList(sha1, hashKey, count.ToString());
            var dic = new Dictionary<string, string>(result.Count / 2);
            for (var i = 0; i < result.Count; i += 2)
            {
                dic.Add(result[i], result[i + 1]);
            }
            return dic;
        }

        /*
            local hash_key = ARGV[1]
            local start = ARGV[2]
            local stop = ARGV[3]
            local hash_all = redis.call('HGETALL',hash_key)
            local keys = {}
            for i = 1, #hash_all, 2 do
	            table.insert(keys,{hash_all[i],hash_all[i+1]})
            end
            table.sort(keys,function(a,b) return tonumber(a[2])> tonumber(b[2]) end)
            if #hash_all < start * 2 then
	            start = #hash_all / 2
            end
            if #hash_all <  stop * 2 then
	            stop = #hash_all / 2
            end
            local result = {}
            for i = start, stop do
	            table.insert(result, keys[i][1])
	            table.insert(result, keys[i][2])
            end
            return result
         * */
        const string get_descending_hash_start_stop_text = "local hash_key = ARGV[1] local start = ARGV[2] local stop = ARGV[3] local hash_all = redis.call('HGETALL',hash_key) local keys = {} for i = 1, #hash_all, 2 do table.insert(keys,{hash_all[i],hash_all[i+1]}) end table.sort(keys,function(a,b) return tonumber(a[2])> tonumber(b[2]) end) if #hash_all < start * 2 then start = #hash_all / 2 end if #hash_all <  stop * 2 then stop = #hash_all / 2 end local result = {} for i = start, stop do table.insert(result, keys[i][1]) table.insert(result, keys[i][2]) end return result";
        /// <summary>
        /// 按value倒序获取一个hash中的对象
        /// </summary>
        /// <param name="client"></param>
        /// <param name="hashKey"></param>
        /// <param name="start">获取的第一个对象index（从1开始算,含）</param>
        /// <param name="stop">获取的最后一个对象index（从1开始算,含）</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDescendingValueFromHash(this IRedisClient client, string hashKey, int start,int stop)
        {
            var sha1 = client.GetSha1(get_descending_hash_start_stop_text);
            var result = client.ExecLuaShaAsList(sha1, hashKey, start.ToString(), stop.ToString());
            var dic = new Dictionary<string, string>(result.Count / 2);
            for (var i = 0; i < result.Count; i += 2)
            {
                dic.Add(result[i], result[i + 1]);
            }
            return dic;
        }

        const string sum_multi_hash_number_value_with_same_key = @"local result = 0
local key = ARGV[1]
for i = 1, #KEYS do
	local v = redis.call('HGET', KEYS[i], key)
	if v then
		result = result + tonumber(v)
	end
end
return result";

        /// <summary>
        /// 获取多个hash中的数字项的值的相加数
        /// </summary>
        /// <param name="redis"></param>
        /// <param name="keyInHash">比如房间id</param>
        /// <param name="hashKeys">比如r:gift:exp,r:live:exp</param>
        /// <returns></returns>
        public static long SumMiltiHashNumberValueWithSameKey(this IRedisClient redis, string keyInHash, params string[] hashKeys)
        {
            var sha1 = redis.GetSha1(sum_multi_hash_number_value_with_same_key);
            return redis.ExecLuaShaAsInt(sha1, hashKeys, new[] { keyInHash });
        }

        const string get_multi_hash_value_with_same_key = @"local result = {}
local key = ARGV[1]
for i = 1, #KEYS do
	table.insert(result, redis.call('HGET',KEYS[i],key));
end
return result";

        /// <summary>
        /// 获取多个hash中的相同项的值
        /// </summary>
        /// <param name="redis"></param>
        /// <param name="keyInHash">比如房间id</param>
        /// <param name="hashKeys">比如r:gift:exp,r:live:exp</param>
        /// <returns></returns>
        public static List<string> GetMiltiHashValueWithSameKey(this IRedisClient redis, string keyInHash, params string[] hashKeys)
        {
            var sha1 = redis.GetSha1(get_multi_hash_value_with_same_key);
            return redis.ExecLuaShaAsList(sha1, hashKeys, new[] { keyInHash });
        }
    }
}
