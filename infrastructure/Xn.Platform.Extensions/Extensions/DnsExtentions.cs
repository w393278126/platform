using System;
using System.Net;

namespace Xn.Platform.Core.Extensions
{
    public static class HostUtility
    {
        public static string GetLocalIP()
        {
            return _myIp.Value;
        }
        static Lazy<string> _myIp = new Lazy<string>(() => _GetIp().Trim());
        static string _GetIp()
        {
            IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
            string ipv4 = null, ipv6 = null;
            foreach (var ip in IpEntry.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipv4 = ip.ToString();
                    if (ipv4.StartsWith("192.168.1")
                        || ipv4.StartsWith("192.168.7"))
                    {
                        return ipv4;
                    }
                }
                else if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    ipv6 = ip.ToString();
                }
            }
            if (!string.IsNullOrEmpty(ipv4))
            {
                return ipv4;
            }
            if (!string.IsNullOrEmpty(ipv6))
            {
                return ipv6;
            }
            return "127.0.0.1";
        }
    }
}
