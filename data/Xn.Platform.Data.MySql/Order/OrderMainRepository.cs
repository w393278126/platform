using Dapper;
using Xn.Platform.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core;
using Xn.Platform.Domain;

namespace Xn.Platform.Data.MySql.Order
{
    public class OrderMainRepository : AbstractRepository<OrderMainModel>
    {
        public OrderMainRepository()
        {
            ConnectionString = ConfigSetting.ConnectionMySqlSportsEntities;
            SlaveConnectionString = ConfigSetting.ConnectionMySqlSportsEntitiesReadOnly;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagedEntity<OrderMainResponse.PageResponse> PageInfo(OrderMainRequest.PageRequest request)
        {
            StringBuilder strSql = new StringBuilder();
            int pageIndex = request.PageIndex;
            request.PageIndex = (request.PageIndex - 1) * request.PageSize;
            strSql.Append(" SELECT SQL_CALC_FOUND_ROWS o.*,u.userName,u.mobile");
            strSql.Append(" FROM t_order_ordermain o ");
            strSql.Append(" LEFT JOIN t_tour_user u ON o.userID=u.id ");
            strSql.Append(" WHERE 1=1 ");
            if (!string.IsNullOrEmpty(request.userName))
                strSql.Append(" AND username like CONCAT('%',@userName,'%') ");
            if (request.orderType > 0)
                strSql.Append(" AND orderType=@orderType ");
            if (!string.IsNullOrEmpty(request.mobile))
                strSql.Append(" AND mobile like CONCAT('%',@mobile,'%') ");
            if (!string.IsNullOrEmpty(request.orderId))
                strSql.Append(" AND orderId like CONCAT('%',@orderId,'%') ");
            if (!string.IsNullOrEmpty(request.SpayDate))
                strSql.Append(" AND payDate>@SpayDate ");
            if (!string.IsNullOrEmpty(request.EpayDate))
                strSql.Append(" AND payDate<EpayDate ");
            if (!string.IsNullOrEmpty(request.SaddDate))
                strSql.Append(" AND addDate>@SaddDate ");
            if (!string.IsNullOrEmpty(request.EaddDate))
                strSql.Append(" AND payDate>EaddDate ");
            strSql.Append(" ORDER BY payDate DESC ");
            strSql.Append(" LIMIT @PageIndex,@PageSize;");
            strSql.Append(" select found_rows() as TotalCount ;");

            var parameters = new
            {
                request.userName,
                request.orderType,
                request.mobile,
                request.orderId,
                request.SpayDate,
                request.EpayDate,
                request.SaddDate,
                request.EaddDate,
                request.PageIndex,
                request.PageSize
            };
            var list = new List<OrderMainResponse.PageResponse>();
            var pageCount = new OrderMainResponse.PageCount();

            OpenSlaveConnection(conn =>
            {
                using (var multiReader = conn.QueryMultiple(strSql.ToString(), parameters))
                {
                    list = multiReader.Read<OrderMainResponse.PageResponse>().ToList();
                    pageCount = multiReader.Read<OrderMainResponse.PageCount>().FirstOrDefault();
                }

            });
            return new PagedEntity<OrderMainResponse.PageResponse>(pageCount.TotalCount, list);

        }
        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public OrderMainModel GetInfo(string Id)
        {
            return GetInfo(Id);
        }
        /// <summary>
        /// 通过OrderId拉取详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderMainResponse.PageResponse GetDetail(string orderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT u.username,u.mobile,o.* ");
            strSql.Append(" FROM t_order_ordermain o ");
            strSql.Append(" LEFT JOIN t_tour_user u on o.userID=u.id ");
            strSql.Append(" WHERE o.orderId=@orderId ");
            var list = new List<OrderMainResponse.PageResponse>();
            OpenSlaveConnection(conn =>
            {
                list = conn.Query<OrderMainResponse.PageResponse>(strSql.ToString(), new { orderId }).ToList();
            });
            return list.FirstOrDefault();
        }
        /// <summary>
        /// 增加订单
        /// </summary>
        /// <param name=""></param>
        public void Add(OrderMainModel entity)
        {
            Insert(entity);
        }
    }
}
