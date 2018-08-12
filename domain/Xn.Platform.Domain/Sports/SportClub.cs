using Xn.Platform.Core.Data;
using System;

namespace Xn.Platform.Domain.Sports
{
    [Table("SportClub")]
    public class SportClub : Entity
    {
        public string ClubName { get; set; }
        public int ProfessionalRoomId { get; set; }
        public int EntertainmentRoomId { get; set; }
        [ExcludeStatus]
        public int Status { get; set; }
        public string ClubSmallLogo { get; set; }
        public DateTime WriteTime { get; set; }

        /// <summary>
        /// 背景图1
        /// </summary>
        public string BackgroundImage1 { get; set; }
        /// <summary>
        /// 背景图2
        /// </summary>
        public string BackgroundImage2 { get; set; }
        /// <summary>
        /// 对阵类型（0=对抗型，1=非对抗型）
        /// </summary>
        public FightTypeEnum FightType { get; set; }
        /// <summary>
        /// 非对抗类型俱乐部的房间ID
        /// </summary>
        public int UnFightRoomId { get; set; }
    }
}
