using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using Xn.Platform.Core;
using Xn.Platform.Core.Extensions;
using Xn.Platform.Data.Redis.Login;

namespace Xn.Platform.Infrastructure.Auth
{
    public static class XnAuthentication
    {
        static string _validationKey = ConfigSetting.ValidationKey;
        static string _encryptionKey = ConfigSetting.EncryptionKey;

        static readonly PluAuthenticationHandler PluAuthenticationHandler = new PluAuthenticationHandler();

        static long _lastUpdate = DateTime.MinValue.Ticks;

        static void UpdateKey()
        {
            Interlocked.Exchange(ref _lastUpdate, DateTime.Now.Ticks);
            var keys = PluAuthenticationHandler.RangeByRank(null,0, 1);
            if (keys.Length == 2)
            {
                Interlocked.Exchange(ref _validationKey, keys[0]);
                Interlocked.Exchange(ref _encryptionKey, keys[1]);
            }
        }

        public static string UnprotectData(string encryptKey, string validateKey, string encrypted)
        {
            // 32 iv + 16 hash, at lest 48 
            if (encrypted.Length < 48)
            {
                return null;
            }
            var sIv = encrypted.Substring(0, 32);
            var cypher = encrypted.Substring(32, encrypted.Length - 48);
            var hash = encrypted.Substring(encrypted.Length - 16);
            if (Validation(validateKey, sIv, cypher, hash))
            {
                return DecryptRj128(cypher, encryptKey, sIv);
            }
            return null;

        }


        public static string ProtectData(string encryptKey, string validateKey, string clearData, string ivStr = null)
        {
            using (var rj = new RijndaelManaged())
            {
                rj.Padding = PaddingMode.PKCS7;
                rj.Mode = CipherMode.CBC;
                rj.KeySize = 256;// 128;
                rj.BlockSize = 128;
                rj.Key = CryptoExtensions.HexToBinary(encryptKey);
                byte[] iv;
                if (ivStr == null)
                {
                    rj.GenerateIV();
                    iv = rj.IV;
                }
                else
                {
                    iv = CryptoExtensions.HexToBinary(ivStr);
                    rj.IV = iv;
                }
                var data = Encoding.UTF8.GetBytes(clearData);

                using (var memoryStream = new MemoryStream())
                {
                    //先将向量写入加密流头部
                    memoryStream.Write(iv, 0, iv.Length);
                    using (var encryptor = rj.CreateEncryptor())
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        //写入加密字节流
                        cryptoStream.Write(data, 0, data.Length);
                        cryptoStream.FlushFinalBlock();

                        //计算当前字节流的Hash值，并写入流末端
                        using (KeyedHashAlgorithm validationAlgorithm
                            = new HMACSHA256(CryptoExtensions.HexToBinary(validateKey)))
                        {
                            var hash = validationAlgorithm.ComputeHash(
                                memoryStream.GetBuffer(), 0, checked((int)memoryStream.Length));
                            memoryStream.Write(hash, 0, 8);
                            return CryptoExtensions.BinaryToHex(memoryStream.ToArray());
                        }
                    }
                }
            }
        }

        static bool Validation(string validateKey, string iv, string cypher, string hash)
        {
            var needToHash = CryptoExtensions.HexToBinary(iv + cypher);
            if (needToHash == null) { return false; }
            var key = CryptoExtensions.HexToBinary(validateKey);
            if (key == null) { return false; }
            var hashData = CryptoExtensions.HexToBinary(hash);
            if (hashData == null) { return false; }
            using (var keyedValidationAlgorithm = new HMACSHA256(key))
            {
                var computed = keyedValidationAlgorithm.ComputeHash(needToHash);
                return CryptoExtensions.BuffersAreEqual(hashData, 0, hashData.Length, computed, 0, hashData.Length);
            }
        }

        static string DecryptRj128(string cypher, string keyString, string ivString)
        {
            var key = CryptoExtensions.HexToBinary(keyString);
            var iv = CryptoExtensions.HexToBinary(ivString);
            var data = CryptoExtensions.HexToBinary(cypher);
            using (var rj = new RijndaelManaged())
            {
                rj.Padding = PaddingMode.PKCS7;
                rj.Mode = CipherMode.CBC;
                rj.KeySize = 256;// 128;
                rj.BlockSize = 128;
                rj.Key = key;
                rj.IV = iv;
                using (var ms = new MemoryStream(data))
                using (var cs = new CryptoStream(ms, rj.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public static string GetAuthCookie(string cookie)
        {
            if (string.IsNullOrEmpty(cookie))
            {
                return null;
            }


            //先用新key解密，如成功，则登录成功
            string username = UnprotectData(_encryptionKey, _validationKey, cookie);
            if (!string.IsNullOrEmpty(username))
            {
                var splitUserName = username.Split('|');
                if (splitUserName.Length > 1)
                {
                    var encryptionUid = splitUserName[0].AsInt();
                    if (encryptionUid <= 0)
                    {
                        SetAuthCookieFalse();
                        return null;
                    }
                    return encryptionUid.ToString();
                }
                return username;
            }
            //如果本地key已经超过一小时未更新，则可能解不成功是因为login的key已更新
            //因此需要从中央存key的地方获取新key，并把更新时间设成当前时间
            //否则，说明用户的cookie为伪造或者太古老的cookie，则返回空
            var updateTimespan = DateTime.Now - new DateTime(_lastUpdate);
            if (updateTimespan.TotalMinutes > 5)
            {
                UpdateKey();
                return GetAuthCookie(cookie);
            }
            SetAuthCookieFalse();
            return null;
        }


        public static void SetAuthCookie(string username)
        {
            var cookieValue = ProtectData(_encryptionKey, _validationKey, username);
            var cookie = new HttpCookie(ConfigSetting.AuthCookieName, cookieValue)
            {
                HttpOnly = true,
                Shareable = false
            };
            var host = HttpContext.Current.Request.Url.Host;
            var domainParts = host.Split('.');
            if (domainParts.Length > 2)
            {
                var domain = domainParts[domainParts.Length - 2] + "." + domainParts[domainParts.Length - 1];
                cookie.Domain = domain;
            }
            HttpContext.Current.Response.SetCookie(cookie);
        }

        public static void SetTestAuthCookie(string username,string domain)
        {
            var cookieValue = ProtectData(_encryptionKey, _validationKey, username);
            var cookie = new HttpCookie(ConfigSetting.AuthCookieName, cookieValue)
            {
                HttpOnly = true,
                Shareable = false
            };
            cookie.Domain = domain;
            HttpContext.Current.Response.SetCookie(cookie);
        }


        /// <summary>
        /// 设置用户cookie为-1
        /// </summary>
        public static void SetAuthCookieFalse()
        {
            var cookieValue = "-1";
            var cookie = new HttpCookie(ConfigSetting.AuthCookieName, cookieValue)
            {
                HttpOnly = true,
                Shareable = false
            };
            var host = HttpContext.Current.Request.Url.Host;
            var domainParts = host.Split('.');
            if (domainParts.Length > 2)
            {
                var domain = domainParts[domainParts.Length - 2] + "." + domainParts[domainParts.Length - 1];
                cookie.Domain = domain;
            }
            HttpContext.Current.Response.SetCookie(cookie);
        }

        public static string GetAuthCookieValue()
        {
            var authCookieValue = "";
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(ConfigSetting.AuthCookieName);
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                authCookieValue = cookie.Value;
            }
            return authCookieValue;
        }
    }
}