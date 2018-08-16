using Xn.Platform.Domain.TourUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Abstractions.Domain;
using Xn.Platform.Data.MySql.TourUser;
using Xn.Platform.Domain;
using System.Reflection;

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
        public ResultWithCodeEntity AddOrEdit(TourUserModel entity)
        {
            try
            {
                int result = 0;
                if (!string.IsNullOrEmpty(entity.id))
                {
                    var detail = tourUserRepository.GetInfo(entity.id);

                    ///赋值
                    PropertyInfo[] propertys = entity.GetType().GetProperties();
                    foreach (var item in propertys)
                    {
                        var val = item.GetValue(entity);
                        //不为空的都赋值
                        if (val != null)
                        {
                            foreach (var item1 in detail.GetType().GetProperties())
                            {
                                if (item1.Name == item.Name)
                                {
                                    item1.SetValue(detail, val);
                                    break;
                                }
                            }
                        }
                    }
                    result = tourUserRepository.Update(detail);
                }
                else
                {
                    entity.id = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random(100).Next(100, 999);
                    entity.status = "1";
                    result = tourUserRepository.Add(entity);
                }
                if (result > 0)
                    return new ResultWithCodeEntity { Code = ResultCode.Success };
                else
                    return new ResultWithCodeEntity { Code = ResultCode.DefaultError };

            }
            catch (Exception ex)
            {
                return new ResultWithCodeEntity
                {
                    Code = ResultCode.ExceptionError,
                };
            }

        }
        /// <summary>
        /// 通过ID删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResultWithCodeEntity Delete(string Id)
        {
            try
            {
                var result = tourUserRepository.Delete(Id);
                if (result > 0)
                    return new ResultWithCodeEntity { Code = ResultCode.Success };
                else
                    return new ResultWithCodeEntity { Code = ResultCode.DefaultError };

            }
            catch (Exception ex)
            {

                return new ResultWithCodeEntity
                {
                    Code = ResultCode.ExceptionError,
                };
            }
        }
    }
}
