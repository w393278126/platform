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
using AutoMapper;

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
        public ResultWithCodeEntity<PagedEntity<TourUserListDTO>> PageList(TourUserRequest request)
        {
            var result = new PagedEntity<TourUserListDTO>() { Items = new List<TourUserListDTO>() };
            try
            {
                var pageResult = tourUserRepository.PageList(request);
                if (pageResult != null)
                {
                    result.TotalItems = pageResult.TotalItems;
                    result.Items = Mapper.Map<List<TourUserListDTO>>(pageResult.Items);
                    //foreach (var item in pageResult.Items)
                    //{
                    //    var val = Mapper.Map<List<TourUserListDTO>>(item);
                    //    result.Items.Add(val);
                    //}
                }

                return Result.Success<PagedEntity<TourUserListDTO>>(result);
            }
            catch (Exception ex)
            {
                return Result.Error<PagedEntity<TourUserListDTO>>(ResultCode.ExceptionError);
            }

        }
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResultWithCodeEntity<TourUserDTO> GetInfo(string Id)
        {
            try
            {
                var info = tourUserRepository.GetInfo(Id);
                var res = Mapper.Map<TourUserDTO>(info);
                return Result.Success<TourUserDTO>(res);
            }
            catch (Exception)
            {
                return Result.Error<TourUserDTO>(ResultCode.ExceptionError);
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultWithCodeEntity AddOrEdit(TourUserDTO entity)
        {
            try
            {
                var res = new TourUserModel();
                var result = false;
                if (!string.IsNullOrEmpty(entity.Id))
                {
                    var detail = tourUserRepository.GetInfo(entity.Id);
                    res = (TourUserModel)Mapper.Map(entity, detail, entity.GetType(), detail.GetType());
                    res.modify_time = DateTime.Now;
                    result = tourUserRepository.Update(res);
                }
                else
                {
                    entity.Id = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random(100).Next(100, 999);
                    //记录一下 数据库设置默认1
                    //entity.status = "1";
                    res = Mapper.Map<TourUserModel>(entity);
                    res.status = "1";
                    res.create_time = DateTime.Now;
                    result = tourUserRepository.Add(res);
                }

                if (result)
                    return Result.Success();
                else
                    return Result.Error(ResultCode.DefaultError);
            }
            catch (Exception ex)
            {
                return Result.Error(ResultCode.ExceptionError);
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
                    return Result.Success();
                else
                    return Result.Error(ResultCode.ExceptionError);

            }
            catch (Exception ex)
            {
                return Result.Error(ResultCode.ExceptionError);
            }
        }
    }
}
