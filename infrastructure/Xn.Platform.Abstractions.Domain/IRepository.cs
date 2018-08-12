using System;
using System.Collections.Generic;
using System.Text;
using Xn.Platform.Domain;

namespace Xn.Platform.Data
{
    public interface IRepository<T>
    {
        /// <summary>
        /// 获取单一对象
        /// </summary>
        /// <param name="id">标识</param>
        /// <returns></returns>
        T Get(int id);
        /// <summary>
        /// 持久化到数据库(保存或者更新)
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        T MakePersistent(T entity);
        /// <summary>
        /// 删除(软删除)
        /// </summary>
        /// <param name="id">标识</param>
        void MakeTransient(int id);
        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <typeparam name="T2">实体类型</typeparam>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        ICollection<T2> GetList<T2>(ICollection<Tuple<string, string, object>> parameters);

        /// <summary>
        /// 获取分页实体集合
        /// </summary>
        /// <typeparam name="T2">实体类型</typeparam>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="orderBy">排序</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        PagedEntity<T2> GetPagedEntity<T2>(int pageIndex, int pageSize, string orderBy, ICollection<Tuple<string, string, object>> parameters);

        /// <summary>
        /// 删除(真删除)
        /// </summary>
        int Delete(ICollection<Tuple<string, string, object>> parameters, bool isDelete);
        /// <summary>
        /// 清空表数据(慎用)
        /// </summary>
        void Clear();
    }
}