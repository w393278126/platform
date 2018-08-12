using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Xn.Platform.Core.Extensions
{
    public static class RegexExtensions
    {
        static Regex regUri = new Regex(@"[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*(?=[^\.\,\)\(\s]?)", RegexOptions.Compiled);
        /// <summary>
        /// http://unicode-table.com/cn/#specials
        /// </summary>
        static Regex regMsg = new Regex(@"[^ -~\u4e00-\u9fa5\uFF00-\uFF65\u3001-\u3011\u2018-\u201d\uAC00-\uD7AF]", RegexOptions.Compiled);
        static Regex httpString = new Regex(@"(http|https|ftp)\://", RegexOptions.Compiled);
        static Regex JudgeNumber = new Regex(@"^[A-Za-z0-9]+$", RegexOptions.IgnoreCase);
        static Regex JudgeNumberOnly = new Regex(@"^[0-9]+$", RegexOptions.IgnoreCase);

        /// <summary>
        /// 是否包含特殊字符
        /// </summary>
        public static bool IsContainIllegalChar(this string value)
        {
            return regMsg.IsMatch(value);
        }

        /// <summary>
        /// 是否是数字和字母
        /// </summary>
        public static bool IsJudgeNumberLetter(this string str)
        {
            return JudgeNumber.Match(str).Success;
        }

        /// <summary>
        /// 是否是数字
        /// </summary>
        public static bool IsJudgeNumber(this string str)
        {
            return JudgeNumberOnly.Match(str).Success;
        }

        public static string GetFirstString(this string stringToSub, int length)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            char[] stringChar = stringToSub.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int nLength = 0;
            for (int i = 0; i < stringChar.Length; i++)
            {
                if (regex.IsMatch((stringChar[i]).ToString()))
                {
                    nLength += 2;
                }
                else
                {
                    nLength = nLength + 1;
                }

                if (nLength <= length)
                {
                    sb.Append(stringChar[i]);
                }
                else
                {
                    break;
                }
            }
            return sb.ToString();
        }

        public static string GetCapitalChar(string input, out ShieldType shieldType)
        {
            long iCnChar;
            shieldType = ShieldType.Chinese;

            byte[] ZW = System.Text.Encoding.Default.GetBytes(input);

            //如果是字母，则直接返回 
            if (ZW.Length == 1)
            {
                Regex r = new Regex(@"^[A-Za-z]{1}$");
                if (r.IsMatch(input))
                {
                    shieldType = ShieldType.English;
                    return input.ToUpper();
                }
                else
                {
                    shieldType = ShieldType.Other;
                    return "0";
                }
            }
            else
            {
                int i1 = (short)(ZW[0]);
                int i2 = (short)(ZW[1]);
                iCnChar = i1 * 256 + i2;
            }

            //expresstion 
            //table of the constant list 
            // 'A'; //45217..45252 
            // 'B'; //45253..45760 
            // 'C'; //45761..46317 
            // 'D'; //46318..46825 
            // 'E'; //46826..47009 
            // 'F'; //47010..47296 
            // 'G'; //47297..47613 
            // 'H'; //47614..48118 
            // 'J'; //48119..49061 
            // 'K'; //49062..49323 
            // 'L'; //49324..49895 
            // 'M'; //49896..50370 
            // 'N'; //50371..50613 
            // 'O'; //50614..50621 
            // 'P'; //50622..50905 
            // 'Q'; //50906..51386 
            // 'R'; //51387..51445 
            // 'S'; //51446..52217 
            // 'T'; //52218..52697 
            //没有U,V 
            // 'W'; //52698..52979 
            // 'X'; //52980..53640 
            // 'Y'; //53689..54480 
            // 'Z'; //54481..55289 

            // iCnChar match the constant 
            if ((iCnChar >= 45217) && (iCnChar <= 45252))
            {
                return "A";
            }
            else if ((iCnChar >= 45253) && (iCnChar <= 45760))
            {
                return "B";
            }
            else if ((iCnChar >= 45761) && (iCnChar <= 46317))
            {
                return "C";
            }
            else if ((iCnChar >= 46318) && (iCnChar <= 46825))
            {
                return "D";
            }
            else if ((iCnChar >= 46826) && (iCnChar <= 47009))
            {
                return "E";
            }
            else if ((iCnChar >= 47010) && (iCnChar <= 47296))
            {
                return "F";
            }
            else if ((iCnChar >= 47297) && (iCnChar <= 47613))
            {
                return "G";
            }
            else if ((iCnChar >= 47614) && (iCnChar <= 48118))
            {
                return "H";
            }
            else if ((iCnChar >= 48119) && (iCnChar <= 49061))
            {
                return "J";
            }
            else if ((iCnChar >= 49062) && (iCnChar <= 49323))
            {
                return "K";
            }
            else if ((iCnChar >= 49324) && (iCnChar <= 49895))
            {
                return "L";
            }
            else if ((iCnChar >= 49896) && (iCnChar <= 50370))
            {
                return "M";
            }

            else if ((iCnChar >= 50371) && (iCnChar <= 50613))
            {
                return "N";
            }
            else if ((iCnChar >= 50614) && (iCnChar <= 50621))
            {
                return "O";
            }
            else if ((iCnChar >= 50622) && (iCnChar <= 50905))
            {
                return "P";
            }
            else if ((iCnChar >= 50906) && (iCnChar <= 51386))
            {
                return "Q";
            }
            else if ((iCnChar >= 51387) && (iCnChar <= 51445))
            {
                return "R";
            }
            else if ((iCnChar >= 51446) && (iCnChar <= 52217))
            {
                return "S";
            }
            else if ((iCnChar >= 52218) && (iCnChar <= 52697))
            {
                return "T";
            }
            else if ((iCnChar >= 52698) && (iCnChar <= 52979))
            {
                return "W";
            }
            else if ((iCnChar >= 52980) && (iCnChar <= 53640))
            {
                return "X";
            }
            else if ((iCnChar >= 53689) && (iCnChar <= 54480))
            {
                return "Y";
            }
            else if ((iCnChar >= 54481) && (iCnChar <= 55289))
            {
                return "Z";
            }
            else return ("?");
        }


        /// <summary>
        /// 返回首字母
        /// </summary>
        public static string GetCapitalChar(string input)
        {
            ShieldType s = ShieldType.Chinese;
            return GetCapitalChar(input, out s);
        }

        /// <summary>
        /// 过滤SQL非法字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetSafeSQL(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            value = Regex.Replace(value, @";", string.Empty);
            value = Regex.Replace(value, @"'", string.Empty);
            value = Regex.Replace(value, @"&", string.Empty);
            value = Regex.Replace(value, @"%20", string.Empty);
            value = Regex.Replace(value, @"--", string.Empty);
            value = Regex.Replace(value, @"==", string.Empty);
            value = Regex.Replace(value, @"<", string.Empty);
            value = Regex.Replace(value, @">", string.Empty);
            value = Regex.Replace(value, @"%", string.Empty);
            value = Regex.Replace(value, "\"", string.Empty);
            value = Regex.Replace(value, @"\\", string.Empty);
            return value;
        }

        public static string GetSafeString(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            value = GetSafeSQL(value);
            value = value.Replace("\r\n", "&nbsp;");
            value = value.Replace("\n", "<br/>");
            value = value.Replace("\r\n", "<br/>");
            value = value.Replace("\n", "<br/>");
            return value;
        }

        /// <summary>
        /// 格式化赛事房间日期
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static long ConvertMatchRoomDate(this string title)
        {
            DateTime dt = new DateTime();
            Regex dateRegex = new Regex(@"(?<month>\d+)月(?<day>\d+)日", RegexOptions.IgnoreCase);
            var match_date = dateRegex.Match(title);
            if (match_date.Success)
            {
                var month = Convert.ToInt32(match_date.Groups["month"].Value);
                var day = Convert.ToInt32(match_date.Groups["day"].Value);
                dt = new DateTime(DateTime.Now.Year, month, day);
                return dt.Date.GetUnixTimeStamp();
            }
            else
            {
                return 0;
            }
        }

        public static string TransformUri(this string content)
        {
            content = httpString.Replace(content, string.Empty);
            return regUri.Replace(content, m => string.Format("<a href=\"http://{0}\" rel=\"nofollow\" target=\"_blank\">{0}</a>", m.Value));
        }
    }
}
