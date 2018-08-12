using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Xn.Platform.Core;
using Xn.Platform.Core.Extensions;
using Xn.Platform.Domain.Report;
using MySql.Data.MySqlClient;

namespace Xn.Platform.Data.MySql.Report
{
    public abstract class ReportRepository<T> : AbstractRepository<T> where T : ReportEntity, new()
    {
        protected string CurrentTableName;
        const int MySqlBatchCount = 1000;

        protected ReportRepository()
        {
            ConnectionString = ConfigSetting.ConnectionReport;
            SlaveConnectionString = ConfigSetting.ConnectionReportReadOnly;
            CurrentTableName = SqlMapperExtensions.GetTableName(Type);
        }

        public void WriteMySqlDatabase(ICollection<T> entities, IDictionary<string, string> parameters)
        {
            WriteMysqlDatabaseBegin(parameters);
            var executeNonQuery = 0;
            int count = 0;
            var sb = new StringBuilder();
            sb.Append(InsertMySqlSql);
            foreach (var entity in entities)
            {
                sb.Append(WriteMySqlEntity(entity, parameters));
                if (++count % MySqlBatchCount == 0)
                {
                    OpenConnection(conn =>
                    {
                        var command = conn.CreateCommand();
                        command.CommandTimeout = 3600;
                        command.CommandText = sb.Remove(sb.Length - 1, 1).ToString();
                        executeNonQuery += command.ExecuteNonQuery();
                    });
                    sb.Clear();
                    sb.Append(InsertMySqlSql);
                }
            }
            if (sb.Length != InsertMySqlSql.Length)
            {
                OpenConnection(conn =>
                {
                    var command = conn.CreateCommand();
                    command.CommandTimeout = 3600;
                    command.CommandText = sb.Remove(sb.Length - 1, 1).ToString();
                    executeNonQuery += command.ExecuteNonQuery();
                });
                sb.Clear();
            }
            //var recordCount = entities.Count;
            //var result = executeNonQuery == recordCount;
            //var str = new StringBuilder();
            //foreach (var parameter in parameters)
            //{
            //    str.AppendFormat("({0}:{1})", parameter.Key, parameter.Value);
            //}
            //Log.Info(string.Format("数据{0}(总数{1})写入MySql数据库{2}(执行行数{3})完成,执行结果{4}", str.ToString(), recordCount, CurrentTableName, executeNonQuery, result));
        }
        protected readonly string MySqlDateFormat = "yyyy-MM-dd";
        protected readonly string MySqlDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        protected virtual string InsertMySqlSql
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        protected virtual string WriteMySqlEntity(T entity, IDictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }

        public virtual void WriteMysqlDatabaseBegin(IDictionary<string, string> parameters)
        {
            VerificationAndDelete(CurrentTableName, parameters, true);
        }

        public bool VerificationAndDelete(string tableName, IDictionary<string, string> parameters, bool isDelete)
        {
            var result = false;
            DynamicParameters dynamicParameters = new DynamicParameters();
            IList<string> whereList = new List<string>();
            foreach (var parameter in parameters)
            {
                if (parameter.Key == "day")
                {
                    dynamicParameters.Add(parameter.Key, parameter.Value.AsDateTime().ToString(MySqlDateTimeFormat));
                }
                else
                {
                    dynamicParameters.Add(parameter.Key, parameter.Value.AsInt());
                }
                whereList.Add(string.Format("{0}=@{0}", parameter.Key));
            }
            var sql = string.Join(" and ", whereList);
            var stateSql = string.Format("select 'x' from {0} where {1} limit 1", tableName, sql);
            var deleteSql = string.Format("delete from {0} where {1}", tableName, sql);
            //if (!isDelete)
                //Log.Info(stateSql);
            OpenConnection(conn =>
            {
                var state = conn.Query<string>(stateSql, dynamicParameters, commandTimeout: 3600).FirstOrDefault();
                if (state != null && state.ToString() == "x")
                {
                    result = true;
                    if (isDelete)
                    {
                        var deleteCount = conn.Execute(deleteSql, dynamicParameters, commandTimeout:3600);
                        var str = new StringBuilder();
                        foreach (var parameter in parameters)
                        {
                            str.AppendFormat("({0}:{1})", parameter.Key, parameter.Value);
                        }
                        //Log.Info(string.Format("删除数据库{0}参数是{1}的{2}条记录", tableName, str.ToString(), deleteCount));
                    }
                }
            });
            return result;
        }

        public void Truncate(IDictionary<string, string> parameters)
        {
            using (var connection = new MySqlConnection(ConfigSetting.ConnectionReportDDL))
            {
                connection.Open();
                connection.Execute(string.Format("truncate table {0}", CurrentTableName));
            }
            //Log.Info(string.Format("清空数据库{0}时间是{1}的所有记录", CurrentTableName, parameters["day"]));
        }

        public List<T> GetDailyList(DateTime day)
        {
            string sql = string.Format(@"select * from {0} where Day=@day", CurrentTableName);
            var results = new List<T>();
            OpenSlaveConnection(conn => results = conn.Query<T>(sql, new { day }, commandTimeout: 3600).ToList());
            return results;
        }
    }
}
