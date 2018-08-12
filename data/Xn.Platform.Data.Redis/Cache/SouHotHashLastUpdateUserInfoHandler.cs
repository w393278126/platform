using Xn.Platform.Abstractions.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Data.Redis.Cache
{
    /// <summary>
    /// 热词搜索大后台配置-最后修改用户信息
    /// </summary>
    public class SouHotHashLastUpdateUserInfoHandler : RedisHash
    {
        public SouHotHashLastUpdateUserInfoHandler() : base(RedisKeyDefinition.SouHotHashLastUpdateUserInfo)
        {

        }


    }
}
