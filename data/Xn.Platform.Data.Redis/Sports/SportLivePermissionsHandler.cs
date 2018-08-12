using Xn.Platform.Abstractions.Redis.RedisCluster;
using Xn.Platform.Core.Extensions;
using Xn.Platform.Domain.Sports;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Xn.Platform.Data.Redis.Sports
{
    public class SportLivePermissionsHandler : RedisHash
    {

        public SportLivePermissionsHandler() : base(RedisKeyDefinition.SportLivePermissions)
        {
        }

        /// <summary>
        /// 针对赛事判断指定渠道是否有直播权限
        /// </summary>
        /// <param name="matchId">赛事</param>
        /// <param name="channel">直播渠道</param>
        /// <returns></returns>
        public bool IsLivePermissions(int matchId, ChannelPermissionsEnum channel)
        {
            var channels = Values(matchId.ToString());
            //如果针对该场赛事没有任何权限设置  则默认全部具备权限
            if (channels == null || channels.Length <= 0)
                return true;
            foreach (var value in channels)
            {
                //权限 或位运算  
                if ((int.Parse(value) & (int)channel) > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 针对赛事判断指定房间是否有直播权限
        /// </summary>
        /// <param name="matchId">赛事</param>
        /// <param name="roomId">房间ID</param>
        /// <returns></returns>
        public bool IsLivePermissions(int matchId, int roomId)
        {
            var keycount = Length(matchId.ToString());
            //如果针对该场赛事没有任何权限设置  则默认全部具备权限
            if (keycount <= 0)
                return true;
            var roomchannels = Exists(matchId.ToString(), roomId.ToString());
            ////roomId=0 代表这个比赛的全部房间
            var allroomchannels = Exists(matchId.ToString(), "0");
            //个人房间权限 或者 全部房间权限 有一个满足即可 
            return roomchannels|| allroomchannels;
        }

        /// <summary>
        /// 针对赛事判断指定房间指定渠道是否有直播权限
        /// </summary>
        /// <param name="matchId">赛事</param>
        /// <param name="roomId">房间ID</param>
        /// <param name="channel">直播渠道</param>
        /// <returns></returns>
        public bool IsLivePermissions(int matchId, int roomId, ChannelPermissionsEnum channel)
        {
            var keycount = Length(matchId.ToString());
            //如果针对该场赛事没有任何权限设置  则默认全部具备权限
            if (keycount <= 0)
                return true;
            var roomchannels = Get(matchId.ToString(), roomId.ToString());
            //roomId=0 代表这个比赛的全部房间
            var allroomchannels = Get(matchId.ToString(), "0");
            //个人房间权限 或 全部房间权限 有一个包含指定渠道即可
            if (!string.IsNullOrEmpty(roomchannels) && !string.IsNullOrEmpty(allroomchannels))
            {
                return ((int.Parse(roomchannels) | int.Parse(allroomchannels)) & (int)channel) > 0;
            }
            else if (!string.IsNullOrEmpty(roomchannels))
            {
                return (int.Parse(roomchannels) & (int)channel) > 0;
            }
            if (!string.IsNullOrEmpty(allroomchannels))
            {
                return (allroomchannels.AsInt() & (int)channel) > 0;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 针对赛事获取最大的权限值
        /// </summary>
        /// <param name="matchId">赛事ID</param>
        /// <returns></returns>
        public int GetMaxPermissions(int matchId)
        {
            var keycount = Length(matchId.ToString());
            //如果针对该场赛事没有任何权限设置  则默认全部具备权限
            if (keycount <= 0)
            {
                return GetMaxChannels();
            }
            var values = Values(matchId.ToString());
            int result = 0;
            foreach (var value in values)
            {
                result = result | int.Parse(value);
            }
            return result;
        }

        /// <summary>
        /// 针对赛事获取指定房间的最大权限值
        /// </summary>
        /// <param name="matchId">赛事ID</param>
        /// <param name="roomId">房间ID</param>
        /// <returns></returns>
        public int GetMaxPermissions(int matchId, int roomId)
        {
            var keycount = Length(matchId.ToString());
            //如果针对该场赛事没有任何权限设置  则默认全部具备权限
            if (keycount <= 0)
            {
                return GetMaxChannels();
            }
            var roomchannels = Get(matchId.ToString(), roomId.ToString());
            var allroomchannels = Get(matchId.ToString(), "0");
            //个人房间权限 和 全部房间权限 获取权限集合
            if (!string.IsNullOrEmpty(roomchannels) && !string.IsNullOrEmpty(allroomchannels))
            {
                return int.Parse(roomchannels) | int.Parse(allroomchannels);
            }
            else if (!string.IsNullOrEmpty(allroomchannels))
            {
                return int.Parse(allroomchannels);
            }
            if (!string.IsNullOrEmpty(roomchannels))
            {
                return roomchannels.AsInt();
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 计算最大的渠道权限值
        /// </summary>
        /// <returns></returns>
        private int GetMaxChannels()
        {
            var values= Enum.GetValues(typeof(ChannelPermissionsEnum));
            int result = 0;
            foreach (int item in values)
            {
                result= result | item;
            }
            return result;
        }


    }
}
