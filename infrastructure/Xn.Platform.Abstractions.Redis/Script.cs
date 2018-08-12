namespace Xn.Platform.Abstractions.Redis
{
	public class Script
	{
		public const string RedisTime = "return redis.call('TIME')";

		public const string StringIncrementLimitByMax = @"
local inc = tonumber(ARGV[1])
local max = tonumber(ARGV[2])
local x = redis.call('incrby', KEYS[1], inc)
if(x > max) then
	redis.call('set', KEYS[1], max)
	x = max
end
return x";

		public const string HashIncrementLimitByMax = @"
local inc = tonumber(ARGV[1])
local max = tonumber(ARGV[2])
local x = redis.call('hincrby', KEYS[1], KEYS[2], inc)
if(x > max) then
	redis.call('hset', KEYS[1], KEYS[2], max)
	x = max
end
return x";

		public const string SortedSetIncrementLimitByMax = @"
local mem = ARGV[1]
local inc = tonumber(ARGV[2])
local max = tonumber(ARGV[3])
local x = tonumber(redis.call('zincrby', KEYS[1], inc, mem))
if(x > max) then
	redis.call('zadd', KEYS[1], max, mem)
	x = max
end
return tostring(x)";

		public const string StringIncrementLimitByMin = @"
local inc = tonumber(ARGV[1])
local min = tonumber(ARGV[2])
local x = redis.call('incrby', KEYS[1], inc)
if(x < min) then
	redis.call('set', KEYS[1], min)
	x = min
end
return x";

		public const string HashIncrementLimitByMin = @"
local inc = tonumber(ARGV[1])
local min = tonumber(ARGV[2])
local x = redis.call('hincrby', KEYS[1], KEYS[2], inc)
if(x < min) then
	redis.call('hset', KEYS[1], KEYS[2], min)
	x = min
end
return x";

		public const string SortedSetIncrementLimitByMin = @"
local mem = ARGV[1]
local inc = tonumber(ARGV[2])
local min = tonumber(ARGV[3])
local x = tonumber(redis.call('zincrby', KEYS[1], inc, mem))
if(x < min) then
	redis.call('zadd', KEYS[1], min, mem)
	x = min
end
return tostring(x)";

		public const string SortedSetDecrementLimitZero = @"
local mem = ARGV[1]
local score = tonumber(ARGV[2])
if(score > 0) then
	redis.call('zincrby', KEYS[1], score, mem)
else
	local x = tonumber(redis.call('zscore', KEYS[1], mem))
    if x ~= nil then
	    if(x <= -score ) then
		    redis.call('zadd', KEYS[1], 0, mem)
	    else
		    redis.call('zincrby', KEYS[1], score, mem)
	    end
    end
end
return tostring(score)";


        public const string StringIncrementFloatLimitByMax = @"
local inc = tonumber(ARGV[1])
local max = tonumber(ARGV[2])
local x = tonumber(redis.call('incrbyfloat', KEYS[1], inc))
if(x > max) then
	redis.call('set', KEYS[1], max)
	x = max
end
return tostring(x)";

		public const string HashIncrementFloatLimitByMax = @"
local inc = tonumber(ARGV[1])
local max = tonumber(ARGV[2])
local x = tonumber(redis.call('hincrbyfloat', KEYS[1], KEYS[2], inc))
if(x > max) then
	redis.call('hset', KEYS[1], KEYS[2], max)
	x = max
end
return tostring(x)";

		public const string StringIncrementFloatLimitByMin = @"
local inc = tonumber(ARGV[1])
local min = tonumber(ARGV[2])
local x = tonumber(redis.call('incrbyfloat', KEYS[1], inc))
if(x < min) then
	redis.call('set', KEYS[1], min)
	x = min
end
return tostring(x)";

		public const string HashIncrementFloatLimitByMin = @"
local inc = tonumber(ARGV[1])
local min = tonumber(ARGV[2])
local x = tonumber(redis.call('hincrbyfloat', KEYS[1], KEYS[2], inc))
if(x < min) then
	redis.call('hset', KEYS[1], KEYS[2], min)
	x = min
end
return tostring(x)";

		public const string RemoveKeysByPatternScript = @"
local keys = redis.call('KEYS',ARGV[1])
for i=1, #keys do
	redis.call('DEL',keys[i])
end
return #keys";

		public const string IncrementLimitByMaxReturnDiffScript = @"
local key = ARGV[1]
local field = ARGV[2]
local value = tonumber(ARGV[3])
local max = tonumber(ARGV[4])
local current = tonumber(redis.call('HINCRBY', key, field, value))
if (current > max) then
	redis.call('HSET', key, field, max)
	return value - current + max
end
return value";

		/// <summary>
		/// ��վ����ͳ��
		/// </summary>
		public const string SiteOnlineUserExpiresAndUpdateCount =
@"local expiredAt = tonumber(ARGV[1])
local keywords = {'video','live'}
local needDeleteCount = 0
local needCountCount = 0
for ik,vkey in ipairs(keywords) do
	local keyword = vkey
	local onlineKeys = redis.call('KEYS','site:online:users:'..keyword..':*')
	local needCount = {}
	local pattern = 'site:online:users:'..keyword..':(%d+)'
	for i,v in ipairs(onlineKeys) do
		local expiredTime = string.match(v,pattern)
		if expiredTime ~= nil then
			if tonumber(expiredTime) < expiredAt then
				needDeleteCount = needDeleteCount + redis.call('DEL',v)
			else
				table.insert(needCount, v)
				needCountCount = needCountCount + 1
			end
		end
	end
	if needCountCount > 0 then
		local countKey = 'site:online:count:'..keyword
		local tempSetKey = 'site:online:temp:'..keyword
		local count = redis.call('SUNIONSTORE',tempSetKey,unpack(needCount))        
		redis.call('SET',countKey,count)
	end
end 
local tempAllSetKey = 'site:online:temp:all'
local allCount = redis.call('SUNIONSTORE',tempAllSetKey,'site:online:temp:video','site:online:temp:live')
redis.call('SET','site:online:count:all',allCount)
redis.call('DEL',tempAllSetKey,'site:online:temp:video','site:online:temp:live')
local result = {}
table.insert(result,needDeleteCount)
table.insert(result,needCountCount)
return result";

		/// <summary>
		/// _live_logon�����ṩ���������б�֧��
		/// </summary>
		public const string RoomOnlineUserExpiresAndUpdateCount =
@"local expiredAt = tonumber(ARGV[1])
local keyword = ARGV[2]
local onlineKeys = redis.call('KEYS','r:online:users'..keyword..':*')
local needCount = {}
local needCountCount = 0
local pattern = 'r:online:users'..keyword..':([%w%-]+):(%d+)'
for i,v in ipairs(onlineKeys) do
	local roomId, expiredTime = string.match(v,pattern)
	if roomId ~= nil and expiredTime ~= nil then
		if tonumber(expiredTime) < expiredAt then
			redis.call('DEL',v)
		end
		if needCount[roomId] then
			table.insert(needCount[roomId],v)
		else
			needCount[roomId] = {v}
			needCountCount = needCountCount +1
		end
	end
end
if needCountCount > 0 then
	local countHashKey = 'r:online:count'..keyword
	local tempHashKey = 'r:online:count:temp'..keyword
	for roomId,keyList in pairs(needCount) do
		local tempSetKey = 'r:online:temp'..keyword..':'..roomId
		local count = redis.call('SUNIONSTORE',tempSetKey,unpack(keyList))
		if keyword == '_live_logon' then
			local onlineUserListKey = 'r:online:list'..keyword..':'..roomId
			redis.call('DEL',onlineUserListKey)
			if count > 0 then
				redis.call('RENAME',tempSetKey,onlineUserListKey)
			end
		end
		redis.call('DEL',tempSetKey)
		redis.call('HSET',tempHashKey,roomId,count)
	end
	redis.call('DEL',countHashKey)
	redis.call('RENAME',tempHashKey,countHashKey)
end
return needCountCount";

		/// <summary>
		/// _live_logon�����ṩ���������б�֧��
		/// </summary>
		public const string RoomOnlineUserExpiresAndUpdateCountNew =
@"local expiredAt = tonumber(ARGV[1])
local keyword = ARGV[2]
local host = ARGV[3]
local port = ARGV[4]
local db = ARGV[5]
local command = ARGV[6]
local onlineKeys = redis.call('KEYS','r:online:users'..keyword..':*')
local needCount = {}
local needDeleteCount = 0
local needCountCount = 0
local pattern = 'r:online:users'..keyword..':([%w%-]+):(%d+)'
for i,v in ipairs(onlineKeys) do
	local roomId, expiredTime = string.match(v,pattern)
	if roomId ~= nil and expiredTime ~= nil then
		if tonumber(expiredTime) < expiredAt then
			needDeleteCount = needDeleteCount +redis.call('DEL',v)
		end
		if needCount[roomId] then
			table.insert(needCount[roomId],v)
		else
			needCount[roomId] = {v}
			needCountCount = needCountCount +1
		end
	end
end
if needCountCount > 0 then
	local tempHashKey = 'r:online:count:temp'..keyword
	for roomId,keyList in pairs(needCount) do
		local tempSetKey = 'r:online:temp'..keyword..':'..roomId
		local count = redis.call('SUNIONSTORE',tempSetKey,unpack(keyList))
		if keyword == '_live_logon' then
			local onlineUserListKey = 'r:online:list'..keyword..':'..roomId
			redis.call('DEL',onlineUserListKey)
			if count > 0 then
				redis.call('RENAME',tempSetKey,onlineUserListKey)
			end
		end
		redis.call('DEL',tempSetKey)
		if command == 'HSET' then
			redis.call(command,tempHashKey,roomId,count)
		else
			redis.call(command,tempHashKey,count,roomId)
		end		
	end
end
local result = {}
table.insert(result,needDeleteCount)
table.insert(result,needCountCount)
return result";

		public const string SortedOnlineListByUserGrade = @"local room = ARGV[1]
local startIndex = tonumber(ARGV[2]) + 1
local endIndex = tonumber(ARGV[3])
local onlineKey = 'r:online:list_live_logon:'..room
local userCount = redis.call('SCARD',onlineKey)
local online_users
if userCount > 500 then
	online_users = redis.call('SRANDMEMBER',onlineKey,endIndex - startIndex)
else
	online_users = redis.call('SMEMBERS',onlineKey)
end
if online_users == nil or #online_users < 1 or online_users[1] == nil then
	return nil
end
if #online_users < startIndex then 
	return nil
else
	local tmp = {}
	for i = 1,#online_users do
		local grade = tonumber( redis.call('HGET', 'u:grade', online_users[i]))
		if grade == nil then
			table.insert(tmp, { online_users[i], 1})
		else
			table.insert(tmp, { online_users[i], grade })
		end
	end
	local sortFunc = function(a,b) return a[2] > b[2] end
	table.sort(tmp, sortFunc)
	if #tmp < endIndex then
		endIndex = #tmp
	end	
	local result = {}	
	for i = startIndex, endIndex do
		table.insert(result,tmp[i][1])
		table.insert(result,tmp[i][2])
	end
	return result
end";

		public const string DeleteKeyByRename = @"local key = ARGV[1]
local isexist = redis.call('EXISTS',key)
if isexist == 1 then
	local index = redis.call('INCR','gc:index')
	local newkey = 'gc:keys:'..index
	redis.call('RENAME',key,newkey)
end";

		public const string RenameFast = @"local key = ARGV[1]
local newkey = ARGV[2]
local isnewkeyexist = redis.call('EXISTS',newkey)
if isnewkeyexist == 1 then
	local index = redis.call('INCR','gc:index')
	local newkeygc = 'gc:keys:'..index
	redis.call('RENAME',newkey,newkeygc)
end
redis.call('RENAME',key,newkey)";

		public const string SADD = @"return redis.call('SADD',KEYS[1], unpack(ARGV))";

		public const string SETNXEX = @"local result = redis.call('SET', KEYS[1], ARGV[1], 'EX', ARGV[2], 'NX' )
if result then
	return 1
else
	return 0
end";

		public const string HKEYSCAN =
@"local h_key = KEYS[1]
local h_cursor = ARGV[1]
local h_count = ARGV[2]
local results = redis.call('HSCAN',h_key,h_cursor,'COUNT',h_count)
local list = {}
if #results > 0 then
	table.insert(list,results[1])

	if #results[2] > 0 then
		for i = 1,#results[2],2 do
			table.insert(list,results[2][i])
		end
	end
end
return list";

	}
}