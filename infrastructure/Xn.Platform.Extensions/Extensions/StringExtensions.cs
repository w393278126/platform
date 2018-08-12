using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Xn.Platform.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ConvertEncodeBase64URLSafe(this string data)
        {
            return data.Replace("=", String.Empty).Replace('+', '-').Replace('/', '_');
        }
        public static string ConvertDecodeBase64URLSafe(this string data)
        {
            data = data.Replace('-', '+').Replace('_', '/');
            int len = data.Length % 4;
            if (len > 0)
            {
                data += "====".Substring(0, 4 - len);
            }
            return data;
        }

        public static IList<int> ToNumberList(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return new int[0];
            return str.Split(',').Select(s => s.AsInt()).ToList();
        }

        public static List<long> ToLongList(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return new List<long>();
            return str.Split(',').Select(s => s.AsLong()).ToList();
        }

        public static IList<DateTime> ToDateTimeList(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return new DateTime[0];
            }

            return str.Split(',').Select(item => item.AsDateTime()).ToList();
        }

        /// <summary>
        /// https://stackoverflow.com/questions/1540625/c-sharp-equivalent-to-javas-digestutils-md5hexstring
        /// DigestUtils.md5Hex 对应的asp.net版本
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5Hex(this string str)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x =
    new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(str);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            str = s.ToString();
            return str;
        }

        public static ISet<string> ToStringSet(this string str)
        {
            var entities = new HashSet<string>();
            if (string.IsNullOrEmpty(str))
                return entities;
            var split = str.Split(',');
            foreach (var item in split)
            {
                entities.Add(item);
            }
            return entities;
        }

        /// <summary>
        /// 格式 xxx:1,2,3|xxx:1,2,3
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static IDictionary<string, ISet<int>> ToIntSet(this string str)
        {
            var entities = new Dictionary<string, ISet<int>>();
            if (string.IsNullOrEmpty(str))
                return entities;
            var split = str.Split('|');
            foreach (var item in split)
            {
                var pre = item.Split(':');
                var value = pre[1].Split(',');
                var values = new HashSet<int>();
                foreach (var i in value)
                {
                    values.Add(item.AsInt());
                }
                entities[pre[0]] = values;
            }
            return entities;
        }

        /// <summary>
        /// 转义字符串为有效json 2014-09-02
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string StringToJson(this string s)
        {
            var ret = s.Replace(@"\", @"\\");
            return ret;
        }

        /// <summary>
        /// 替换非法sql字符
        /// </summary>
        public static string Replace_SqlText(this string inputtxt)
        {
            if (string.IsNullOrEmpty(inputtxt))
            {
                return "";
            }
            string ret = string.Empty;
            ret = inputtxt;
            ret = ret.Replace("<", " ");
            ret = ret.Replace(">", " ");

            return ret;
        }

        public static List<string> ToListByMoreSplit(this string ids)
        {
            if (string.IsNullOrEmpty(ids))
                return new List<string>();
            var idList = ids.Split(new[] { ",", "，", "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            return idList;
        }

        public static string GetUpdateDatePlan(this string updateDatePlan)
        {
            string ret = "";
            if (updateDatePlan == "0")
            {
                ret = "不定期";
            }
            else
            {
                if (!string.IsNullOrEmpty(updateDatePlan))
                {
                    var splitStr1 = updateDatePlan.Split('-');
                    if (splitStr1.Length == 2)
                    {
                        if (splitStr1[0] == "1")
                        {
                            ret = "每周" + GetWeekend(Convert.ToInt32(splitStr1[1]));
                        }
                        else if (splitStr1[0] == "2")
                        {
                            if (splitStr1[1] == "29")
                            {
                                ret = "每月最后3天";
                            }
                            else if (splitStr1[1] == "30")
                            {
                                ret = "每月最后2天";
                            }
                            else if (splitStr1[1] == "31")
                            {
                                ret = "每月最后1天";
                            }
                            else
                            {
                                ret = "每月" + splitStr1[1] + "号";
                            }
                        }
                    }
                }
            }

            return ret;
        }

        private static string GetWeekend(int weekDay)
        {
            string ret = "";
            switch (weekDay)
            {
                case 1:
                    ret = "星期一";
                    break;
                case 2:
                    ret = "星期二";
                    break;
                case 3:
                    ret = "星期三";
                    break;
                case 4:
                    ret = "星期四";
                    break;
                case 5:
                    ret = "星期五";
                    break;
                case 6:
                    ret = "星期六";
                    break;
                case 7:
                    ret = "星期日";
                    break;
            }
            return ret;
        }

        /// <summary>
        /// UrlEncode
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UrlEncode(this string text)
        {
            return HttpUtility.UrlEncode(text);
        }

        /// <summary>
        /// UrlDecode
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UrlDecode(this string text)
        {
            return HttpUtility.UrlDecode(text);
        }

        /// <summary>
        /// 检测含有中文字符串的实际长度
        /// </summary>
        public static int Characterlen(this string str)
        {
            System.Text.ASCIIEncoding n = new System.Text.ASCIIEncoding();
            byte[] b = n.GetBytes(str);
            int l = 0; // l 为字符串之实际长度
            for (int i = 0; i <= b.Length - 1; i++)
            {
                if (b[i] == 63) //判断是否为汉字或全脚符号
                {
                    l++;
                }
                l++;
            }
            return l;
        }

        /// <summary>
        /// 是否是汉字
        /// </summary>
        public static bool IsChina(this string str)
        {
            bool result = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (Convert.ToInt32(Convert.ToChar(str.Substring(i, 1))) < Convert.ToInt32(Convert.ToChar(128)))
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            return result;
        }

        #region 身份证检测
        /// <summary>
        /// 身份证验证
        /// </summary>
        /// <param name="id">身份证号</param>
        /// <returns>如果身份证格式正确，返回true，否则返回false</returns>
        public static Tuple<bool, DateTime> CheckIdCard(this string id)
        {
            if (id.Length == 18)
            {
                var check = CheckIdCard18(id);
                return check;
            }
            else if (id.Length == 15)
            {
                var check = CheckIdCard15(id);
                return check;
            }
            else
            {
                return Tuple.Create(false, DateTime.MinValue);
            }
        }

        /// <summary>
        /// 18位身份证验证
        /// </summary>
        /// <param name="id">身份证号</param>
        /// <returns></returns>
        private static Tuple<bool, DateTime> CheckIdCard18(string id)
        {
            long n = 0;
            if (!long.TryParse(id.Remove(17), out n) || n < Math.Pow(10, 16) || long.TryParse(id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return Tuple.Create(false, DateTime.MinValue);//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2)) == -1)
            {
                return Tuple.Create(false, DateTime.MinValue);//省份验证
            }
            string birth = id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time;
            if (DateTime.TryParse(birth, out time) == false)
            {
                return Tuple.Create(false, DateTime.MinValue);//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != id.Substring(17, 1).ToLower())
            {
                return Tuple.Create(false, DateTime.MinValue);//校验码验证
            }
            return Tuple.Create(true, time);//符合GB11643-1999标准
        }

        /// <summary>
        /// 15位身份证验证
        /// </summary>
        /// <param name="id">身份证号</param>
        /// <returns></returns>
        private static Tuple<bool, DateTime> CheckIdCard15(string id)
        {
            long n = 0;
            if (!long.TryParse(id, out n) || n < Math.Pow(10, 14))
            {
                return Tuple.Create(false, DateTime.MinValue);//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2), StringComparison.Ordinal) == -1)
            {
                return Tuple.Create(false, DateTime.MinValue);//省份验证
            }
            var birth = id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time;
            if (!DateTime.TryParse(birth, out time))
            {
                return Tuple.Create(false, DateTime.MinValue);//生日验证
            }
            return Tuple.Create(true, time);//符合15位身份证标准
        }
        #endregion

        public static string GetString(string str, int len)
        {
            string result = string.Empty;// 最终返回的结果
            int byteLen = System.Text.Encoding.Default.GetByteCount(str);// 单字节字符长度
            int charLen = str.Length;// 把字符平等对待时的字符串长度
            int byteCount = 0;// 记录读取进度
            int pos = 0;// 记录截取位置
            if (byteLen > len)
            {
                for (int i = 0; i < charLen; i++)
                {
                    if (Convert.ToInt32(str.ToCharArray()[i]) > 255)// 按中文字符计算加2
                        byteCount += 2;
                    else// 按英文字符计算加1
                        byteCount += 1;
                    if (byteCount > len)// 超出时只记下上一个有效位置
                    {
                        pos = i;
                        break;
                    }
                    else if (byteCount == len)// 记下当前位置
                    {
                        pos = i + 1;
                        break;
                    }
                }

                if (pos >= 0)
                    result = str.Substring(0, pos);
            }
            else
                result = str;

            return result;
        }

        /// <summary>
        /// 根据文件尝试返回字符编码
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns>如果文件无法读取，返回null。否则，返回根据BOM判断的编码或者缺省编码（没有BOM）。</returns>
        public static Encoding GetEncoding(string file)
        {
            using (var stream = File.OpenRead(file))
            {
                //判断流可读？
                if (!stream.CanRead)
                    return null;
                //字节数组存储BOM
                var bom = new byte[4];
                //实际读入的长度
                int readc;

                readc = stream.Read(bom, 0, 4);

                if (readc >= 2)
                {
                    if (readc >= 4)
                    {
                        //UTF32，Big-Endian
                        if (CheckBytes(bom, 4, 0x00, 0x00, 0xFE, 0xFF))
                            return new UTF32Encoding(true, true);
                        //UTF32，Little-Endian
                        if (CheckBytes(bom, 4, 0xFF, 0xFE, 0x00, 0x00))
                            return new UTF32Encoding(false, true);
                    }
                    //UTF8
                    if (readc >= 3 && CheckBytes(bom, 3, 0xEF, 0xBB, 0xBF))
                        return new UTF8Encoding(true);

                    //UTF16，Big-Endian
                    if (CheckBytes(bom, 2, 0xFE, 0xFF))
                        return new UnicodeEncoding(true, true);
                    //UTF16，Little-Endian
                    if (CheckBytes(bom, 2, 0xFF, 0xFE))
                        return new UnicodeEncoding(false, true);
                }

                return Encoding.Unicode;
            }
        }

        //辅助函数，判断字节中的值
        static bool CheckBytes(byte[] bytes, int count, params int[] values)
        {
            for (int i = 0; i < count; i++)
                if (bytes[i] != values[i])
                    return false;
            return true;
        }

        /// <summary>
        /// 判断decimal是否有效 20150901
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsValidDecimal(this string str)
        {
            bool result = true;
            var splite = str.Split('.');
            if (splite.Length == 2)
            {
                if (splite[1].Length > 2)
                {
                    result = false;
                }
            }
            return result;
        }
            
        /// <summary>
        /// 判断string是否为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 根据分隔符拆分字符串，忽略空字段
        /// </summary>
        public static string[] Splits(this string str, char separator)
        {
            return str.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string Join(this IEnumerable<string> values, string separator)
        {
            return string.Join(separator, values);
        }

        public static string HexEncode(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            StringBuilder sb = new StringBuilder();
            foreach (var item in str)
            {
                int value = Convert.ToInt32(item);
                string hexOutput = String.Format("{0:x}", value);
                sb.Append(hexOutput);
            }
            return sb.ToString().ToLower();
        }


        public static int GetEmojiLength(this string str, int count)
        {
            if (string.IsNullOrEmpty(str))
                return count;

            Regex regImg = new Regex(@"\[em_\d{1,2}\]");
            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(str);
            return count + (matches.Count * 7);
        }


        public static string Zeroize(this int num)
        {
            if (num < 10)
            {
                return $"0{num}";
            }
            return num.ToString();
        }
    }
}