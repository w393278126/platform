using Plu.Platform.Domain.TourUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Domain;

namespace Xn.Platform.Data.MySql.TourUser
{
    public class TourUsersRepository : AbstractRepository<TourUserModel>
    {
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TourUserModel GetInfo(int Id)
        {
            var parameter = new List<Tuple<string, string, object>>();
            parameter.Add(new Tuple<string, string, object>("Id", "=", Id));
            var info = GetList<TourUserModel>(0, 1, "id desc", parameter).FirstOrDefault();
            return info;
        }
        public PagedEntity<TourUserModel> PageList(int pageIndex,int pageSize)
        {
            var parameter=new List<Tuple<string, string, object>>();
            var entity = GetPagedEntity<TourUserModel>(pageIndex, pageSize, "Id",parameter);
            return entity;
        }
    }
}
