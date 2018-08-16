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
            strSql.Append(" SELECT SQL_CALC_FOUND_ROWS o.id,o.userID,u.username,addDate,sendDate,states,receiverName,receiverTelphone,payName,payTelephone,payType,payTSN,payDate,orderAmount,payAmount,refundID,orderID,refundDate,refundAmout,expressAmount,postType,expressCode,expressName,expressOdd,provinceID,provinceName,cityID,channelID,cityName,areaID,o.address,areaName,platform,o.deviceID,longitude,latitude");
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
            strSql.Append(" LIMIT @PageIndex,@PageSize;select found_rows()");

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
            int totalCount = 0;

            OpenSlaveConnection(conn =>
            {

                var result = conn.QueryMultiple(strSql.ToString(), parameters);
                list = result.Read<OrderMainResponse.PageResponse>().ToList();
                totalCount = result.Read<int>().FirstOrDefault();
            });
            return new PagedEntity<OrderMainResponse.PageResponse>(totalCount, list);

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
        /// 增加订单
        /// </summary>
        /// <param name=""></param>
        public void Add(OrderMainModel entity)
        {
            Insert(entity);
        }
    }
}
