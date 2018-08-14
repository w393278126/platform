using System;
using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Login
{
    public class XnValidateCodeHandler : RedisString
    {
        public XnValidateCodeHandler() : base(RedisKeyDefinition.LoginValidateCodeKey)
        {
        }

        public string SetCode(string value)
        {
            var token = Guid.NewGuid().ToString("N");
            Set(token, value.ToLower(), new TimeSpan(0, 10, 0));
            return token;
        }


        public bool IsAuthCode(string token, string value)
        {
            var val = Get(token);
            var result = val == value.ToLower();
            Delete(token);
            return result;
        }
    }
}
