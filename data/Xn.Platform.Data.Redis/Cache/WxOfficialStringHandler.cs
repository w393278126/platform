using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Cache
{
    public class WxOfficialAkStringHandler:RedisString
    {
        public WxOfficialAkStringHandler() : base(RedisKeyDefinition.ConfigStringWxOfficialAk)
        {
        }
    }

    public class WxOfficialJsApiTiketHandler : RedisString
    {
        public WxOfficialJsApiTiketHandler() : base(RedisKeyDefinition.ConfigStringWxOfficialJsApiTiket)
        {
        }

    }

    public class WxOfficialPayNoticeHandler : RedisString
    {
        public WxOfficialPayNoticeHandler() : base(RedisKeyDefinition.CacheStringWxOfficialPayNotice)
        {
        }
    }
}
