using System;
using System.ComponentModel;
using System.Text;

namespace Xn.Platform.Domain.Admin
{
    public class VideoSheet
    {
        public VideoSheet()
        {
            AdvertisingDomain = string.Empty;
        }

        public int Id { get; set; }
        public int AdvertiseWidth { get; set; } //��ʾ���
        public string AdvertisingName { get; set; }
        public string AdvertisingPlacement { get; set; }
        public string AdvertisingDomain { get; set; }
        public DateTime AdvertisingStartTime { get; set; }
        public DateTime AdvertisingEndTime { get; set; }
        public string AdvertisingUrl { get; set; }
        public int Duration { get; set; }
        public string AdvertisingUrl2 { get; set; }
        public int Duration2 { get; set; }
        public string MonitorCode { get; set; }
        public string TargetUrlCode { get; set; }
        public string MonitorCode2 { get; set; }
        public string TargetUrlCode2 { get; set; }
        public int DomainBind { get; set; }
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public TargetType TargetType { get; set; }
        public string TargetIds { get; set; }
        public int Enable { get; set; }
        //Ԥ����ʱ��
        public int? PreloadTime { get; set; }
        //�ӳټ���ʱ��
        public int? LazyloadTime { get; set; }

    }

    public enum TargetType
    {
        [Description("δ����")]
        NotSet = 0,
        [Description("ȫ��")]
        Global = 1,
        [Description("����")]
        Room = 2,
        [Description("��ϷID")]
        Game = 3
    }

    public enum AdvertiseType
    {
        [Description("live_videosheet_s6")]
        VideoSheet = 0,
        [Description("live_google_sheet")]
        Google = 1,
        [Description("live_s6_dropdown1")]
        Dropdown1 = 2,
        [Description("live_s6_dropdown2")]
        Dropdown2 = 3,
        [Description("live_room")]
        LiveRoom = 4
    }
}
