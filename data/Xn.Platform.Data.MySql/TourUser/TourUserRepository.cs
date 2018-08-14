using Plu.Platform.Domain.TourUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core;
using Xn.Platform.Domain;

namespace Xn.Platform.Data.MySql.TourUser
{
    public class TourUsersRepository : AbstractRepository<TourUserModel>
    {
        public TourUsersRepository()
        {
            ConnectionString = ConfigSetting.ConnectionMySqlSportsEntities;
            SlaveConnectionString = ConfigSetting.ConnectionMySqlSportsEntitiesReadOnly;
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TourUserModel GetInfo(string Id)
        {
            var parameter = new List<Tuple<string, string, object>>();
            parameter.Add(new Tuple<string, string, object>("Id", "=", Id));
            var info = GetList<TourUserModel>(0, 1, "id desc", parameter).FirstOrDefault();
            return info;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagedEntity<TourUserModel> PageList(TourUserRequest.PageResult request)
        {
            var parameter = new List<Tuple<string, string, object>>();
            parameter.Add(new Tuple<string, string, object>("status", "=", 1));
            if (request.username != null)
                parameter.Add(new Tuple<string, string, object>("username", "like", string.Concat("%", request.username + "%")));
            if (request.ToSort)
                request.OrderBy += " desc";

            var entity = GetPagedEntity<TourUserModel>(request.PageIndex > 0 ? request.PageIndex - 1 : request.PageIndex, request.PageSize, request.OrderBy, parameter);
            return entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TourUserModel AddOrUpdate(TourUserModel entity)
        {
            // var parameter = new List<Tuple<string, string, object>>();
            return MakePersistent(entity);
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int Delete(string Id)
        {
            var parameter = new List<Tuple<string, string, object>>();
            parameter.Add(new Tuple<string, string, object>("Id", "=", Id));
            return DeleteAnyStatus(parameter, true);
        }
    }
}
