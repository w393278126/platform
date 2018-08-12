using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Xn.Platform.Extensions
{
    public static class NewHMACSHA1Util
    {
        /// <summary>
        /// HmacSha1 加密
        /// </summary>
        public static string HmacSha1(this string value, string keyVal, bool isLowCase = true)
        {
            if (value == null)
            {
                throw new ArgumentNullException("未将对象引用设置到对象的实例。");
            }
            var encoding = Encoding.UTF8;
            byte[] keyStr = encoding.GetBytes(keyVal);
            HMACSHA1 hmacSha1 = new HMACSHA1(keyStr);
            var encrypt = HashAlgorithmBase(hmacSha1, value, encoding);
            return isLowCase ? encrypt.ToLower() : encrypt;
        }

        /// <summary>
        /// HashAlgorithm 加密统一方法
        /// </summary>
        private static string HashAlgorithmBase(HashAlgorithm hashAlgorithmObj, string source, Encoding encoding)
        {
            byte[] btStr = encoding.GetBytes(source);
            byte[] hashStr = hashAlgorithmObj.ComputeHash(btStr);
            return hashStr.Bytes2Str();
        }

        /// <summary>
        /// 转换成字符串
        /// </summary>
        private static string Bytes2Str(this IEnumerable<byte> source, string formatStr = "{0:X2}")
        {
            StringBuilder pwd = new StringBuilder();
            foreach (byte btStr in source) { pwd.AppendFormat(formatStr, btStr); }
            return pwd.ToString();
        }

        public static string SHA1(string content)
        {
            try
            {
                byte[] StrRes = Encoding.Default.GetBytes(content);
                HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
                StrRes = iSHA.ComputeHash(StrRes);
                StringBuilder EnText = new StringBuilder();
                foreach (byte iByte in StrRes)
                {
                    EnText.AppendFormat("{0:x2}", iByte);
                }
                return EnText.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }
    }
}
