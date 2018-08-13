using Plu.Platform.Domain.TourUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Abstractions.Domain;
using Xn.Platform.Data.MySql.TourUser;
using Xn.Platform.Domain;

namespace Plu.Platform.Domain.Impl.TourUser
{
    public class TourUserService
    {
        private TourUsersRepository tourUserRepository = new TourUsersRepository();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResultWithCodeEntity<PagedEntity<TourUserModel>> PageList(TourUserRequest.PageResult request)
        {
            try
            {
                var item = tourUserRepository.PageList(request);
                return new ResultWithCodeEntity<PagedEntity<TourUserModel>>
                {
                    Code = ResultCode.Success,
                    Data = item
                };
            }
            catch (Exception ex)
            {
                return new ResultWithCodeEntity<PagedEntity<TourUserModel>>
                {
                    Code = ResultCode.ExceptionError,
                    Data = new PagedEntity<TourUserModel>()
                };
            }

        }
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResultWithCodeEntity<TourUserModel> GetInfo(string Id)
        {
            try
            {
                var item = tourUserRepository.GetInfo(Id);
                return new ResultWithCodeEntity<TourUserModel>
                {
                    Code = ResultCode.Success,
                    Data = item
                };
            }
            catch (Exception)
            {

                return new ResultWithCodeEntity<TourUserModel>
                {
                    Code = ResultCode.ExceptionError,
                    Data = new TourUserModel()
                };
            }

        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultWithCodeEntity<TourUserModel> AddMsg(TourUserModel entity)
        {
            try
            {
                var item = tourUserRepository.AddOrUpdate(entity);
                return new ResultWithCodeEntity<TourUserModel>
                {
                    Code = ResultCode.Success,
                    Data = item
                };
            }
            catch (Exception)
            {
                return new ResultWithCodeEntity<TourUserModel>
                {
                    Code = ResultCode.ExceptionError,
                    Data = new TourUserModel()
                };
            }

        }
    }
}
