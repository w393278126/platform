using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Abstractions.Domain;
using Xn.Platform.Data.MySql.Order;
using Xn.Platform.Domain.Order;

namespace Xn.Platform.Domain.Impl.Order
{
    public class XnPassengerService
    {
        private XnPassengerRepository xnPassengerRepository = new XnPassengerRepository();
        private XnPassengerCardRepository xnPassengerCardRepository = new XnPassengerCardRepository();
        private XnOrderPassengerRepository xnOrderPassengerRepository = new XnOrderPassengerRepository();
        private XnOrderRepository xnOrderRepository = new XnOrderRepository();
        /// <summary>
        /// 通过ID查询出行人详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResultWithCodeEntity<XnPassengerDTO> GetInfo(int Id)
        {
            try
            {
                var result = xnPassengerRepository.GetPassengerInfo(Id);
                return Result.Success(result);
            }
            catch (Exception ex)
            {
                return Result.Error(ResultCode.ExceptionError, new XnPassengerDTO());
            }
        }
        /// <summary>
        /// 新增或编辑
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResultWithCodeEntity AddOrEdit(XnPassengerRequest request)
        {
            try
            {
                var orderEntity = xnOrderRepository.Get(request.OrderId);
                if (orderEntity == null || orderEntity.Id == 0)
                    return Result.Error(ResultCode.ParameterError);
                ///新增出行人的基础信息
                var res = Mapper.Map<XnPassengerModel>(request);
                if (request.Id == 0)
                    res.CreateTime = DateTime.Now;
                else
                    res.LastTime = DateTime.Now;
                res.UserId = orderEntity.UserId;
                var entity = xnPassengerRepository.MakePersistent(res);
                if (entity == null || entity.Id == 0)
                {
                    return Result.Error(ResultCode.DefaultError);
                }
                ///身份证
                EditPassgerCard(request.IDNumber, 1, entity.Id);
                ///护照
                EditPassgerCard(request.PassportNo, 2, entity.Id);
                ///新增时必须新增订单人员出行记录
                if (request.Id == 0)
                {
                    ///新增订单人员关联表
                    xnOrderPassengerRepository.MakePersistent(new XnOrderPassengerModel
                    {
                        OrderId = orderEntity.Id,
                        PassengerId = entity.Id,
                        Status = 1,
                        CreateTime = DateTime.Now
                    });
                }
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error(ResultCode.ExceptionError);
            }
        }
        private void EditPassgerCard(string cardId, int cardType, int passengerId)
        {
            if (!string.IsNullOrWhiteSpace(cardId))
            {
                var cardEntity = xnPassengerCardRepository.GetCardEntity(passengerId, cardType);
                if (cardEntity == null)
                    cardEntity = new XnPassengerCardModel()
                    {
                        CardType = cardType,
                        PassengerId = passengerId,
                        CreateTime = DateTime.Now,
                        Status = 1
                    };
                cardEntity.CardId = cardId;
                xnPassengerCardRepository.MakePersistent(cardEntity);

            }

        }

    }
}
