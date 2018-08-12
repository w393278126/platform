using System.ComponentModel;

namespace Xn.Platform.Abstractions.Domain
{
    /// <summary>
    /// 直播推流CDN服务商类型枚举
    /// </summary>
    public enum LiveStreamType
    {
        /// <summary>
        /// 0未设置
        /// </summary>
        [Description("未设置")]
        NotSet,
        /// <summary>
        /// 1随心播
        /// </summary>
        [Description("随心播")]
        Tga,
        /// <summary>
        /// 2外站直播
        /// </summary>
        [Description("外站直播")]
        Outside,
        /// <summary>
        /// 9网宿
        /// </summary>
        [Description("网宿")]
        WS = 9,
        /// <summary>
        /// 10七牛App
        /// </summary>
        [Description("七牛")]
        Qiniu = 10,
        /// <summary>
        /// 11金山云
        /// </summary>
        [Description("金山")]
        Ksyun = 11,
        /// <summary>
        /// 12腾讯云
        /// </summary>
        [Description("腾讯")]
        Qcloud = 12,
        /// <summary>
        /// 13星域CDN
        /// </summary>
        [Description("星域")]
        XY = 13,
        /// <summary>
        /// 15阿里
        /// </summary>
        [Description("阿里")]
        Ali = 15,
        /// <summary>
        /// 17 pptv
        /// </summary>
        [Description("pptv")]
        PPTV = 17,
        /// <summary>
        /// 18七牛
        /// </summary>
        [Description("七牛")]
        QiniuWeb = 18,
        /// <summary>
        /// 19云帆
        /// </summary>
        [Description("云帆")]
        Yunfan = 19,
        /// <summary>
        /// 20百度
        /// </summary>
        [Description("百度")]
        Baidu = 20
    }
}