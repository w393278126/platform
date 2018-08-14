using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core.Data;
using Xn.Platform.Domain;

namespace Xn.Platform.Domain.TourUser
{
    [Table("t_tour_user")]
    public class TourUserModel
    {
        /// <summary>
        /// id
        /// </summary>		
        public string id { get; set; }
        /// <summary>
        /// username
        /// </summary>		
        public string username { get; set; }
        /// <summary>
        /// qq_number
        /// </summary>		
        public string qq_number { get; set; }
        /// <summary>
        /// unionid
        /// </summary>		
        public string unionid { get; set; }
        /// <summary>
        /// wechat
        /// </summary>		
        public string wechat { get; set; }
        /// <summary>
        /// password
        /// </summary>		
        public string password { get; set; }
        /// <summary>
        /// nick_name
        /// </summary>		
        public string nick_name { get; set; }
        /// <summary>
        /// sex
        /// </summary>		
        public string sex { get; set; }
        /// <summary>
        /// real_name
        /// </summary>		
        public string real_name { get; set; }
        /// <summary>
        /// mobile
        /// </summary>		
        public string mobile { get; set; }
        /// <summary>
        /// address
        /// </summary>		
        public string address { get; set; }
        /// <summary>
        /// nationality
        /// </summary>		
        public string nationality { get; set; }
        /// <summary>
        /// passport
        /// </summary>		
        public string passport { get; set; }
        /// <summary>
        /// birthday
        /// </summary>		
        public string birthday { get; set; }
        /// <summary>
        /// source
        /// </summary>		
        public string source { get; set; }
        /// <summary>
        /// picture_url
        /// </summary>		
        public string picture_url { get; set; }
        /// <summary>
        /// status
        /// </summary>		
        public string status { get; set; }
        /// <summary>
        /// create_by
        /// </summary>		
        public int create_by { get; set; }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>		
        public DateTime create_time { get; set; }
        /// <summary>
        /// modify_by
        /// </summary>		
        public int modify_by { get; set; }
        /// <summary>
        /// modify_time
        /// </summary>		
        public DateTime modify_time { get; set; }
        /// <summary>
        /// deviceID
        /// </summary>		
        public string deviceID { get; set; }
        /// <summary>
        /// last_login_time
        /// </summary>		
        public DateTime last_login_time { get; set; }
        /// <summary>
        /// token
        /// </summary>		
        public string token { get; set; }
        /// <summary>
        /// smsCode
        /// </summary>		
        public string smsCode { get; set; }
        /// <summary>
        /// last_smscode_time
        /// </summary>		
        public DateTime last_smscode_time { get; set; }
        /// <summary>
        /// invitation_code
        /// </summary>		
        public string invitation_code { get; set; }
        /// <summary>
        /// father_code
        /// </summary>		
        public string father_code { get; set; }
        /// <summary>
        /// followNum
        /// </summary>		
        public int followNum { get; set; }
        /// <summary>
        /// fansNum
        /// </summary>		
        public int fansNum { get; set; }
        /// <summary>
        /// praisedNum
        /// </summary>		
        public int praisedNum { get; set; }
        /// <summary>
        /// dynamicNum
        /// </summary>		
        public int dynamicNum { get; set; }
        /// <summary>
        /// idFace
        /// </summary>		
        public string idFace { get; set; }
        /// <summary>
        /// idBack
        /// </summary>		
        public string idBack { get; set; }
        /// <summary>
        /// city
        /// </summary>		
        public string city { get; set; }
        /// <summary>
        /// prvinice
        /// </summary>		
        public string prvinice { get; set; }
    }

    public class TourUserRequest
    {
        public class PageResult: BaseQueryModel
        {
           
        }
        

    }
}
