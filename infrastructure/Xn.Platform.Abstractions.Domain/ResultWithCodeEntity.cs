using System.Collections.Generic;

namespace Xn.Platform.Abstractions.Domain
{
    public enum ResultCode
    {
        // 常规参数错误 100000-199999
        Success = 0,

        Cookie = 10,
        UserNotExist = 11,
        ParameterError = 12,
        ExceptionError = 13,
        ValidateCodeError = 13,
        DefaultError = 100000,

        // 用户模块相关错误 100001-199999 
       
        // 订单模块参数错误 200000-299999

        // 支付模块参数错误 300000-399999

    }

    public class ResultWithCodeEntity
    {
        private static readonly Dictionary<ResultCode, string> ErrorMessage = new Dictionary<ResultCode, string>
        {
            #region 信息配置

            {ResultCode.DefaultError, "系统错误"},
            {ResultCode.Success, "操作成功"},
            {ResultCode.Cookie, "用户未登录"},
            {ResultCode.UserNotExist, "用户不存在"},
            {ResultCode.ParameterError, "参数不能为空"},
            {ResultCode.ExceptionError, "系统错误"},
            #endregion
        };

        /// <summary>
        /// 对用户显示错误信息(需要经过产品确认!!!!)
        /// </summary>
        private static readonly Dictionary<ResultCode, string> ErrorDisplayMessage = new Dictionary<ResultCode, string>
        {
            #region 信息配置
            {ResultCode.Success, "操作成功"},
            {ResultCode.DefaultError, "系统错误"},
            {ResultCode.Cookie, "用户未登录"},
            {ResultCode.UserNotExist, "用户不存在"},
            {ResultCode.ParameterError, "参数不能为空"},
            #endregion
        };

        /// <summary>
        /// 表示该错误的编号。这个属性通常表示HTTP响应码。
        /// </summary>
        public ResultCode Code { get; set; } = ResultCode.Success;
        /// <summary>
        /// 一个人类可读的信息，提供有关错误的详细信息。
        /// </summary>
        public string Message => DefaultErrorMessage(Code);

        /// <summary>
        /// 对用户显示错误信息
        /// </summary>
        public string DisplayMessage => DefaultErrorDisplayMessage(Code);

        public static string DefaultErrorMessage(ResultCode code)
        {
            if (!ErrorMessage.ContainsKey(code))
            {
                return ErrorMessage[ResultCode.DefaultError];
            }
            return ErrorMessage[code];
        }

        /// <summary>
        /// 对用户显示错误信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string DefaultErrorDisplayMessage(ResultCode code)
        {
            if (!ErrorDisplayMessage.ContainsKey(code))
            {
                return string.Empty;
            }
            return ErrorDisplayMessage[code];
        }
    }


    public class ResultWithCodeEntity<T> : ResultWithCodeEntity
    {
        public T Data { get; set; }
    }


    public class Result
    {
        public static ResultWithCodeEntity Success()
        {
            return new ResultWithCodeEntity { Code = ResultCode.Success };
        }

        public static ResultWithCodeEntity<T> Success<T>(T data)
        {
            return new ResultWithCodeEntity<T> { Code = ResultCode.Success, Data = data };
        }

        public static ResultWithCodeEntity Error(ResultCode code)
        {
            return new ResultWithCodeEntity { Code = code };
        }

        public static ResultWithCodeEntity<T> Error<T>(ResultCode code)
        {
            return new ResultWithCodeEntity<T> { Code = code, Data = default(T) };
        }

        public static ResultWithCodeEntity<T> Error<T>(ResultCode code, T data)
        {
            return new ResultWithCodeEntity<T> { Code = code, Data = data };
        }
    }
}
