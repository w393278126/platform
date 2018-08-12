namespace Xn.Platform.Data
{
    public interface IMongoRepository<T> : IRepository<T>
    {
        /// <summary>
        /// 更新指定id对象的propertyName属性的值为value
        /// </summary>
        /// <typeparam name="TV">目标属性值类型</typeparam>
        /// <param name="id">条件的值</param>
        /// <param name="where">条件字段</param>
        /// <param name="propertyName">要更新值的属性名称</param>
        /// <param name="value">属性新的值</param>
        void Update<TV>(int id, string where, string propertyName, TV value);
    }
}