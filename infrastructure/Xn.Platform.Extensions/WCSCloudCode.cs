namespace Xn.Platform.Core
{
    public class WCSCloudCode
    {
        /// <summary>
        /// 是否匹配
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="userId"></param>
        /// <param name="sign">加密串</param>
        /// <returns></returns>
        public static bool IsMatch(string domain, int userId, string sign)
        {
            string originalToken = EncryptHelper.GetMD5((domain + "$%" + ConfigSetting.WCSCloudCode + "**ujj" + userId.ToString()).ToLower());
            return originalToken == sign;
        }

        /// <summary>
        /// 产生网宿加密串
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string ProduceCode(string domain, int userId)
        {
            return EncryptHelper.GetMD5((domain + "$%" + ConfigSetting.WCSCloudCode + "**ujj" + userId.ToString()).ToLower());
        }

    }
}
