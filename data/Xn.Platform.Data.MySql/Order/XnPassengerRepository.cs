using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core;
using Xn.Platform.Domain.Order;

namespace Xn.Platform.Data.MySql.Order
{
    public class XnPassengerRepository : AbstractRepository<XnPassengerModel>
    {
        public XnPassengerRepository()
        {
            ConnectionString = ConfigSetting.ConnectionMySqlSportsEntities;
            SlaveConnectionString = ConfigSetting.ConnectionMySqlSportsEntitiesReadOnly;
        }
        /// <summary>
        /// 通过订单ID获取人员列表
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<XnPassengerDTO> GetPassengerList(int OrderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT p.*, c.CardId AS IDNumber, c1.CardId AS PassportNo  FROM xnorderpassenger op LEFT JOIN xnpassenger p ON op.PassengerId = p.Id ");
            strSql.Append(" LEFT JOIN xnpassengercard c ON op.PassengerId = c.PassengerId AND c.CardType = 1 ");
            strSql.Append(" LEFT JOIN xnpassengercard c1 ON op.PassengerId = c1.PassengerId AND c1.CardType = 2 ");
            strSql.Append(" WHERE op.OrderId = @OrderId");
            var list = new List<XnPassengerDTO>();
            OpenSlaveConnection(p =>
            {
                list = p.Query<XnPassengerDTO>(strSql.ToString(), new { OrderId }).ToList();
            });
            return list;
        }
        /// <summary>
        /// 通过主键ID获取出行人员的详细信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public XnPassengerDTO GetPassengerInfo(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT p.*,c.CardId AS IDNumber, c1.CardId AS PassportNo FROM xnpassenger p ");
            strSql.Append(" LEFT JOIN xnpassengercard c ON p.Id=c.PassengerId AND c.CardType=1 ");
            strSql.Append(" LEFT JOIN xnpassengercard c1 ON p.Id=c1.PassengerId AND c1.CardType=2 ");
            strSql.Append(" WHERE p.Id=@Id");
            var entity = new XnPassengerDTO();
            OpenSlaveConnection(conn => entity = conn.Query<XnPassengerDTO>(strSql.ToString(), new { Id }).FirstOrDefault());
            return entity;
        }
    }
}
