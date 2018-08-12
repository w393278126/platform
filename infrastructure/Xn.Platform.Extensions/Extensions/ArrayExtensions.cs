using Xn.Platform.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Xn.Platform.Extensions.Extensions
{
    public static class ArrayExtensions
    {
        private static Random random = new Random(Environment.TickCount);

        /// <summary>
        /// 将一个大数组切割成多个最大不超过blockSize的小数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">原数组</param>
        /// <param name="blockSize">小数组最大长度</param>
        /// <returns></returns>
        public static IEnumerable<T[]> ToBlocks<T>(this T[] values, int blockSize = 100)
        {
            if (values == null)
            {
                yield break;
            }

            var index = 0;

            while (true)
            {
                var block = values.Skip(index).Take(blockSize).ToArray();
                if (block.Length > 0)
                {
                    index += blockSize;
                    yield return block;
                }
                else
                {
                    yield break;
                }
            }
        }

        public static IEnumerable<List<T>> ToBlocks<T>(this List<T> values, int blockSize = 100)
        {
            if (values == null)
            {
                yield break;
            }

            var index = 0;

            while (true)
            {
                var block = values.Skip(index).Take(blockSize).ToList();
                if (block.Count > 0)
                {
                    index += blockSize;
                    yield return block;
                }
                else
                {
                    yield break;
                }
            }
        }

        public static IEnumerable<T[]> ToBlocks<T>(this IEnumerable<T> values, int blockSize = 100)
        {
            if (values == null)
            {
                yield break;
            }

            var block = new List<T>(blockSize);
            foreach (var value in values)
            {
                block.Add(value);
                if (block.Count == blockSize)
                {
                    yield return block.ToArray();
                    block.Clear();
                }
            }
            if (block.Count > 0)
            {
                yield return block.ToArray();
            }
        }

        public static string ToHexString(this byte[] values)
        {
            return values.Select(b => b.ToString("X2")).Join("");
        }

        /// <summary>
        /// 洗牌算法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                int k = random.Next(n--);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
