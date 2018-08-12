using System.Linq;
using System.Text.RegularExpressions;

namespace Xn.Platform.Core.Extensions
{
    public static class IpExtenstions
    {
        static Regex regIp = new Regex(@"\d+\.\d+\.\d+\.\d+", RegexOptions.Compiled);
        const string ipFormatter = "{0}.{1}.{2}.{3}";

        public static uint IpToInt(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                return 0;
            }
            if (ip.IndexOf(',') > 0)
            {
                ip = ip.Split(',')[0].Trim();
            }
            if (regIp.IsMatch(ip))
            {
                byte[] parts = ip.Split('.').Select(s => s.AsByte()).ToArray();
                uint result = 0;
                for (int i = 0; i < parts.Length; i++)
                {
                    result |= (uint)(parts[i] << (8 * i));
                }
                return result;

            }
            return 0;
        }

        public static string IntToIp(uint ip)
        {
            uint byte1 = ip & 0x000000ff;
            uint byte2 = (ip & 0x0000ff00) >> 8;
            uint byte3 = (ip & 0x00ff0000) >> 16;
            uint byte4 = (ip & 0xff000000) >> 24;
            return string.Format(ipFormatter, byte1, byte2, byte3, byte4);
        }
    }
}
