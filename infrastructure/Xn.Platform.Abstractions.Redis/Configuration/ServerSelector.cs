using System;
using System.Collections.Generic;
using StackExchange.Redis;

namespace Xn.Platform.Abstractions.Redis.Configuration
{
    public interface IServerSelector
    {
        RedisSettings Select(IList<RedisSettings> settings, RedisKey key);
    }

    public class SimpleHashingSelector : IServerSelector
    {
        public RedisSettings Select(IList<RedisSettings> settings, RedisKey key)
        {
            if (settings.Count == 0) throw new ArgumentException("settings length is 0");
            if (settings.Count == 1) return settings[0];
            using (var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                var hashBytes = md5.ComputeHash((byte[])key);
                var preSeed = BitConverter.ToInt32(hashBytes, 0);
                if (preSeed == int.MinValue) preSeed++; // int.MinValue can't do Abs

                var seed = System.Math.Abs(preSeed);
                var index = seed % settings.Count;
                return settings[index];
            }
        }
    }
}
