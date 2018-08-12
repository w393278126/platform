using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core.Data;

namespace Xn.Platform.Domain.Sports
{
    /// <summary>
    /// 配置体育APP首页活动入口
    /// </summary>
    [Table("AppHomePageActivityEntryConfig")]
    public class AppHomePageActivityEntryConfig : Entity
    {
        public AppHomePageActivityEntryConfig()
        {
            ShowPostion = ImgPostion.None;
            CreateTime = DateTime.Now;
            UpdateTime = CreateTime;
            ShareButton = ShareButtonEnum.No;
        }

        /// <summary>
        /// 入口类型（-1=无，0=单入口，1=双入口）
        /// </summary>
        public ImgPostion ShowPostion { get; set; }

        /// <summary>
        /// 平台类型（0=Android，1=iOS）
        /// </summary>
        public PlatformTypeEnum PlatformType { get; set; }

        /// <summary>
        /// 左边半图链接
        /// </summary>
        public string LeftHalfImg { get; set; }

        /// <summary>
        /// 左边半图点击后跳转链接
        /// </summary>
        public string LeftHalfImgHrefLink { get; set; }

        /// <summary>
        /// 右边半图链接
        /// </summary>
        public string RightHalfImg { get; set; }

        /// <summary>
        /// 右边半图点击后跳转链接
        /// </summary>
        public string RightHalfImgHrefLink { get; set; }

        /// <summary>
        /// 全屏图链接
        /// </summary>
        public string FullImg { get; set; }

        /// <summary>
        /// 全屏图点击后跳转链接
        /// </summary>
        public string FullImgHrefLink { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 操作人UserId
        /// </summary>
        public int OperatorUserId { get; set; }

        /// <summary>
        /// H5标题
        /// </summary>
        public string H5Title { get; set; }

        /// <summary>
        /// 分享按钮
        /// </summary>
        public ShareButtonEnum ShareButton { get; set; }
        /// <summary>
        /// H5标题-右侧
        /// </summary>
        public string H5Title2 { get; set; }

        /// <summary>
        /// 分享按钮-右侧
        /// </summary>
        public ShareButtonEnum ShareButton2 { get; set; }



        public static List<AppHomePageActivityEntryConfig> CreateBase()
        {
            return new List<AppHomePageActivityEntryConfig>()
                {
                    new AppHomePageActivityEntryConfig(){
                        Id=0,
                        PlatformType= PlatformTypeEnum.Android
                    },
                    new AppHomePageActivityEntryConfig(){
                        Id=0,
                        PlatformType= PlatformTypeEnum.iOS
                    }
                };
        }
    }

    public enum PlatformTypeEnum
    {
        Android = 0,
        iOS = 1
    }

    public enum ImgPostion
    {
        None = -1,
        Single = 0,
        Double = 1,
    }

    public enum ShareButtonEnum
    {
        No = 0,
        Yes = 1
    }
}
