using Xn.Platform.Core;
using Xn.Platform.Core.Extensions;
using  Xn.Platform.Infrastructure.Auth;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;

namespace Xn.Platform.Domain.Impl
{
    public static class HttpContextBaseExtensions
    {
        /// <summary>
        /// webapi转mvc
        /// </summary>
        public static HttpContextBase GetHttpContextBase(this HttpRequestMessage request)
        {
            return (HttpContextBase)request.Properties["MS_HttpContext"];//获取传统context
        }

        #region login url
        public static string GetLogoutUrl(this HttpContextBase ctx)
        {
            return "http://login.Xn.cn/user/logout?returnurl=" + ctx.Server.UrlEncode(ctx.Request.Url.AbsoluteUri);
        }

        public static string GetLogoutUrl(this HttpContext ctx)
        {
            return GetLogoutUrl(new HttpContextWrapper(ctx));
        }

        public static string GetLoginUrl(this HttpContextBase ctx)
        {
            return "http://login.Xn.cn/qq/login?returnurl=" + ctx.Server.UrlEncode(ctx.Request.Url.AbsoluteUri);
        }

        public static string GetLoginUrl(this HttpContext ctx)
        {
            return GetLoginUrl(new HttpContextWrapper(ctx));
        }

        #endregion

        #region client ip
        /// <summary>
        /// When a client IP can't be determined
        /// </summary>
        public const string UnknownIP = "0.0.0.0";

        private static readonly Regex IPv4Regex = new Regex(@"\b([0-9]{1,3}\.){3}[0-9]{1,3}$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// returns true if this is a private network IP  
        /// http://en.wikipedia.org/wiki/Private_network
        /// </summary>
        public static bool IsPrivateIP(string s)
        {
            return (s.StartsWith("192.168.") || s.StartsWith("10.") || s.StartsWith("127.0.0."));
        }
        /// <summary>
        /// retrieves the IP address of the current request -- handles proxies and private networks
        /// </summary>
        public static string GetRemoteIP(this NameValueCollection serverVariables)
        {
            var ip = serverVariables["REMOTE_ADDR"]; // could be a proxy -- beware
            var ipForwarded = serverVariables["HTTP_X_FORWARDED_FOR"];

            // check if we were forwarded from a proxy
            if (!string.IsNullOrEmpty(ipForwarded))
            {
                ipForwarded = IPv4Regex.Match(ipForwarded).Value;
                if (!string.IsNullOrEmpty(ipForwarded) && !IsPrivateIP(ipForwarded))
                    ip = ipForwarded;
            }

            return !string.IsNullOrEmpty(ip) ? ip : UnknownIP;
        }

        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP(this HttpContextBase ctx)
        {
            string ip = string.Empty;
            if (ctx.Request.ServerVariables["HTTP_X_Rea1_IP"] != null)
            {
                ip = ctx.Request.ServerVariables["HTTP_X_Rea1_IP"].Split(',')[0].Trim();
            }
            else if (ctx.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                ip = ctx.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',')[0].Trim();
            }
            else
            {
                ip = ctx.Request.UserHostAddress;
            }
            return ip;
        }


        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientIPAddress()
        {
            string clientIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(clientIP))
            {
                clientIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }


            if (!string.IsNullOrEmpty(clientIP))
                return clientIP;
            else
                return "127.0.0.1";
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        public static string GetClientIP(this HttpContext ctx)
        {
            return GetClientIP(new HttpContextWrapper(ctx));
        }

        public static string GetClientIP(this HttpRequestBase hrb)
        {
            return GetClientIP(hrb.RequestContext.HttpContext);
        }

        #endregion

        #region user id

        /// <summary>
        /// 如果为正，则为登录用户，如果为-n，则为游客，
        /// 但是这个-n是不准的，准确的请GetCurrentGuestId()，
        /// 如果为0，则有问题！
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static int GetCurrentUserId(this HttpContextBase ctx)
        {
            int userId = GetLoggedUserId(ctx.User);
            if (userId == 0)
            {
                userId = (int)GetCurrentGuestId(ctx);
                if (userId > 0)
                {
                    userId *= -1;
                }
            }
            return userId;
        }

        public static int GetCurrentUserId(this HttpRequestBase hrb)
        {
            return GetCurrentUserId(hrb.RequestContext.HttpContext);
        }

        static int GetLoggedUserId(IPrincipal user)
        {
            int userId = 0;
            if (user.Identity.IsAuthenticated)
            {
                int.TryParse(user.Identity.Name, out userId);
            }
            return userId;
        }

        #endregion

        #region guest id

        const string guest_cookie_name = "pluguest";
        const string validationKey = "6F4EF067E6867A2ECED32FE5B577D278";
        const string encryptionKey = "38275588D0A30E781FA8E3ED5C7B45DB3996A71E08CCC777998E78D25530ED0F";
        const string guestIdFormatter = "-8{0:d10}{1:d4}";
        static readonly Regex RegexGuestId = new Regex(@"^\-8\d{14}$", RegexOptions.Compiled);
        //static Random Rnd = new Random();
        static int _rnd = 0;
        static int GetRnd()
        {
            if (_rnd >= 10000)
                _rnd = 0;
            _rnd++;
            return _rnd;
        }

        public static long GetCurrentGuestId(this HttpContextBase ctx)
        {
            bool needRecookie = false;

            long guestId = -1;
            string guestValue = "-1";
            var cookieValue = ctx.Request.Cookies[guest_cookie_name];
            if (!string.IsNullOrEmpty(cookieValue?.Value))
            {
                if (RegexGuestId.IsMatch(cookieValue.Value) && !ConfigSetting.ProtectGuestId)
                {
                    guestValue = cookieValue.Value;
                    //needRecookie = ProtectGuestId;
                }
                else if (cookieValue.Value.Length > 40)
                {
                    var unprotected = XnAuthentication.UnprotectData(encryptionKey, validationKey, cookieValue.Value);
                    if (unprotected != null && RegexGuestId.IsMatch(unprotected))
                    {
                        guestValue = unprotected;
                        needRecookie = !ConfigSetting.ProtectGuestId;
                    }
                }
            }

            long.TryParse(guestValue, out guestId);

            if (guestId > -2)
            {
                //userId = GuestHandler.GenerateGuestId();
                var ipByInt = IpExtenstions.IpToInt(ctx.GetClientIP());
                guestValue = String.Format(guestIdFormatter, ipByInt, GetRnd());
                guestId = long.Parse(guestValue);
                needRecookie = true;
            }
            if (needRecookie)
            {
                var newCookieValue = ConfigSetting.ProtectGuestId ? XnAuthentication.ProtectData(encryptionKey, validationKey, guestValue) : guestValue;
                var cookie = new HttpCookie(guest_cookie_name, newCookieValue);
                var requestDomain = ctx.Request.Url?.Host.Split('.');
                if (requestDomain?.Length > 2)
                {
                    cookie.Domain = requestDomain[requestDomain.Length - 2] + "."
                        + requestDomain[requestDomain.Length - 1];
                }
                else
                {
#if DEBUG
                    cookie.Domain = "Xn.dev";
#else
                    cookie.Domain = "Xn.cn";
#endif
                }
                cookie.Shareable = false;
                cookie.Expires = DateTime.Now.AddYears(5);
                ctx.Response.SetCookie(cookie);
            }
            return guestId;
        }
        
        #endregion
        

        public static Guid UpdateCorrelationId(this HttpContextBase context)
        {
            Guid guid;
            if (!context.Items.Contains("X-CorrelationId"))
            {
                guid = Trace.CorrelationManager.ActivityId;
                if (guid == Guid.Empty)
                    guid = Guid.NewGuid();
                context.Items.Add("X-CorrelationId", guid);
            }
            else
            {
                guid = (Guid)context.Items["X-CorrelationId"];
                Trace.CorrelationManager.ActivityId = guid;
            }
            return guid;
        }
    }
}