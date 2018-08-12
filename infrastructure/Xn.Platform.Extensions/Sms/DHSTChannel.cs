using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xn.Platform.Core.Extensions;

namespace Xn.Platform.Core.Sms
{
    public static class DhstChannel
    {
        /*
         <?xml version="1.0" encoding="UTF-8"?>
        <message>
            <account>test</account>
            <password>bb43a2c4081bec02fca7b72f38e63021</password>
            <msgid></msgid>
            <phones>13111111111,13222222222,13333333333</phones>
            <content>测试短信</content>
            <sign>【签名内容】</sign> //签名需要用“【】”标记
            <subcode></subcode> //扩展号码，可为空
            <sendtime>201405111230</sendtime>//定时下发时间
        </message>
         * */
        const string msgTemplate = 
@"message=<?xml version=""1.0"" encoding=""UTF-8""?><message><account>{0}</account><password>{1}</password><msgid></msgid><phones>{2}</phones><content>{3}</content><sign>【{4}】</sign><subcode></subcode><sendtime>{5}</sendtime></message>";

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="receiverList">电话列表，逗号分隔</param>
        /// <param name="content"></param>
        /// <param name="sign">短信签名</param>
        /// <param name="sendTime">定时发送时间</param>
        /// <returns></returns>
        public static SendResult SendSMS(string receiverList, string content, DateTime sendTime, string sign = "腾讯游戏竞技平台")
        {
            string account = "dh1874";
            string pwd = GetMD5String("tga1234");
            string body = string.Format(msgTemplate,
                account, pwd, receiverList, content, sign, sendTime.ToString("yyyyMMddHHmm"));
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            var responseData = client.UploadData("http://www.10690300.com/http/sms/Submit",
                Encoding.UTF8.GetBytes(body));
            var responseText = Encoding.UTF8.GetString(responseData);
            var doc = XDocument.Parse(responseText);
            var msgid = doc.Root.Descendants("msgid").First().Value;
            var result = -1;
            int.TryParse(doc.Root.Descendants("result").First().Value,out result);
            SendResult r = new SendResult { Code = result, MsgId = msgid };
            return r;
        }


        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="receiverList">电话列表，逗号分隔</param>
        /// <param name="content"></param>
        /// <param name="sign">短信签名</param>
        /// <param name="sendTime">定时发送时间</param>
        /// <returns></returns>
        public static async Task<SendResult> SendSMSAsync(string receiverList, string content, DateTime sendTime, string sign = "腾讯游戏竞技平台")
        {
            string account = "dh1874";
            string pwd = GetMD5String("tga1234");
            string body = string.Format(msgTemplate,
                account, pwd, receiverList, content, sign, sendTime.ToString("yyyyMMddHHmm"));
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            var responseData = await client.UploadDataTaskAsync("http://www.10690300.com/http/sms/Submit", Encoding.UTF8.GetBytes(body)).ConfigureAwait(false);
            var responseText = Encoding.UTF8.GetString(responseData);
            var doc = XDocument.Parse(responseText);
            var msgid = doc.Root.Descendants("msgid").First().Value;
            var result = -1;
            int.TryParse(doc.Root.Descendants("result").First().Value, out result);
            SendResult r = new SendResult { Code = result, MsgId = msgid };
            return r;
        }

        static MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        static string GetMD5String(string original)
        {
            var encrypted = md5.ComputeHash(Encoding.ASCII.GetBytes(original));
            return CryptoExtensions.BinaryToHex(encrypted).ToLower();
        }

        public struct SendResult
        {
            public string MsgId { get; set; }
            public int Code { get; set; }
            static Dictionary<int, string> description = new Dictionary<int, string>{
                {0,"提交成功"},
                {1,"账号无效"},
                {2,"密码错误"},
                {3,"msgid 不唯一"},
                {4,"存在无效手机号码"},
                {5,"手机号码个数超过最大限制"},
                {6,"短信内容超过最大限制"},
                {7,"扩展子号码无效"},
                {8,"发送时间格式无效"},
                {9,"请求来源地址无效"},
                {12,"订购关系无效"},
                {13,"签名无效"},
                {14,"无效的手机子码"},
                {15,"产品不存在"},
                {16,"号码个数小于最小限制"},
                {17,"超出流量监控"},
                {18,"业务标识无效"},
                {19,"用户被禁发"},
                {20,"ip 鉴权失败"},
                {21,"短信内容为空"},
                {97,"接入方式错误"},
                {98,"系统繁忙"},
                {99,"消息格式错误"}
            };
            public static string GetCodeDesc(int code)
            {
                if(description.ContainsKey(code))
                return description[code];
                else
                return "无效的状态码";

            }

            public string GetCurrentCodeDesc()
            {
                return GetCodeDesc(this.Code);
            }
        }
    }
}
