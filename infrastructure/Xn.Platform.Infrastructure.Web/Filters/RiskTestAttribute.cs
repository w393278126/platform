using  Xn.Platform.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Xn.Platform.Infrastructure.Web.Filters
{
    public class RiskTestAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContext = filterContext.HttpContext;

            var risk_challenge = httpContext.Request.QueryString["risk_challenge"];
            var risk_policy = httpContext.Request.QueryString["risk_policy"];
            var risk_seccode = httpContext.Request.QueryString["risk_seccode"];
            var risk_token = httpContext.Request.QueryString["risk_token"];
            var risk_validate = httpContext.Request.QueryString["risk_validate"];

            if (string.IsNullOrEmpty(risk_challenge) && string.IsNullOrEmpty(risk_policy) &&
                string.IsNullOrEmpty(risk_seccode) && string.IsNullOrEmpty(risk_token) && string.IsNullOrEmpty(risk_validate)
                )
            {//接口第一次调用  需要调用GRPC验证  RiskBarrier.Prepare

            }
            else
            {
                if (string.IsNullOrEmpty(risk_challenge) || string.IsNullOrEmpty(risk_policy) ||
                    string.IsNullOrEmpty(risk_seccode) || string.IsNullOrEmpty(risk_token) || string.IsNullOrEmpty(risk_validate)
                    )
                {
                    filterContext.Result = Error(ResultCode.RiskTestMissingParameter, RiskPolicyEnum.NoSet);
                }
                else
                {
                    //二次验证 极验 RiskBarrier.Verify

                }
            }

        }


        private JsonpResult Error(ResultCode resultCode, RiskPolicyEnum policyEnum, RiskExtraInfo ext = null)
        {
            var result = new RiskTestResponse();
            result.Code = resultCode;
            result.risk = new RiskInfo
            {
                policy = (int)policyEnum,
                extra = ext,
            };
            return JsonpResult.AsCamelCaseJson(result);
        }
    }

    public class RiskTestResponse : ResultWithCodeEntity
    {
        /// <summary>
        /// 安全风控数据
        /// </summary>
        public RiskInfo risk { get; set; }
    }
    public class RiskInfo
    {
        /// <summary>
        /// 风险策略	
        /// 1.绑定手机,2.极验,3.手机短信验证码,4.踢出登陆态重新登陆
        /// </summary>
        public int policy { get; set; }

        /// <summary>
        /// 任意拓展类型	
        /// </summary>
        public RiskExtraInfo extra { get; set; }
    }

    /// <summary>
    /// 安全风控数据
    /// </summary>
    public class RiskExtraInfo
    {
        /// <summary>
        /// 极验所需数据
        /// </summary>
        public string capatcha { get; set; }

        /// <summary>
        /// token
        /// </summary>
        public string token { get; set; }
    }

    /// <summary>
    /// 风险策略	
    /// 1.绑定手机,2.极验,3.手机短信验证码,4.踢出登陆态重新登陆
    /// </summary>
    public enum RiskPolicyEnum
    {
        NoSet = 0,
        BindPhone = 1,
        Geetest = 2,
        SMS = 3,
        Logout = 4,
    }

    /// <summary>
    /// 检测场景
    /// </summary>
    public enum RiskScene
    {
        //礼物	1
        Gift = 1,
        //红包	2
        Redbag = 2,
        //礼卷	3
        Coupon = 3,
        //宝箱	4
        Box = 4,
        //签到	5
        Sign = 5,

    }
}
