using Newtonsoft.Json;
using Xn.Platform.Abstractions.Domain;
using Xn.Platform.Core.Data;
using Xn.Platform.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain.Sports
{
    /*
     * DTO 代表进行传输的数据对象
     * DO  代表映射数据表（mysql、redis）的数据对象
     */
    /// <summary>
    /// 体育VIP记录明细
    /// </summary>
    [Table("SportVipInfos")]
    public class SportVipInfoDO
    {
        [Key]
        public int Id { get; set; }
        public int UserId { set; get; }
        public int Type { set; get; }
        public DateTime ExpireTime { set; get; }
        public DateTime CreateTime { set; get; }
        public DateTime UpdateTime { set; get; }

        /// <summary>
        /// VIP剩余天数，用进一法，0.1天算1天
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public int RemainDays()
        {
            return UserSportVipSimpleInfo.RemainDays(ExpireTime.ToTimestamp());
        }
    }

    /// <summary>
    /// 体育VIP道具实体（后续可根据字典表进行数据映射）
    /// </summary>
    public class SportVipDetailDO
    {
        /// <summary>
        /// 购买Vip天数
        /// </summary>
        public SportVipOptionEnum Option { get; set; }
        /// <summary>
        /// 购买Vip价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 购买Vip类型
        /// </summary>
        public SportVipEnum Type { get; set; }
        /// <summary>
        /// 道具类型标识
        /// </summary>
        public string ItemType { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        public int Sort { get; set; }
    }

    /// <summary>
    /// 购买体育VIP的入参
    /// </summary>
    public class SportVipBuyDTO : SportVipDetailDO
    {
        public SportVipBuyDTO()
        {
            this.NeedBalance = true;
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 是否收费
        /// </summary>
        public bool NeedBalance { get; set; }
    }

    /// <summary>
    /// 购买体育VIP的结果
    /// </summary>
    public class SportVipBuyResultDTO : UserSportVipSimpleInfo
    {}

    /// <summary>
    /// 体育VIP的基本信息（核心）
    /// </summary>
    [DataContract]
    public class UserSportVipSimpleInfo
    {
        /// <summary>
        /// vip类型
        /// </summary>
        [DataMember(Name = "type")]
        public int Type { set; get; }

        /// <summary>
        /// 到期时间 utc时间戳 
        /// </summary>
        [DataMember(Name = "expire")]
        public long Expire { set; get; }

        /// <summary>
        /// Vip的排序值
        /// </summary>
        [DataMember(Name = "sort")]
        public int Sort { get; set; }

        /// <summary>
        /// VIP剩余天数
        /// </summary>
        [IgnoreDataMember]
        public int RemainingDays => RemainDays(Expire);
        /// <summary>
        /// VIP剩余天数，用进一法，0.1天算1天
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static int RemainDays(long timestamp)
        {
            var now = DateTime.Now.ToTimestamp();
            return timestamp <= now ? 0 : (int)Math.Ceiling((double)(timestamp - now) / 86400);
        }
    }

    /// <summary>
    /// 体育VIP类型
    /// </summary>
    public enum SportVipEnum
    {
        Sport = 1
    }

    /// <summary>
    /// 体育VIP时间档次
    /// </summary>
    public enum SportVipOptionEnum
    {
        day30 = 30,
        day100 = 100,
        day210 = 210,
        day450 = 450
    }

    /// <summary>
    /// 体育会员静态类
    /// </summary>
    public static class SportVipConstant
    {
        /// <summary>
        /// 体育VIP会员体系结构
        /// </summary>
        public static readonly List<SportVipDetailDO> sportVipList = new List<SportVipDetailDO>()
        {
            new SportVipDetailDO(){Option=SportVipOptionEnum.day30,Price=2m,Type=SportVipEnum.Sport,Sort=1 },
            new SportVipDetailDO(){Option=SportVipOptionEnum.day100,Price=6m,Type=SportVipEnum.Sport,Sort=1 },
            new SportVipDetailDO(){Option=SportVipOptionEnum.day210,Price=12m,Type=SportVipEnum.Sport,Sort=1 },
            new SportVipDetailDO(){Option=SportVipOptionEnum.day450,Price=24m,Type=SportVipEnum.Sport,Sort=1 },
        };

        /// <summary>
        /// 获取VIP的排序值
        /// </summary>
        /// <param name="VipEnum"></param>
        /// <returns></returns>
        public static int GetSort(SportVipEnum VipEnum)
        {
            var vip = sportVipList.First(o => o.Type == VipEnum);
            return vip == null ? 0 : vip.Sort;
        }
        /// <summary>
        /// 获取VIP的排序值
        /// </summary>
        /// <param name="VipEnum"></param>
        /// <returns></returns>
        public static int GetSort(int VipEnum)
        {
            int result = 0;
            if (Enum.IsDefined(typeof(SportVipEnum), VipEnum))
            {
                var vip = sportVipList.First(o => o.Type == (SportVipEnum)VipEnum);
                result = vip == null ? 0 : vip.Sort;
            }
            return result;
        }
    }
}
