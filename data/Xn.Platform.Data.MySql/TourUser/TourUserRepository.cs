using Xn.Platform.Domain.TourUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core;
using Xn.Platform.Domain;
using Dapper;

namespace Xn.Platform.Data.MySql.TourUser
{
    public class TourUsersRepository : AbstractRepository<TourUserModel>
    {
        public TourUsersRepository()
        {
            ConnectionString = ConfigSetting.ConnectionLongzhuSportsEntities;
            SlaveConnectionString = ConfigSetting.ConnectionLongzhuSportsEntitiesReadOnly;
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TourUserModel GetInfo(string Id)
        {
            var parameter = new List<Tuple<string, string, object>>();
            parameter.Add(new Tuple<string, string, object>("Id", "=", Id));
            var info = GetList<TourUserModel>(0, 1, "id desc", parameter).FirstOrDefault();
            return info;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagedEntity<TourUserModel> PageList(TourUserRequest request)
        {
            var parameter = new List<Tuple<string, string, object>>();
            parameter.Add(new Tuple<string, string, object>("status", "=", 1));
            if (request.username != null)
                parameter.Add(new Tuple<string, string, object>("username", "like", string.Concat("%", request.username + "%")));
            if (request.ToSort)
                request.OrderBy += " desc";

            var entity = GetPagedEntity<TourUserModel>(request.PageIndex > 0 ? request.PageIndex - 1 : request.PageIndex, request.PageSize, request.OrderBy, parameter);
            return entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Add(TourUserModel entity)
        {
            InsertNoDefaultId(entity);
            return !string.IsNullOrEmpty(entity.id);
            
            /*
            // var parameter = new List<Tuple<string, string, object>>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_tour_user(");
            strSql.Append("id,mobile,address,nationality,passport,birthday,source,picture_url,status,create_by,create_time,username,deviceID,token,smsCode,invitation_code,father_code,followNum,qq_number,fansNum,praisedNum,dynamicNum,idFace,idBack,city,prvinice,unionid,wechat,password,nick_name,sex,real_name");
            strSql.Append(") values (");
            strSql.Append("@id,@mobile,@address,@nationality,@passport,@birthday,@source,@picture_url,@status,@create_by,@create_time,@username,@deviceID,@token,@smsCode,@invitation_code,@father_code,@followNum,@qq_number,@fansNum,@praisedNum,@dynamicNum,@idFace,@idBack,@city,@prvinice,@unionid,@wechat,@password,@nick_name,@sex,@real_name");
            strSql.Append(") ");
            var parameters = new
            {
                entity.id,
                entity.mobile,
                entity.address,
                entity.nationality,
                entity.passport,
                entity.birthday,
                entity.source,
                entity.picture_url,
                entity.status,
                entity.create_by,
                entity.create_time,
                entity.username,
                entity.deviceID,
                entity.token,
                entity.smsCode,
                entity.invitation_code,
                entity.father_code,
                entity.followNum,
                entity.qq_number,
                entity.fansNum,
                entity.praisedNum,
                entity.dynamicNum,
                entity.idFace,
                entity.idBack,
                entity.city,
                entity.prvinice,
                entity.unionid,
                entity.wechat,
                entity.password,
                entity.nick_name,
                entity.sex,
                entity.real_name,
            };
            var result = 0;
            OpenConnection(conn => result = conn.Execute(strSql.ToString(), parameters));
            return result;*/
        }
        public int Updates(TourUserModel entity)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_tour_user set ");
            strSql.Append(" id = @id , ");
            strSql.Append(" mobile = @mobile , ");
            strSql.Append(" address = @address , ");
            strSql.Append(" nationality = @nationality , ");
            strSql.Append(" passport = @passport , ");
            strSql.Append(" birthday = @birthday , ");
            strSql.Append(" source = @source , ");
            strSql.Append(" picture_url = @picture_url , ");
            strSql.Append(" username = @username , ");
            strSql.Append(" modify_by = @modify_by , ");
            strSql.Append(" modify_time = @modify_time , ");
            strSql.Append(" deviceID = @deviceID , ");
            strSql.Append(" token = @token , ");
            strSql.Append(" smsCode = @smsCode , ");
            strSql.Append(" invitation_code = @invitation_code , ");
            strSql.Append(" father_code = @father_code , ");
            strSql.Append(" followNum = @followNum , ");
            strSql.Append(" qq_number = @qq_number , ");
            strSql.Append(" fansNum = @fansNum , ");
            strSql.Append(" praisedNum = @praisedNum , ");
            strSql.Append(" dynamicNum = @dynamicNum , ");
            strSql.Append(" idFace = @idFace , ");
            strSql.Append(" idBack = @idBack , ");
            strSql.Append(" city = @city , ");
            strSql.Append(" prvinice = @prvinice , ");
            strSql.Append(" unionid = @unionid , ");
            strSql.Append(" wechat = @wechat , ");
            strSql.Append(" password = @password , ");
            strSql.Append(" nick_name = @nick_name , ");
            strSql.Append(" sex = @sex , ");
            strSql.Append(" real_name = @real_name  ");
            strSql.Append(" where id=@id  ");
            var parameters = new
            {
                entity.id,
                entity.mobile,
                entity.address,
                entity.nationality,
                entity.passport,
                entity.birthday,
                entity.source,
                entity.picture_url,
                entity.username,
                entity.modify_by,
                entity.modify_time,
                entity.deviceID,
                entity.token,
                entity.smsCode,
                entity.invitation_code,
                entity.father_code,
                entity.followNum,
                entity.qq_number,
                entity.fansNum,
                entity.praisedNum,
                entity.dynamicNum,
                entity.idFace,
                entity.idBack,
                entity.city,
                entity.prvinice,
                entity.unionid,
                entity.wechat,
                entity.password,
                entity.nick_name,
                entity.sex,
                entity.real_name,
            };
            var result = 0;
            OpenConnection(conn => result = conn.Execute(strSql.ToString(), parameters));
            return result;
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int Delete(string Id)
        {
            string sql = "Update t_tour_user set Status=0 where Id=@Id";
            var result = 0;
            OpenConnection(conn => result = conn.Execute(sql, new { Id }));
            return result;
        }
    }
}
