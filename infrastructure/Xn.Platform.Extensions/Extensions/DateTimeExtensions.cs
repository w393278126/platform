using System;
using System.Collections.Generic;
using System.Globalization;

namespace Xn.Platform.Core.Extensions
{
    public class Appointment
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public static class DateTimeExtensions
    {
        /// <summary>
        /// UTC时间戳起点
        /// </summary>
        public static readonly DateTime UTCZero = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        static readonly DateTime unix_time_zero = new DateTime(1970, 1, 1);

        public static DateTime FromTodayGetSundayDay(this DateTime day)
        {
            switch (day.DayOfWeek)
            {
                case DayOfWeek.Monday: return day.AddDays(-1);
                case DayOfWeek.Tuesday: return day.AddDays(-2);
                case DayOfWeek.Wednesday: return day.AddDays(-3);
                case DayOfWeek.Thursday: return day.AddDays(-4);
                case DayOfWeek.Friday: return day.AddDays(-5);
                case DayOfWeek.Saturday: return day.AddDays(-6);
                case DayOfWeek.Sunday: return day.AddDays(-7);
            }
            return day;
        }

        public static List<Appointment> SplitAppointment(Appointment appt)
        {
            TimeSpan oneDay = TimeSpan.FromDays(1);
            TimeSpan endOfDay = oneDay; // last possible time of a day
            List<Appointment> result = new List<Appointment>();
            DateTime currentDay = appt.StartDate;
            DateTime endOfCurrentDay = currentDay.Date + endOfDay;
            while (endOfCurrentDay < appt.EndDate)
            {
                result.Add(new Appointment() { StartDate = currentDay, EndDate = endOfCurrentDay });
                // copy other attributes of appt
                currentDay = currentDay.Date + oneDay;   // EDIT: changed this line
                endOfCurrentDay += oneDay;
            }
            if (currentDay < appt.EndDate)
            {
                result.Add(new Appointment() { StartDate = currentDay, EndDate = appt.EndDate });
                // copy other attributes of appt
            }
            return result;
        }

        ///   <summary>    
        ///   计算某日起始日期（礼拜一的日期）    
        ///   </summary>    
        ///   <param name="someDate">该周中任意一天</param>    
        ///   <returns>返回礼拜一日期，后面的具体时、分、秒和传入值相等</returns>    
        public static DateTime GetMondayDate(this DateTime someDate)
        {
            int i = someDate.DayOfWeek - DayOfWeek.Monday;
            if (i == -1) i = 6;//   i值   >   =   0   ，因为枚举原因，Sunday排在最前，此时Sunday-Monday=-1，必须+7=6。    
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return someDate.Subtract(ts);
        }

        /// <summary>
        /// 时间转换为时间戳,秒
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime time)
        {
            return (long)(time.ToUniversalTime() - UTCZero).TotalSeconds;
        }
        /// <summary>
        /// 时间转换为时间戳，毫秒
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ToTimestampMilliseconds(this DateTime time)
        {
            return (long)(time.ToUniversalTime() - UTCZero).TotalMilliseconds;
        }
        /// <summary>
        /// 时间戳转换为UTC时间
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ToUTCTime(this long timestamp)
        {
            return UTCZero.AddSeconds(timestamp);
        }

        /// <summary>
        /// 时间戳转换为本地时间
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ToLocalTime(this long timestamp)
        {
            return ToUTCTime(timestamp).ToLocalTime();
        }

        public static TimeSpan GetUnixTimeSpan(DateTime utcTime)
        {
            return utcTime - unix_time_zero;
        }

        public static TimeSpan GetUnixTimeSpanFromNow()
        {
            return GetUnixTimeSpan(DateTime.UtcNow);
        }

        public static DateTime UnixTimeStampToDateTime(this long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            try
            {
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            }
            catch { }
            return dtDateTime;
        }

        public static DateTime MillisecondsToDateTime(this long milliseconds)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            try
            {
                dtDateTime = dtDateTime.AddMilliseconds(milliseconds).ToLocalTime();
            }
            catch { }
            return dtDateTime;
        }

        /// <summary>
        /// 距离1970/1/1的秒数，UTC时间
        /// </summary>
        /// <param name="utcTime"></param>
        /// <returns></returns>
        public static long GetUnixTimeStamp(this DateTime utcTime)
        {
            return (long)GetUnixTimeSpan(utcTime).TotalSeconds;
        }

        /// <summary>
        /// 获取本地时间utc时间戳
        /// </summary>
        /// <param name="localTime"></param>
        /// <returns></returns>
        public static long GetLocalTimeUnixTimeStamp(this DateTime localTime)
        {
            return (long)GetUnixTimeSpan(localTime.ToUniversalTime()).TotalSeconds;
        }

        /// <summary>
        /// 距离1970/1/1的毫秒数，UTC时间
        /// </summary>
        /// <param name="utcTime"></param>
        /// <returns></returns>
        public static long GetUnixTimeMillisecondsStamp(this DateTime utcTime)
        {
            return (long)GetUnixTimeSpan(utcTime).TotalMilliseconds;
        }

        /// <summary>
        /// 当前距离1970/1/1的秒数，UTC时间
        /// </summary>
        public static long GetUnixTimeStampFromNow()
        {
            return (long)GetUnixTimeSpanFromNow().TotalSeconds;
        }

        static DateTime year2013 = new DateTime(2013, 1, 1);
        public static long GetTicketsFrom2013(this DateTime time)
        {
            return (time - year2013).Ticks;
        }

        public static double GetTotalMinutesFrom2013(this DateTime time)
        {
            return Math.Floor((time - year2013).TotalMinutes);
        }

        public static DateTime GetDateTimeFrom2013TotalMinutes(this double totalMinutes)
        {
            return year2013.AddMinutes(totalMinutes);
        }

        /// <summary>
        /// 获得该时间从2013-1-1开始算起的秒数，如果是2013前的时间，返回0
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static uint GetSecondsFrom2013(this DateTime time)
        {
            var span = (time - year2013).TotalSeconds;
            if (span > 0)
            {
                return (uint)span;
            }
            return 0;
        }

        /// <summary>
        /// 格式化显示时间为几个月,几天前,几小时前,几分钟前,或几秒前
        /// 页面使用
        /// </summary>
        public static string DateStringFromNow(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalDays > 365)
            {
                return dt.ToShortDateString();
            }
            else if (span.TotalDays > 30)
            {
                return String.Format("{0}个月前", (int)Math.Floor(span.TotalDays / 30));
            }
            else if (span.TotalDays > 14)
            {
                return "2周前";
            }
            else if (span.TotalDays > 7)
            {
                return "1周前";
            }
            else if (span.TotalDays > 1)
            {
                return String.Format("{0}天前", (int)Math.Floor(span.TotalDays));
            }
            else if (span.TotalHours > 1)
            {
                return String.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
            }
            else if (span.TotalMinutes > 1)
            {
                return String.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
            }
            else if (span.TotalSeconds >= 1)
            {
                return String.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
            }
            else
            {
                return "1秒前";
            }
        }

        public static int GetIso8601WeekOfYear(this DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }
            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// 获取本月的第一天00:00:00
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static DateTime GetCurrentMonth(this DateTime day)
        {
            return new DateTime(day.Year, day.Month, 1);
        }

        public static bool IsMonthLastDay(this DateTime day)
        {
            return day.Date == new DateTime(day.Year, day.Month, 1).AddMonths(1).AddDays(-1);
        }

        public static DateTime StampToDateTime(string timeStamp)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dateTimeStart.Add(toNow);
        }
        public static string RsetDateTimeStr(string dateTime)
        {
            const string defalut1 = "1901/1/1 0:00:00";
            const string defalut2 = "0001/1/1 0:00:00";
            if (dateTime == defalut1 || dateTime == defalut2)
            {
                return string.Empty;
            }
            else
            {
                return dateTime;
            }

        }

        // <summary>
        /// 计算本周起始日期（礼拜一的日期）
        /// </summary>
        /// <param name="someDate">该周中任意一天</param>
        /// <returns>返回礼拜一日期，后面的具体时、分、秒和传入值相等</returns>
        public static DateTime CalculateFirstDateOfWeek(DateTime someDate)
        {
            int i = someDate.DayOfWeek - DayOfWeek.Monday;
            if (i == -1) i = 6;// i值 > = 0 ，因为枚举原因，Sunday排在最前，此时Sunday-Monday=-1，必须+7=6。
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return someDate.Subtract(ts);
        }

        /// <summary>
        /// 计算本周结束日期（礼拜日的日期）
        /// </summary>
        /// <param name="someDate">该周中任意一天</param>
        /// <returns>返回礼拜日日期，后面的具体时、分、秒和传入值相等</returns>
        public static DateTime CalculateLastDateOfWeek(DateTime someDate)
        {
            int i = someDate.DayOfWeek - DayOfWeek.Sunday;
            if (i != 0) i = 7 - i;// 因为枚举原因，Sunday排在最前，相减间隔要被7减。
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return someDate.Add(ts);
        }

        /// <summary>
        /// 判断选择的日期是否是本周（根据系统当前时间决定的‘本周'比较而言）
        /// </summary>
        /// <param name="someDate"></param>
        /// <returns></returns>
        public static bool IsThisWeek(DateTime someDate)
        {
            //得到someDate对应的周一
            DateTime someMon = CalculateFirstDateOfWeek(someDate);
            //得到本周一
            DateTime nowMon = CalculateFirstDateOfWeek(DateTime.Now);
            TimeSpan ts = someMon - nowMon;
            if (ts.Days < 0)
                ts = -ts;//取正
            if (ts.Days >= 7)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        ///获取当前日期在一周里的第几天，sunday默认为0，转换为7
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static string GetDayOfWeek(this DateTime now)
        {
            return now.DayOfWeek == DayOfWeek.Sunday ? "7" : now.DayOfWeek.ToString("D");
        }

        /// <summary>
        /// 时间拼接时分秒 时间2017-06-27 00:00:00 转换后变成2017-06-27 23：59：59
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime AppendDateHMS(this DateTime now)
        {
            return now.AddHours(23).AddMinutes(59).AddSeconds(59);
        }
    }
}
