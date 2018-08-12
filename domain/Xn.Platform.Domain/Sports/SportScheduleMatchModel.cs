using System;
using System.Collections.Generic;

namespace Xn.Platform.Domain.Sports
{
    public class SportScheduleMatchModel : SportSchedule
    {
        public string CreateAuthorName { set; get; }
        public string LastAuthorName { set; get; }
        public List<SportMatch> Matchs { set; get; }
    }

    public class SportScheduleMatchModelV2 : SportSchedulesV2
    {
        public SportScheduleMatchModelV2()
        {
            AllowForceDel = false;
        }

        public string CreateAuthorName { set; get; }
        public string LastAuthorName { set; get; }
        public List<SportMatchV2> Matchs { set; get; }

        /// <summary>
        /// 管理后台强制删除标记
        /// </summary>
        public bool AllowForceDel { get; set; }
    }

    public class SportRecommendRoomsModelV2
    {
        public List<RecommendRooms> RecommendRooms { get; set; }
        public int MatchId { get; set; }
        public int MatchConfigId { get; set; }
        public int ScheduleId { get; set; }
    }

    public class SportClubModel : SportClub
    {
        public string ProfessionalBarPic { get; set; }
        public string ProfessionalChatPic { get; set; }
        public string EntertainmentBarPic { get; set; }
        public string EntertainmentChatPic { get; set; }
        public new FightTypeEnum FightType { get; set; }
        public int BetClubId { get; set; }
    }

    public class SportClubListModel : SportClub
    {
        public string ProfessionalDomain { get; set; }
        public string EntertaimentDomain { get; set; }
        public string ProfessionalRoomName { get; set; }
        public string EntertaimentRoomName { get; set; }

    }

    public class SportMatchsListModel : SportMatch
    {
        public string HomeTeamName { get; set; }
        public string HomeTeamLogo { get; set; }
        public string VisitingTeamName { get; set; }
        public string VisitingTeamLogo { get; set; }
        public string MatchConfigName { get; set; }
    }

    public class SportMatchsListModelV2 : SportMatchV2
    {
        public SportMatchsListModelV2()
        {
            RoundName = string.Empty;
            TeamALogo = string.Empty;
            TeamBLogo = string.Empty;
            LeagueName = string.Empty;
            SubscribeBtnStatus = SubscribeButtonStyle.None;
        }
        public new bool CanByLottery { get; set; }

        public string RoundName { get; set; }
        public string TeamALogo { get; set; }
        public string TeamBLogo { get; set; }
        public RecommendRoom TopRecommend { get; set; }
        /// <summary>
        /// 房间RoomId
        /// </summary>
        public int LiveRoomId { get; set; }
        /// <summary>
        /// 赛事id
        /// </summary>
        public int MatchId { get; set; }
        /// <summary>
        /// 赛事名
        /// </summary>
        public string LeagueName { get; set; }
        public int LeagueId { get; set; }
        public List<RecommendRoom> RecommendRooms { get; set; }
        /// <summary>
        /// 开始直播的日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 预约信息
        /// </summary>
        public List<SubscribeInfo> Subscribe { get; set; }
        public bool IsSubscribe { get; set; }

        /// <summary>
        /// 预约按钮状态（0=预约、1=进入房间）
        /// </summary>
        public SubscribeButtonStyle SubscribeBtnStatus { get; set; }
    }

    public enum SubscribeButtonStyle
    {
        /// <summary>
        /// 无
        /// </summary>
        None = -1,
        /// <summary>
        /// 预约
        /// </summary>
        Subscribe = 0,
        /// <summary>
        /// 进入房间
        /// </summary>
        Entrance = 1,
        /// <summary>
        /// 已结束有回放
        /// </summary>
        EndThenHasReplayVideo = 2,
        /// <summary>
        /// 已结束无回放
        /// </summary>
        EndThenNoReplayVideo = 3,
    }
    /// <summary>
    /// 推荐房间明细
    /// </summary>
    public class RecommendRooms
    {
        public RecommendRooms()
        {
            this.IsTopRecommendRooms = false;
            this.IsOfficialRoom = false;
            this.Slogan = string.Empty;
            RoomName = string.Empty;
            PlayTitle = string.Empty;
            Domain = string.Empty;
            RoomLogo = string.Empty;
            RoomId = string.Empty;
            PrivateRoom = new PrivateRoom();
            ReplayVideoId = 0;
        }
        /// <summary>
        /// 房间域名
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// 房间ID
        /// </summary>
        public string RoomId { get; set; }
        /// <summary>
        /// 房间logo
        /// </summary>
        public string RoomLogo { get; set; }
        /// <summary>
        /// 房间名称
        /// </summary>
        public string RoomName { get; set; }
        /// <summary>
        /// 房主Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 在暴露到接口层，需要判断房间是否直播，如果直播则获取在线数，否则返回0
        /// </summary>
        public int OnlineCount { get; set; }
        /// <summary>
        /// 峰值在线人数
        /// </summary>
        public int OnlineMaxCount { get; set; }
        /// <summary>
        /// 在暴露到接口层，需要判断房间是否直播
        /// </summary>
        public string PlayTitle { get; set; }

        private string _slogan;
        /// <summary>
        /// Slogan
        /// </summary>
        public string Slogan
        {
            get
            {
                if (null == _slogan)
                {
                    return "";
                }
                else
                {
                    return _slogan;
                }
            }
            set { _slogan = value; }
        }

        /// <summary>
        /// 在暴露到接口层，需要判断房间是否直播,直播获取直播游戏id，未直播则取签约游戏id
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// 是否是直播
        /// </summary>
        public bool IsLive { get; set; }
        /// <summary>
        /// 排序ID，越大越靠前
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 是否是固定推荐（false = 动态推荐）
        /// </summary>
        public bool IsTopRecommendRooms { get; set; }
        /// <summary>
        /// 是否是官方房间推流
        /// </summary>
        public bool IsOfficialRoom { get; set; }
        /// <summary>
        /// 赛事ID
        /// </summary>
        public int MatchId { get; set; }
        /// <summary>
        /// 封面或者截图
        /// </summary>
        public string Preview { get; set; }
        /// <summary>
        /// 私有房间类型
        /// </summary>
        public PrivateRoom PrivateRoom { get; set; }
        /// <summary>
        /// 回放视频Id
        /// </summary>
        public int ReplayVideoId { get; set; }
        /// <summary>
        /// 首次开播时间
        /// </summary>
        public string StartLiveTime { get; set; }
    }

    public class PrivateRoom
    {
        public PrivateRoom()
        {
            RoomPerm = RoomPermEnum.Permission;
            RoomType = RoomTypeEnum.None;
        }

        public RoomPermEnum RoomPerm { get; set; }
        public RoomTypeEnum RoomType { get; set; }
    }

    public enum RoomPermEnum
    {
        /// <summary>
        /// 无权限访问
        /// </summary>
        UnPermission = 0,
        /// <summary>
        /// 有权限访问
        /// </summary>
        Permission = 1
    }

    public enum RoomTypeEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        None = 0,
        /// <summary>
        /// 加密
        /// </summary>
        Lock = 1,
        /// <summary>
        /// 收费
        /// </summary>
        Charge = 2
    }

    public class RecommendRoomsEditerSet
    {
        public RecommendRooms OfficialRecommendRooms { get; set; }
        public RecommendRooms TopRecommendRooms { get; set; }
        public List<RecommendRooms> DynamicRecommendRooms { get; set; }

        private List<RecommendRooms> _recommendRooms;
        public List<RecommendRooms> RecommendRooms
        {
            get
            {
                var result = new List<RecommendRooms>();
                if (null != OfficialRecommendRooms) { result.Add(OfficialRecommendRooms); }
                if (null != TopRecommendRooms) { result.Add(TopRecommendRooms); }
                if (null != DynamicRecommendRooms) { result.AddRange(DynamicRecommendRooms); }
                return result;
            }
            set { _recommendRooms = value; }
        }

        public int MatchId { get; set; }
        public int MatchConfigId { get; set; }
        public int ScheduleId { get; set; }
        /// <summary>
        /// 赛事的预约信息
        /// </summary>
        public List<SubscribeInfo> SubscribeInfos { get; set; }
        /// <summary>
        /// 已结束且有回放视频的房间
        /// </summary>
        public List<RecommendRooms> MatchOverRooms { get; set; }
    }

    public class MatchRelateRoom
    {
        public string RoomName { get; set; }
        public string RoomLogo { get; set; }
        public string LiveTitle { get; set; }
        public string Domain { get; set; }
        public int RoomId { get; set; }
        public int GameId { get; set; }
        /// <summary>
        /// 本场贡献
        /// </summary>
        public int LiveContribution { get; set; }
        public int OnlineCount { get; set; }

        /// <summary>
        /// 热力值
        /// </summary>
        public double HeatValue { get; set; }

        /// <summary>
        /// 主播前端认证
        /// </summary>
        public string TagInfo { get; set; }
        /// <summary>
        /// 截图
        /// </summary>
        public string Preview { get; set; }
        public string Slogan { get; set; }
    }

    public class JumpTemplate
    {
        public string Template { get; set; }
    }

    public class MatchAwesomeRoom
    {
        public string RoomName { get; set; }
        public string RoomLogo { get; set; }
        public string LiveTitle { get; set; }
        public string RoundName { get; set; }
        public string Domain { get; set; }
        public int RoomId { get; set; }
        public int GameId { get; set; }
        public int MatchId { get; set; }
        public string TeamAName { get; set; }
        public string TeamALogo { get; set; }
        public string TeamBName { get; set; }
        public string TeamBLogo { get; set; }
        public int OnlineCount { get; set; }

        /// <summary>
        /// 热力值
        /// </summary>
        public double HeatValue { get; set; }

        /// <summary>
        /// 主播前端认证
        /// </summary>
        public string TagInfo { get; set; }
        /// <summary>
        /// 截图
        /// </summary>
        public string Preview { get; set; }
        public string Slogan { get; set; }

    }

    public class MatchDateInfo
    {
        public MatchDateInfo()
        {
            MatchCount = 0;
        }

        public string Date { get; set; }
        public int MatchCount { get; set; }
    }
}