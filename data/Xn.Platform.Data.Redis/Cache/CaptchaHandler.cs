using System;
using Xn.Platform.Abstractions.Redis;

namespace Xn.Platform.Data.Redis.Cache
{
    /// <summary>
    /// Cache
    /// </summary>
    public class CaptchaHandler : RedisString
    {
        public CaptchaHandler() : base(RedisKeyDefinition.CacheStringCaptcha)
        {
        }
        public bool Validate(string id, string captcha, bool refresh)
        {
            var code = captcha.ToLower();
            var value = Get(id);
            var result = value == code;
            if (refresh)
                Delete(id);
            return result;
        }

        /// <summary>
        /// 设置验证码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="captcha"></param>
        public void SetCaptcha(string id, string captcha)
        {
            if (id == null || captcha == null)
                throw new ArgumentNullException();
            var code = captcha.ToLower();
            Set(id, code, new TimeSpan(0, 10, 0));
        }
    }
}
