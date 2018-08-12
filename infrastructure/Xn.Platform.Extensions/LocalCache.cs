using Logging.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text;

namespace Xn.Platform.Extensions
{
    /// <summary>
    /// 缓存通用类
    /// </summary>
    public class LocalCache
    {
        private static readonly MemoryCache Cache = new MemoryCache(nameof(LocalCache));
        private static readonly ConcurrentDictionary<string, object> Lockers = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// 用于存储缓存失效前的有效数据
        /// </summary>
        private static readonly ConcurrentDictionary<string, object> DicMemory = new ConcurrentDictionary<string, object>();


        /// <summary>
        /// 日志
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(LocalCache));

        private LocalCache()
        {

        }

        public static LocalCache Current { get; } = new LocalCache();

        /// <summary>
        /// 缓存是否存在
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            return Cache[key] != null;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            var o = Cache[key];

            if (o == null) return default(T);

            if (o is T)
                return (T)o;

            return default(T);
        }


        public T GetOrSet<T>(string key, Func<T> getFunc, TimeSpan? duration, bool sliding = false)
        {
            if (Exists(key))
                return Get<T>(key);

            var value = default(T);
            if (getFunc == null)
                return value;

            var locker = Lockers.GetOrAdd(key, new object());
            lock (locker)
            {
                if (Exists(key))
                    return Get<T>(key);

                value = getFunc();
                Set(key, value, duration, sliding);
            }
            return value;
        }

        /// <summary>
        /// 从缓存中获取数据。当缓存没有的时候，第一次进入的请求从内存中获取，内存中没有则先从数据源处更新数据,如果失败从本地/持久化中存储获取
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="getFunc">正常数据源</param>
        /// <param name="getFuncLocal">持久化数据源</param>
        /// <param name="duration">缓存时间</param>
        public T GetOrSet<T>(string key, Func<T> getFunc, Func<T> getFuncLocal, TimeSpan? duration, bool sliding = false)
        {

            if (Exists(key))
                return Get<T>(key);

            var value = default(T);
            if (getFunc == null)
                return value;

            try
            {
                //如果是缓存失效之后，第一次调用正常的数据源
                bool IsFirst = false;

                var locker = Lockers.GetOrAdd(key, new object());

                lock (locker)
                {
                    if (Exists(key))
                        return Get<T>(key);

                    IsFirst = true;

                    //先从内存中获取上一次的有效值，防止正常获取方式时间过长，导致线程阻塞
                    if (DicMemory.ContainsKey(key))
                    {
                        value = (T)DicMemory[key];
                        Set(key, value, duration, sliding);
                    }
                    else
                    { //内存中没有的时候，从正常数据源获取，如果正常数据源失败，直接从持久化数据源获取

                        value = getFunc();

                        //从正常数据源获取失败
                        if (value == null || value.Equals(default(T)))
                        {
                            if (getFuncLocal != null)
                            {
                                //从持久化数据源获取
                                value = getFuncLocal();
                            }
                        }

                        Set(key, value, duration, sliding);
                        DicMemory[key] = value;
                        IsFirst = false;
                    }
                }

                if (IsFirst)
                {
                    value = getFunc();
                    Set(key, value, duration, sliding);
                    //更新持久化内存中的数据
                    DicMemory[key] = value;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("LocalCache.GetOrSet", ex);
            }

            return value;
        }


        /// <summary>
        /// 加入缓存
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="duration">缓存时间</param>
        /// <param name="sliding">是否sliding模式，如果是，在缓存时间内如果有访问，则缓存不会消失</param>
        public void Set<T>(string key, T value, TimeSpan? duration, bool sliding = false)
        {
            SetWithPriority(key, value, duration, CacheItemPriority.Default, sliding);
        }
        /// <summary>
        /// 加入缓存
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="duration">缓存时间</param>
        /// <param name="priority">缓存是否可被移除</param>
        /// <param name="isSliding">是否sliding模式，如果是，在缓存时间内如果有访问，则缓存不会消失</param>
        public void SetWithPriority<T>(string key, T value, TimeSpan? duration, CacheItemPriority priority, bool isSliding = false)
        {
            RawSet(key, value, duration, isSliding, priority);
        }

        private void RawSet(string cacheKey, object value, TimeSpan? duration, bool isSliding, CacheItemPriority priority)
        {
            var policy = new CacheItemPolicy { Priority = priority };
            if (!isSliding && duration.HasValue)
                policy.AbsoluteExpiration = DateTime.Now.Add(duration.Value);
            if (isSliding && duration.HasValue)
                policy.SlidingExpiration = duration.Value;

            Cache.Add(cacheKey, value, policy);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        public void Remove(string key) => Cache.Remove(key);


    }

    public static class ExtensionMethods
    {
        public static TimeSpan Seconds(this int seconds) => TimeSpan.FromSeconds(seconds);
        public static TimeSpan Minutes(this int minutes) => TimeSpan.FromMinutes(minutes);
        public static TimeSpan Hours(this int hours) => TimeSpan.FromHours(hours);
        public static TimeSpan Days(this int days) => TimeSpan.FromDays(days);
    }
}