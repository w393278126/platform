using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Xn.Platform.Domain;
using Xn.Platform.Data;

namespace Xn.Platform.Data.MongoDb
{
    public class MogoEntity
    {
        /// <summary>
        /// 实体标识
        /// </summary>
        public ObjectId Id { get; set; }
    }
    public abstract class AbstractRepository<T> : IMongoRepository<T> where T : MogoEntity, new()
    {
        protected abstract string DataBase { get; }

        /// <summary>
        /// 根据Identity列的值进行对象查找，能有效的使用内置缓存优化读性能
        /// 建议使用这个方法而不用linq或Where等方法
        /// </summary>
        /// <param name="id">标识列值</param>
        /// <returns>找到的对应实体对象(找不到为null)</returns>
        public T Get(int id)
        {
            var collection = MongoDataBase.GetCollection<T>(DataBase);
            var entity = collection.FindOne(Query.EQ("_id", BsonValue.Create(id)));
            return entity;
        }

        public T Get(string id)
        {
            var collection = MongoDataBase.GetCollection<T>(DataBase);
            var entity = collection.FindOne(Query.EQ("_id", ObjectId.Parse(id)));
            return entity;
        }


        public virtual T MakePersistent(T entity)
        {
            var collection = MongoDataBase.GetCollection<T>(DataBase);
            if (entity.Id == ObjectId.Empty)
                collection.Insert<T>(entity);
            else
                collection.Save<T>(entity);
            return entity;
        }


        /// <summary>
        /// 根据标识属性删除对象
        /// </summary>
        /// <param name="id"></param>
        public void MakeTransient(int id)
        {
            var collection = MongoDataBase.GetCollection<T>(DataBase);
            collection.Remove(Query.EQ("_id", BsonValue.Create(id)));
        }

        /// <summary>
        /// 根据标识属性删除对象
        /// </summary>
        /// <param name="id"></param>
        public void MakeTransient(string id)
        {
            var collection = MongoDataBase.GetCollection<T>(DataBase);
            collection.Remove(Query.EQ("_id", ObjectId.Parse(id)));
        }


        public ICollection<T2> GetList<T2>(ICollection<Tuple<string, string, object>> parameters)
        {
            throw new NotImplementedException();
        }

        public PagedEntity<T2> GetPagedEntity<T2>(int pageIndex, int pageSize, string orderBy, ICollection<Tuple<string, string, object>> parameters)
        {
            var entities = new PagedEntity<T2>();
            var collection = MongoDataBase.GetCollection<T2>(DataBase);

            var queries = new List<IMongoQuery>();
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    queries.Add(Query.EQ(parameter.Item1, BsonValue.Create(parameter.Item3)));
                }
            }
            MongoCursor<T2> cursor;
            if (queries.Count > 0)
            {
                cursor = collection.Find(Query.And(queries));
            }
            else
            {
                cursor = collection.FindAll();
            }
            IMongoSortBy sortBy;
            var value = orderBy.Split(' ');
            if (value.Length == 2)
            {
                if (value[1] == "asc")
                {
                    sortBy = SortBy.Ascending(value[0]);
                }
                else
                {
                    sortBy = SortBy.Descending(value[0]);
                }
            }
            else
            {
                sortBy = SortBy.Descending(orderBy);
            }
            //CreateTime
            entities.Items = cursor.SetSortOrder(sortBy).SetSkip(pageIndex * pageSize).SetLimit(pageSize).ToList();
            entities.TotalItems = (int)cursor.Count();
            return entities;
        }

        public int Delete(ICollection<Tuple<string, string, object>> parameters, bool isDelete)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Update<TV>(int id, string where, string propertyName, TV value)
        {
            var collection = MongoDataBase.GetCollection<T>(DataBase);
            collection.Update(Query.EQ(where, BsonValue.Create(id)),
                new UpdateBuilder().Set(propertyName, BsonValue.Create(value)));
        }
    }
}