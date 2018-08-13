using Dapper;
using MySql.Data.MySqlClient;
using Xn.Platform.Core;
using Xn.Platform.Core.Extensions;
using Xn.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Xn.Platform.Data.MySql
{
    public abstract class AbstractRepository<T> : IRepository<T> where T : new()
    {
        protected string ConnectionString;
        protected string SlaveConnectionString;
        protected readonly Type Type;

        protected AbstractRepository()
        {
            ConnectionString = ConfigSetting.ConnectionHome;
            SlaveConnectionString = ConfigSetting.ConnectionHomeReadOnly;
            Type = typeof(T);
        }

        public void OpenConnection(Action<IDbConnection> action)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                action(connection);
            }
        }

        protected void OpenSlaveConnection(Action<IDbConnection> action)
        {
            using (var connection = new MySqlConnection(SlaveConnectionString))
            {
                connection.Open();
                action(connection);
            }
        }

        protected static string GetBaseSelectSql(Type type)
        {
            string baseSelectSql;
            if (!SqlMapperExtensions.SelectQueries.TryGetValue(type.TypeHandle, out baseSelectSql))
            {
                var tableName = SqlMapperExtensions.GetTableName(type);
                var allProperties = SqlMapperExtensions.TypePropertiesCache(type);
                var computedProperties = SqlMapperExtensions.ComputedPropertiesCache(type);
                var allPropertiesExceptComputed = allProperties.Except(computedProperties);
                var status = false;
                var hasStatus = allPropertiesExceptComputed.FirstOrDefault(o => o.Name == "Status");
                if (hasStatus != null)
                {
                    status = !SqlMapperExtensions.ExcludeStatus(hasStatus);
                }
                baseSelectSql = string.Format("select {0} from {1} where {2} = 1",
                    string.Join(",", allPropertiesExceptComputed.Select(o => string.Format("`{0}`", o.Name))),
                    tableName, status ? "Status" : "1");
                SqlMapperExtensions.SelectQueries[type.TypeHandle] = baseSelectSql;
            }
            return baseSelectSql;
        }

        protected static string GetBaseSelectSqlForNoStatus(Type type)
        {
            string baseSelectSql;
            if (!SqlMapperExtensions.SelectQueries.TryGetValue(type.TypeHandle, out baseSelectSql))
            {
                var tableName = SqlMapperExtensions.GetTableName(type);
                var allProperties = SqlMapperExtensions.TypePropertiesCache(type);
                var computedProperties = SqlMapperExtensions.ComputedPropertiesCache(type);
                var allPropertiesExceptComputed = allProperties.Except(computedProperties);
                baseSelectSql = string.Format("select {0} from {1} where {2} = 1",
                    string.Join(",", allPropertiesExceptComputed.Select(o => string.Format("`{0}`", o.Name))),
                    tableName, "1");
                SqlMapperExtensions.SelectQueries[type.TypeHandle] = baseSelectSql;
            }
            return baseSelectSql;
        }


        public virtual T Get(int id)
        {
            if (id == 0)
                return new T();
            T entity = default(T);

            var keyProperties = SqlMapperExtensions.KeyPropertiesCache(Type);
            if (keyProperties.Count != 1)
                throw new DataException("Get<T> only supports an entity with a single [Key] property");
            var idProperty = keyProperties.First();
            var baseSelectSql = GetBaseSelectSql(Type);
            StringBuilder sql = new StringBuilder(baseSelectSql);
            sql.AppendFormat(" and {0}=@id limit 1", idProperty.Name);

            var dynParms = new DynamicParameters();
            dynParms.Add("@id", id);
            OpenSlaveConnection(conn =>
            {
                entity = conn.Query<T>(sql.ToString(), dynParms).FirstOrDefault();
            });
            return entity;
        }

        public virtual T GetFromMaster(int id)
        {
            if (id == 0)
                return new T();
            T entity = default(T);

            var keyProperties = SqlMapperExtensions.KeyPropertiesCache(Type);
            if (keyProperties.Count != 1)
                throw new DataException("Get<T> only supports an entity with a single [Key] property");
            var idProperty = keyProperties.First();
            var baseSelectSql = GetBaseSelectSql(Type);
            StringBuilder sql = new StringBuilder(baseSelectSql);
            sql.AppendFormat(" and {0}=@id limit 1", idProperty.Name);

            var dynParms = new DynamicParameters();
            dynParms.Add("@id", id);
            OpenConnection(conn =>
            {
                entity = conn.Query<T>(sql.ToString(), dynParms).FirstOrDefault();
            });
            return entity;
        }

        public virtual T GetDataFromMaster(string fieldName, object value)
        {
            T entity = default(T);

            var keyProperties = SqlMapperExtensions.KeyPropertiesCache(Type);
            if (keyProperties.Count != 1)
                throw new DataException("Get<T> only supports an entity with a single [Key] property");

            var baseSelectSql = GetBaseSelectSql(Type);
            StringBuilder sql = new StringBuilder(baseSelectSql);
            sql.AppendFormat(" and {0}=@{0} limit 1", fieldName);

            var dynParms = new DynamicParameters();
            dynParms.Add("@" + fieldName, value);
            OpenConnection(conn =>
            {
                entity = conn.Query<T>(sql.ToString(), dynParms).FirstOrDefault();
            });
            return entity;
        }

        public virtual T GetDataFromSlave(string fieldName, object value)
        {
            T entity = default(T);

            var keyProperties = SqlMapperExtensions.KeyPropertiesCache(Type);
            if (keyProperties.Count != 1)
                throw new DataException("Get<T> only supports an entity with a single [Key] property");

            var baseSelectSql = GetBaseSelectSql(Type);
            StringBuilder sql = new StringBuilder(baseSelectSql);
            sql.AppendFormat(" and {0}=@{0} limit 1", fieldName);

            var dynParms = new DynamicParameters();
            dynParms.Add("@" + fieldName, value);
            OpenSlaveConnection(conn =>
            {
                entity = conn.Query<T>(sql.ToString(), dynParms).FirstOrDefault();
            });
            return entity;
        }

        public virtual T GetForStatus(int id)
        {
            if (id == 0)
                return new T();
            T entity = default(T);

            var keyProperties = SqlMapperExtensions.KeyPropertiesCache(Type);
            if (keyProperties.Count != 1)
                throw new DataException("Get<T> only supports an entity with a single [Key] property");
            var idProperty = keyProperties.First();
            var baseSelectSql = GetBaseSelectSqlForNoStatus(Type);
            StringBuilder sql = new StringBuilder(baseSelectSql);
            sql.AppendFormat(" and {0}=@id limit 1", idProperty.Name);

            var dynParms = new DynamicParameters();
            dynParms.Add("@id", id);
            OpenSlaveConnection(conn =>
            {
                entity = conn.Query<T>(sql.ToString(), dynParms).FirstOrDefault();
            });
            return entity;
        }

        public virtual T MakePersistent(T entity)
        {
            if (entity == null)
                return new T();
            var keyProperties = SqlMapperExtensions.KeyPropertiesCache(Type);
            if (keyProperties.Count != 1)
                throw new DataException("Get<T> only supports an entity with a single [Key] property");
            var idProperty = keyProperties.First();
            var id = idProperty.GetValue(entity).AsInt();
            if (id == 0)
            {
                Insert(entity);
            }
            else
            {
                Update(entity);
            }
            return entity;
        }

        public virtual T MakePersistentNoException(T entity)
        {
            try
            {
                return MakePersistent(entity);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        protected void Insert(T entity)
        {
            var tableName = SqlMapperExtensions.GetTableName(Type);
            var allProperties = SqlMapperExtensions.TypePropertiesCache(Type);
            var keyProperties = SqlMapperExtensions.KeyPropertiesCache(Type);
            var computedProperties = SqlMapperExtensions.ComputedPropertiesCache(Type);
            var allPropertiesExceptKeyAndComputed = allProperties.Except(keyProperties.Union(computedProperties)).ToList();

            if (keyProperties.Count != 1)
                throw new DataException("Get<T> only supports an entity with a single [Key] property");
            var idProperty = keyProperties.First();

            var sbColumnList = new StringBuilder(null);
            for (var i = 0; i < allPropertiesExceptKeyAndComputed.Count(); i++)
            {
                var property = allPropertiesExceptKeyAndComputed.ElementAt(i);
                sbColumnList.AppendFormat("`{0}`", property.Name);
                if (i < allPropertiesExceptKeyAndComputed.Count() - 1)
                    sbColumnList.Append(", ");
            }

            var sbParameterList = new StringBuilder(null);
            for (var i = 0; i < allPropertiesExceptKeyAndComputed.Count(); i++)
            {
                var property = allPropertiesExceptKeyAndComputed.ElementAt(i);
                sbParameterList.AppendFormat("@{0}", property.Name);
                if (i < allPropertiesExceptKeyAndComputed.Count() - 1)
                    sbParameterList.Append(", ");
            }
            //string sql = String.Format("insert into {0} ({1},Status,LastChanged) values ({2},1,ADDTIME(now(),'00:00:10'))", tableName, sbColumnList.ToString(), sbParameterList.ToString());

            string sql = String.Format("insert into {0} ({1}) values ({2});select LAST_INSERT_ID()", tableName, sbColumnList.ToString(), sbParameterList.ToString());

            OpenConnection(conn =>
            {
                var inserted = conn.Query<int>(sql, entity).FirstOrDefault();
                if (idProperty.PropertyType.Name == "Int16") //for short id/key types issue #196
                    idProperty.SetValue(entity, (Int16)inserted, null);
                else
                    idProperty.SetValue(entity, inserted, null);
            });
        }

        public bool Update(T entity)
        {
            var keyProperties = SqlMapperExtensions.KeyPropertiesCache(Type);
            if (keyProperties.Count == 0)
                throw new ArgumentException("Entity must have at least one [Key] property");

            var name = SqlMapperExtensions.GetTableName(Type);

            var sb = new StringBuilder();
            sb.AppendFormat("update {0} set ", name);

            var allProperties = SqlMapperExtensions.TypePropertiesCache(Type);
            var computedProperties = SqlMapperExtensions.ComputedPropertiesCache(Type);
            var nonIdProps = allProperties.Except(keyProperties.Union(computedProperties)).ToList();

            for (var i = 0; i < nonIdProps.Count(); i++)
            {
                var property = nonIdProps.ElementAt(i);
                sb.AppendFormat("`{0}` = @{1}", property.Name, property.Name);
                if (i < nonIdProps.Count() - 1)
                    sb.AppendFormat(", ");
            }
            //sb.Append(",LastChanged=ADDTIME(now(),'00:00:10')");
            sb.Append(" where ");
            for (var i = 0; i < keyProperties.Count; i++)
            {
                var property = keyProperties.ElementAt(i);
                sb.AppendFormat("`{0}` = @{1}", property.Name, property.Name);
                if (i < keyProperties.Count() - 1)
                    sb.AppendFormat(" and ");
            }
            var updated = 0;
            OpenConnection(conn =>
            {
                updated = conn.Execute(sb.ToString(), entity);
            });

            return updated > 0;
        }

        /// <summary>
        /// 设置状态为1的记录为-1
        /// </summary>
        public virtual void MakeTransient(int id)
        {
            if (id == 0)
                return;
            var keyProperties = SqlMapperExtensions.KeyPropertiesCache(Type);
            if (keyProperties.Count != 1)
                throw new DataException("Get<T> only supports an entity with a single [Key] property");
            var idProperty = keyProperties.First();
            Delete(new List<Tuple<string, string, object>> { Tuple.Create<string, string, object>(idProperty.Name, "=", id) }, false);
        }

        /// <summary>
        /// 设置状态为-1（没有任何限制）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int MakeTransientWithAnyStatus(int id)
        {
            if (id == 0)
                return 0;
            var keyProperties = SqlMapperExtensions.KeyPropertiesCache(Type);
            if (keyProperties.Count != 1)
                throw new DataException("Get<T> only supports an entity with a single [Key] property");
            var idProperty = keyProperties.First();
            return DeleteAnyStatus(new List<Tuple<string, string, object>> { Tuple.Create<string, string, object>(idProperty.Name, "=", id) }, false);
        }


        public virtual ICollection<T2> GetList<T2>(ICollection<Tuple<string, string, object>> parameters)
        {
            var baseSelectSql = GetBaseSelectSql(typeof(T2));
            var sql = new StringBuilder();
            var dynamicParameters = new DynamicParameters();
            if (parameters != null)
            {
                for (var i = 0; i < parameters.Count; i++)
                {
                    var parameter = parameters.ElementAt(i);
                    var parameterName = string.Format("{0}{1}", parameter.Item1, i);
                    sql.AppendFormat(" and {0} {1} @{2}", parameter.Item1, parameter.Item2, parameterName);
                    dynamicParameters.Add(parameterName, parameter.Item3);
                }
            }
            ICollection<T2> entities = null;
            OpenSlaveConnection(conn =>
            {
                entities = conn.Query<T2>(string.Format("{0}{1}", baseSelectSql, sql.ToString()), dynamicParameters).ToList();
            });
            return entities;
        }

        public virtual ICollection<T2> GetListFromMaster<T2>(ICollection<Tuple<string, string, object>> parameters)
        {
            var baseSelectSql = GetBaseSelectSql(typeof(T2));
            var sql = new StringBuilder();
            var dynamicParameters = new DynamicParameters();
            if (parameters != null)
            {
                for (var i = 0; i < parameters.Count; i++)
                {
                    var parameter = parameters.ElementAt(i);
                    var parameterName = string.Format("{0}{1}", parameter.Item1, i);
                    sql.AppendFormat(" and {0} {1} @{2}", parameter.Item1, parameter.Item2, parameterName);
                    dynamicParameters.Add(parameterName, parameter.Item3);
                }
            }
            ICollection<T2> entities = null;
            OpenConnection(conn =>
            {
                entities = conn.Query<T2>(string.Format("{0}{1}", baseSelectSql, sql.ToString()), dynamicParameters).ToList();
            });
            return entities;
        }

        public virtual ICollection<T2> GetListFromSlave<T2>(ICollection<Tuple<string, string, object>> parameters)
        {
            var baseSelectSql = GetBaseSelectSql(typeof(T2));
            var sql = new StringBuilder();
            var dynamicParameters = new DynamicParameters();
            if (parameters != null)
            {
                for (var i = 0; i < parameters.Count; i++)
                {
                    var parameter = parameters.ElementAt(i);
                    var parameterName = string.Format("{0}{1}", parameter.Item1, i);
                    sql.AppendFormat(" and {0} {1} @{2}", parameter.Item1, parameter.Item2, parameterName);
                    dynamicParameters.Add(parameterName, parameter.Item3);
                }
            }
            ICollection<T2> entities = null;
            OpenSlaveConnection(conn =>
            {
                entities = conn.Query<T2>(string.Format("{0}{1}", baseSelectSql, sql.ToString()), dynamicParameters).ToList();
            });
            return entities;
        }


        public virtual PagedEntity<T2> GetPagedEntity<T2>(int pageIndex, int pageSize, string orderBy, ICollection<Tuple<string, string, object>> parameters)
        {
            var baseSelectSql = GetBaseSelectSqlForNoStatus(typeof(T2));
            var tableName = SqlMapperExtensions.GetTableName(typeof(T2));
            var allProperties = SqlMapperExtensions.TypePropertiesCache(typeof(T2));
            var computedProperties = SqlMapperExtensions.ComputedPropertiesCache(typeof(T2));
            var allPropertiesExceptComputed = allProperties.Except(computedProperties);
            bool status = false;
            var hasStatus = allPropertiesExceptComputed.FirstOrDefault(o => o.Name == "Status");
            if (hasStatus != null)
            {
                status = !SqlMapperExtensions.ExcludeStatus(hasStatus);
            }
            var countsql = string.Format("select count(1) from {0} where 1 = 1", tableName);

            var sql = new StringBuilder();
            var dynamicParameters = new DynamicParameters();
            if (parameters != null)
            {
                for (var i = 0; i < parameters.Count; i++)
                {
                    var parameter = parameters.ElementAt(i);
                    var parameterName = string.Format("{0}{1}", parameter.Item1, i);
                    sql.AppendFormat(" and {0} {1} @{2}", parameter.Item1, parameter.Item2, parameterName);
                    dynamicParameters.Add(parameterName, parameter.Item3);
                }
            }

            if (status)
            {
                sql.Append(" and Status = 1");
            }

            var entities = new PagedEntity<T2>();
            entities.Items = new List<T2>();
            OpenSlaveConnection(conn =>
            {
                entities.TotalItems = conn.Query<int>(string.Format("{0}{1}", countsql, sql.ToString()), dynamicParameters).FirstOrDefault();
                if (entities.TotalItems == 0)
                    return;
                sql.AppendFormat(" order by {0} limit @pageIndex,@pageSize", orderBy);
                dynamicParameters.Add("pageIndex", pageIndex * pageSize);
                dynamicParameters.Add("pageSize", pageSize);
                entities.Items = conn.Query<T2>(string.Format("{0}{1}", baseSelectSql, sql.ToString()), dynamicParameters).ToList();
            });
            return entities;
        }

        /// <summary>
        /// 分页-同PagedEntity<T2> GetPagedEntity
        /// 多出一个功能：为某一列做统计信息，统计信息需为能够被double覆盖的类型
        /// </summary>
        /// <param name="statisticalFiled">统计字段</param>
        /// <returns></returns>
        public virtual PagedEntityWithStatistical<T2> GetPagedEntityWithStatistical<T2>(int pageIndex, int pageSize, string orderBy, ICollection<Tuple<string, string, object>> parameters, string statisticalFiled)
        {
            var baseSelectSql = GetBaseSelectSqlForNoStatus(typeof(T2));
            var tableName = SqlMapperExtensions.GetTableName(typeof(T2));
            var allProperties = SqlMapperExtensions.TypePropertiesCache(typeof(T2));
            var computedProperties = SqlMapperExtensions.ComputedPropertiesCache(typeof(T2));
            var allPropertiesExceptComputed = allProperties.Except(computedProperties);
            bool status = false;
            var hasStatus = allPropertiesExceptComputed.FirstOrDefault(o => o.Name == "Status");
            if (hasStatus != null)
            {
                status = !SqlMapperExtensions.ExcludeStatus(hasStatus);
            }
            var countsql = string.Format("select count(1) from {0} where 1 = 1", tableName);

            var statisticalSql = string.Format("select sum({0}) from {1} where 1 = 1", statisticalFiled, tableName);

            var sql = new StringBuilder();
            var dynamicParameters = new DynamicParameters();
            if (parameters != null)
            {
                for (var i = 0; i < parameters.Count; i++)
                {
                    var parameter = parameters.ElementAt(i);
                    var parameterName = string.Format("{0}{1}", parameter.Item1, i);
                    sql.AppendFormat(" and {0} {1} @{2}", parameter.Item1, parameter.Item2, parameterName);
                    dynamicParameters.Add(parameterName, parameter.Item3);
                }
            }

            if (status)
            {
                sql.Append(" and Status = 1");
            }

            var entities = new PagedEntityWithStatistical<T2>();
            entities.Items = new List<T2>();
            OpenSlaveConnection(conn =>
            {
                entities.TotalItems = conn.Query<int>(string.Format("{0}{1}", countsql, sql.ToString()), dynamicParameters).FirstOrDefault();
                if (entities.TotalItems == 0)
                    return;

                entities.TotalStatistical = conn.Query<double>(string.Format("{0}{1}", statisticalSql, sql.ToString()), dynamicParameters).FirstOrDefault();

                sql.AppendFormat(" order by {0} limit @pageIndex,@pageSize", orderBy);
                dynamicParameters.Add("pageIndex", pageIndex * pageSize);
                dynamicParameters.Add("pageSize", pageSize);
                entities.Items = conn.Query<T2>(string.Format("{0}{1}", baseSelectSql, sql.ToString()), dynamicParameters).ToList();
            });
            return entities;
        }


        public virtual PagedEntity<T2> GetPagedEntityByDefine<T2>(int start, int pageSize, string orderBy, ICollection<Tuple<string, string, object>> parameters)
        {
            var baseSelectSql = GetBaseSelectSql(typeof(T2));
            var tableName = SqlMapperExtensions.GetTableName(typeof(T2));
            var allProperties = SqlMapperExtensions.TypePropertiesCache(typeof(T2));
            var computedProperties = SqlMapperExtensions.ComputedPropertiesCache(typeof(T2));
            var allPropertiesExceptComputed = allProperties.Except(computedProperties);
            bool status = false;
            var hasStatus = allPropertiesExceptComputed.FirstOrDefault(o => o.Name == "Status");
            if (hasStatus != null)
            {
                status = !SqlMapperExtensions.ExcludeStatus(hasStatus);
            }
            var countsql = $"select count(1) from {tableName} where {(status ? "Status" : "1")} = 1";

            var sql = new StringBuilder();
            var dynamicParameters = new DynamicParameters();
            if (parameters != null)
            {
                for (var i = 0; i < parameters.Count; i++)
                {
                    var parameter = parameters.ElementAt(i);
                    var parameterName = $"{parameter.Item1}{i}";
                    sql.AppendFormat(" and {0} {1} @{2}", parameter.Item1, parameter.Item2, parameterName);
                    dynamicParameters.Add(parameterName, parameter.Item3);
                }
            }
            var entities = new PagedEntity<T2>();
            entities.Items = new List<T2>();
            OpenSlaveConnection(conn =>
            {
                entities.TotalItems = conn.Query<int>($"{countsql}{sql.ToString()}", dynamicParameters).FirstOrDefault();
                if (entities.TotalItems == 0)
                    return;
                sql.AppendFormat(" order by {0} limit @pageIndex,@pageSize", orderBy);
                dynamicParameters.Add("pageIndex", start);
                dynamicParameters.Add("pageSize", pageSize);
                entities.Items = conn.Query<T2>($"{baseSelectSql}{sql.ToString()}", dynamicParameters).ToList();
            });
            return entities;
        }


        public virtual PagedEntity<T2> GetPagedOrEntity<T2>(int pageIndex, int pageSize, string orderBy, ICollection<Tuple<string, string, object>> parameters, bool onlyNormalStatus = false)
        {
            var tableName = SqlMapperExtensions.GetTableName(typeof(T2));
            var baseSelectSql = string.Format("select * from {0}  where 1 = 1", tableName);

            var countsql = string.Format("select count(1) from {0} where 1 = 1", tableName);

            var sql = new StringBuilder();
            var dynamicParameters = new DynamicParameters();

            if (parameters != null && parameters.Count > 0)
            {
                for (var i = 0; i < parameters.Count; i++)
                {
                    var parameter = parameters.ElementAt(i);
                    var parameterName = string.Format("{0}", parameter.Item1);
                    if (i == 0)
                    {
                        sql.AppendFormat(" and ( {0} {1} @{2}", parameter.Item1, parameter.Item2, parameterName);
                    }
                    else
                    {
                        sql.AppendFormat(" or {0} {1} @{2}", parameter.Item1, parameter.Item2, parameterName);
                    }
                    dynamicParameters.Add(parameterName, parameter.Item3);
                }

                sql.Append(")");
            }

            if (onlyNormalStatus)
            {
                var allProperties = SqlMapperExtensions.TypePropertiesCache(typeof(T2));
                var computedProperties = SqlMapperExtensions.ComputedPropertiesCache(typeof(T2));
                var allPropertiesExceptComputed = allProperties.Except(computedProperties);
                bool status = false;
                var hasStatus = allPropertiesExceptComputed.FirstOrDefault(o => o.Name == "Status");
                if (hasStatus != null)
                {
                    status = !SqlMapperExtensions.ExcludeStatus(hasStatus);
                }

                if (status)
                {
                    sql.Append(" and Status = 1");
                }
            }

            var entities = new PagedEntity<T2>();
            entities.Items = new List<T2>();
            OpenSlaveConnection(conn =>
            {
                entities.TotalItems = conn.Query<int>(string.Format("{0}{1}", countsql, sql.ToString()), dynamicParameters).FirstOrDefault();
                if (entities.TotalItems == 0)
                    return;
                sql.AppendFormat(" order by {0} limit @pageIndex,@pageSize", orderBy);
                dynamicParameters.Add("pageIndex", pageIndex * pageSize);
                dynamicParameters.Add("pageSize", pageSize);
                entities.Items = conn.Query<T2>(string.Format("{0}{1}", baseSelectSql, sql.ToString()), dynamicParameters).ToList();
            });
            return entities;
        }



        public PagedEntity<T2> GetPaged<T2>(int pageIndex, int pageSize, string orderBy, ICollection<Tuple<string, string, object>> parameters, string conditionKey = "and", string sqlNoValue = "")
        {
            var tableName = SqlMapperExtensions.GetTableName(typeof(T2));

            var baseSelectSql = $"select * from {tableName}";
            var countsql = $"select count(1) from {tableName}  ";

            var sql = new StringBuilder();
            var dynamicParameters = new DynamicParameters();
            if (parameters != null)
            {
                for (var i = 0; i < parameters.Count; i++)
                {
                    var parameter = parameters.ElementAt(i);
                    var parameterName = $"{parameter.Item1}";
                    if (i == 0)
                    {
                        sql.AppendFormat(" where {0} {1} @{2}", parameter.Item1, parameter.Item2, parameterName);
                    }
                    else
                    {
                        sql.AppendFormat(" {0} {1} {2} @{3}", conditionKey, parameter.Item1, parameter.Item2, parameterName);
                    }

                    dynamicParameters.Add(parameterName, parameter.Item3);
                }
            }

            if (!string.IsNullOrEmpty(sqlNoValue))
            {
                sql.Append(parameters != null && parameters.Count > 0 ? sqlNoValue : $" where 1=1 {sqlNoValue}");
            }

            var entities = new PagedEntity<T2> { Items = new List<T2>() };
            OpenSlaveConnection(conn =>
            {
                entities.TotalItems = conn.Query<int>($"{countsql}{sql.ToString()}", dynamicParameters).FirstOrDefault();
                if (entities.TotalItems == 0)
                    return;
                sql.AppendFormat(" order by {0} limit @pageIndex,@pageSize", orderBy);
                dynamicParameters.Add("pageIndex", pageIndex * pageSize);
                dynamicParameters.Add("pageSize", pageSize);
                entities.Items = conn.Query<T2>($"{baseSelectSql}{sql.ToString()}", dynamicParameters).ToList();
            });
            return entities;
        }


        public virtual ICollection<T2> GetList<T2>(int pageIndex, int pageSize, string orderBy, ICollection<Tuple<string, string, object>> parameters)
        {
            var baseSelectSql = GetBaseSelectSql(typeof(T2));
            var sql = new StringBuilder();
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("pageIndex", pageIndex * pageSize);
            dynamicParameters.Add("pageSize", pageSize);
            if (parameters != null)
            {
                for (var i = 0; i < parameters.Count; i++)
                {
                    var parameter = parameters.ElementAt(i);
                    var parameterName = string.Format("{0}{1}", parameter.Item1, i);
                    sql.AppendFormat(" and {0} {1} @{2}", parameter.Item1, parameter.Item2, parameterName);
                    dynamicParameters.Add(parameterName, parameter.Item3);
                }
            }
            sql.AppendFormat(" order by {0} limit @pageIndex,@pageSize", orderBy);
            ICollection<T2> entities = null;
            OpenSlaveConnection(conn =>
            {
                entities = conn.Query<T2>(string.Format("{0}{1}", baseSelectSql, sql.ToString()), dynamicParameters).ToList();
            });
            return entities;
        }


        public virtual ICollection<T2> GetListWithOutStatus<T2>(ICollection<Tuple<string, string, object>> parameters)
        {
            var tableName = SqlMapperExtensions.GetTableName(typeof(T2));
            var baseSelectSql = string.Format("select * from {0}  where 1 = 1", tableName);

            var sql = new StringBuilder();
            var dynamicParameters = new DynamicParameters();
            if (parameters != null)
            {
                for (var i = 0; i < parameters.Count; i++)
                {
                    var parameter = parameters.ElementAt(i);
                    var parameterName = string.Format("{0}{1}", parameter.Item1, i);
                    sql.AppendFormat(" and {0} {1} @{2}", parameter.Item1, parameter.Item2, parameterName);
                    dynamicParameters.Add(parameterName, parameter.Item3);
                }
            }
            ICollection<T2> entities = null;
            OpenSlaveConnection(conn =>
            {
                entities = conn.Query<T2>(string.Format("{0}{1}", baseSelectSql, sql.ToString()), dynamicParameters).ToList();
            });
            return entities;
        }

        public virtual ICollection<T2> GetList<T2>(string orderBy, ICollection<Tuple<string, string, object>> parameters)
        {
            var baseSelectSql = GetBaseSelectSql(typeof(T2));
            var sql = new StringBuilder();
            var dynamicParameters = new DynamicParameters();
 
            if (parameters != null)
            {
                for (var i = 0; i < parameters.Count; i++)
                {
                    var parameter = parameters.ElementAt(i);
                    var parameterName = string.Format("{0}{1}", parameter.Item1, i);
                    sql.AppendFormat(" and {0} {1} @{2}", parameter.Item1, parameter.Item2, parameterName);
                    dynamicParameters.Add(parameterName, parameter.Item3);
                }
            }
            sql.AppendFormat(" order by {0} ", orderBy);
            ICollection<T2> entities = null;
            OpenSlaveConnection(conn =>
            {
                entities = conn.Query<T2>(string.Format("{0}{1}", baseSelectSql, sql.ToString()), dynamicParameters).ToList();
            });
            return entities;
        }

        public int Delete(ICollection<Tuple<string, string, object>> parameters, bool isDelete)
        {
            var tableName = SqlMapperExtensions.GetTableName(Type);
            var allProperties = SqlMapperExtensions.TypePropertiesCache(Type);
            var computedProperties = SqlMapperExtensions.ComputedPropertiesCache(Type);
            var allPropertiesExceptComputed = allProperties.Except(computedProperties);
            var hasStatus = allPropertiesExceptComputed.FirstOrDefault(o => o.Name == "Status");

            var sql = new StringBuilder();
            if (!isDelete && hasStatus != null)
            {
                sql.AppendFormat("update {0} set Status=-1 where Status=1 ", tableName);

                //sql.AppendFormat("update {0} set LastChanged =ADDTIME(now(),'00:00:10'),Status=-1 where Status=1 ", tableName);
            }
            else
            {
                sql.AppendFormat("delete from {0} where 1=1 ", tableName);
            }
            var dynamicParameters = new DynamicParameters();
            if (parameters != null)
            {
                for (var i = 0; i < parameters.Count; i++)
                {
                    var parameter = parameters.ElementAt(i);
                    var parameterName = string.Format("{0}{1}", parameter.Item1, i);
                    sql.AppendFormat(" and {0} {1} @{2}", parameter.Item1, parameter.Item2, parameterName);
                    dynamicParameters.Add(parameterName, parameter.Item3);
                }
            }
            var deleted = 0;
            OpenConnection(conn =>
            {
                deleted = conn.Execute(sql.ToString(), dynamicParameters);
            });

            return deleted;
        }


        public int DeleteAnyStatus(ICollection<Tuple<string, string, object>> parameters, bool isDelete)
        {
            var tableName = SqlMapperExtensions.GetTableName(Type);
            var allProperties = SqlMapperExtensions.TypePropertiesCache(Type);
            var computedProperties = SqlMapperExtensions.ComputedPropertiesCache(Type);
            var allPropertiesExceptComputed = allProperties.Except(computedProperties);
            var hasStatus = allPropertiesExceptComputed.FirstOrDefault(o => o.Name == "Status");

            var sql = new StringBuilder();
            if (!isDelete && hasStatus != null)
            {
                sql.AppendFormat("update {0} set Status=-1 where 1=1 ", tableName);

                //sql.AppendFormat("update {0} set LastChanged =ADDTIME(now(),'00:00:10'),Status=-1 where Status=1 ", tableName);
            }
            else
            {
                sql.AppendFormat("delete from {0} where 1=1 ", tableName);
            }
            var dynamicParameters = new DynamicParameters();
            if (parameters != null)
            {
                for (var i = 0; i < parameters.Count; i++)
                {
                    var parameter = parameters.ElementAt(i);
                    var parameterName = string.Format("{0}{1}", parameter.Item1, i);
                    sql.AppendFormat(" and {0} {1} @{2}", parameter.Item1, parameter.Item2, parameterName);
                    dynamicParameters.Add(parameterName, parameter.Item3);
                }
            }
            var deleted = 0;
            OpenConnection(conn =>
            {
                deleted = conn.Execute(sql.ToString(), dynamicParameters);
            });

            return deleted;
        }
        public virtual void Clear()
        {
            var tableName = SqlMapperExtensions.GetTableName(Type);
            var sql = string.Format("TRUNCATE {0}", tableName);
            OpenConnection(conn => conn.Execute(sql));
        }
    }
}
