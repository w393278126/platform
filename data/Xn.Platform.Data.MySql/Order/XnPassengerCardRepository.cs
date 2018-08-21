using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core;
using Xn.Platform.Domain.Order;
using Dapper;

namespace Xn.Platform.Data.MySql.Order
{
    public class XnPassengerCardRepository : AbstractRepository<XnPassengerCardModel>
    {
        public XnPassengerCardRepository()
        {
            ConnectionString = ConfigSetting.ConnectionMySqlSportsEntities;
            SlaveConnectionString = ConfigSetting.ConnectionMySqlSportsEntitiesReadOnly;
        }
        /// <summary>
        /// 查询出行人证件信息
        /// </summary>
        /// <param name="passengerId">出行人ID</param>
        /// <param name="type">证件类型：1.身份证  2.护照</param>
        /// <returns></returns>
        public XnPassengerCardModel GetCardEntity(int passengerId,int type)
        {
            var parameter = new List<Tuple<string, string, object>>();
            parameter.Add(new Tuple<string, string, object>("PassengerId", "=", passengerId));
            parameter.Add(new Tuple<string, string, object>("CardType", "=", type));
            return GetList<XnPassengerCardModel>(parameter).FirstOrDefault();
        }
    }
}
