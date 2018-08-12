namespace Xn.Platform.Abstractions.Redis.LuaScripts
{
    public struct RedisScanLua
    {
        public const string HSCAN =
@"local h_key = ARGV[1]
local h_cursor = ARGV[2]
local h_count = ARGV[3]
local results = redis.call('HSCAN',h_key,h_cursor,'COUNT',h_count)
local list = {}
if #results > 0 then
    table.insert(list,results[1])

    if #results[2] > 0 then
        for i = 1,#results[2] do
            table.insert(list,results[2][i])
        end
    end
end
return list";

        public const string SSCAN =
@"local s_key = ARGV[1]
local s_cursor = ARGV[2]
local s_count = ARGV[3]
local results = redis.call('SSCAN',s_key,s_cursor,'COUNT',s_count)
local list = {}
if #results > 0 then
    table.insert(list,results[1])

    if #results[2] > 0 then
        for i = 1,#results[2] do
            table.insert(list,results[2][i])
        end
    end
end
return list";

        public const string HKEYSCAN =
@"local h_key = ARGV[1]
local h_cursor = ARGV[2]
local h_count = ARGV[3]
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

        public const string SCAN =
@"local pattern = ARGV[1]
local cursor = ARGV[2]
local count = ARGV[3]
local results = redis.call('SCAN',cursor,'MATCH',pattern,'COUNT',count)
local list = {}
if #results > 0 then
    table.insert(list,results[1])

    if #results[2] > 0 then
        for i = 1,#results[2] do
            table.insert(list,results[2][i])
        end
    end
end
return list";


    }
}
