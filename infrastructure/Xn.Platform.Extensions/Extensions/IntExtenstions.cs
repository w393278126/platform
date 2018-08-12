using System;
using System.Collections.Generic;
using System.Linq;

namespace Xn.Platform.Core.Extensions
{
    public static class IntExtenstions
    {
        private static readonly Random Rng = new Random(Environment.TickCount);

        public static int GetMax(this int a, int b)
        {
            return a > b ? a : b;
        }

        public static int GetRandomBySliding(this int a, int sliding)
        {
            return Rng.Next(a - sliding, a + sliding);
        }

        /// <summary>
        /// 转换访问数 页面使用
        /// </summary>
        public static string FormatViewCount(this int viewCount)
        {
            string ret = viewCount.ToString();

            if (viewCount > 100000)
            {
                var thousand = "";
                var res = (viewCount % 10000);
                res = Convert.ToInt32(res / 1000);
                if (res > 0)
                {
                    thousand = "." + res;
                }
                viewCount = Convert.ToInt32(viewCount / 10000);
                ret = viewCount + thousand + "万";
            }

            return ret;
        }

        /// <summary>
        /// 转换播放时长 页面使用
        /// </summary>>
        public static string FormatTimeSpan(this int timeSpan)
        {
            var str = "";
            var minStr = "";
            var secStr = "";

            var mins = Convert.ToInt32(timeSpan / 60);
            minStr = mins.ToString();
            if (mins < 10)
            {
                minStr = "0" + mins;
            }

            var secs = Convert.ToInt32((timeSpan % 3600) % 60);
            secStr = secs.ToString();
            if (secs < 10)
            {
                secStr = "0" + secs;
            }

            str = minStr + ":" + secStr;

            return str;
        }

        /// <summary>
        /// 获取5星评分的显示数组 页面使用
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int[] GetGradeStar(this decimal num)
        {
            var count = (int)num / 2;
            var lastStar = num % 2 != 0;
            var star = new int[5];
            for (var i = 0; i < star.Length; i++)
            {
                if (num - 2 >= 0)
                {
                    num -= 2;
                    star[i] = 1;
                }
                else if (num - 2 > -2)
                {
                    num -= 2;
                    star[i] = 2;
                }
                else
                {
                    star[i] = 0;
                }
            }
            return star;
        }

        /// <summary>
        /// 掷骰子获取随机数
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static double DiceRandom(double[] num)
        {
            if (num.Length == 1)
            {
                return num[0];
            }
            List<double> points = new List<double>();
            for (int i = 0; i < num.Length; i++)
            {
                for (int j = 0; j < num[i]; j++)
                {
                    points.Add(num[i]);
                }
            }
            double sum = num.Sum();
            int r = Math.Abs(Guid.NewGuid().GetHashCode()) % points.Count;
            double p = points[r];
            return p;
        }
    }
}