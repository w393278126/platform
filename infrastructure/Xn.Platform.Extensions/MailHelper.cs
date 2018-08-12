using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using Logging.Client;
using Xn.Platform.Core.Logs;
using Xn.Home.Utils.Configuration;

namespace Xn.Platform.Core
{
    public static class MailHelper
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(MailHelper));
        private static Item configurations;

        private static Item Configurations
        {
            get
            {
                if (configurations == null) configurations = MailManager.Get("feedback");    
                if (configurations == null) throw new SettingsPropertyNotFoundException("没有在配置文件中配置Feedback邮件服务");
                return configurations;
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">主题</param>
        /// <param name="toEmail">接受邮件地址</param>
        /// <param name="body">类容</param>
        /// <param name="isBodyHtml">是否Html</param>
        public static int SendMail(string subject, string toEmail, string body, bool isBodyHtml)
        {
            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress(Configurations.From);
            mailMsg.To.Add(toEmail);
            mailMsg.Subject = subject;
            mailMsg.Body = body;
            mailMsg.BodyEncoding = Encoding.UTF8;
            mailMsg.IsBodyHtml = isBodyHtml;
            mailMsg.Priority = MailPriority.High;

            SmtpClient smtp = new SmtpClient();
            // 提供身份验证的用户名和密码 
            // 网易邮件用户可能为：username password 
            // Gmail 用户可能为：username@gmail.com password 
            smtp.Credentials = new NetworkCredential(Configurations.From, Configurations.Password);
            smtp.Port = Configurations.Port; // Gmail 使用 465 和 587 端口 
            smtp.Host = configurations.Host; // 如 smtp.163.com, smtp.gmail.com 
            smtp.EnableSsl = false; // 如果使用GMail，则需要设置为true 
            smtp.SendCompleted += new SendCompletedEventHandler(SendMailCompleted);
            try
            {
                smtp.Send(mailMsg);
                return 1;
            }
            catch (SmtpException ex)
            {
                _logger.Error(ex);
                return 0;
            } 
        }

        /// <summary>
        /// 发送邮件，无需账号和密码，在特定服务器上使用即可
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="toEmail"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        /// <returns></returns>
        public static int SendMailNOAccount(string subject, string toEmail, string body, bool isBodyHtml)
        {
            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress("noreply@service.Xn.cn");
            mailMsg.To.Add(toEmail);
            mailMsg.Subject = subject;
            mailMsg.Body = body;
            mailMsg.BodyEncoding = Encoding.UTF8;
            mailMsg.IsBodyHtml = isBodyHtml;
            mailMsg.Priority = MailPriority.High;

            SmtpClient smtp = new SmtpClient();

            smtp.Port = 25; // Gmail 使用 465 和 587 端口 
            smtp.Host = "mx1.qq.com"; // 如 smtp.163.com, smtp.gmail.com 
            smtp.EnableSsl = false; // 如果使用GMail，则需要设置为true 
            smtp.SendCompleted += new SendCompletedEventHandler(SendMailCompleted);
            try
            {
                smtp.Send(mailMsg); 
                return 1;
            }
            catch (SmtpException ex)
            {
                _logger.Error(ex);
                return 0;
            }
        }

        public static int SendMailNOAccountRightNow(string subject, string toEmail, string body, bool isBodyHtml)
        {
            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress("noreply@service.Xn.cn");
            mailMsg.To.Add(toEmail);
            mailMsg.Subject = subject;
            mailMsg.Body = body;
            mailMsg.BodyEncoding = Encoding.UTF8;
            mailMsg.IsBodyHtml = isBodyHtml;
            mailMsg.Priority = MailPriority.High;

            SmtpClient smtp = new SmtpClient();

            smtp.Port = 25; // Gmail 使用 465 和 587 端口 
            smtp.Host = "mx1.qq.com"; // 如 smtp.163.com, smtp.gmail.com 
            smtp.EnableSsl = false; // 如果使用GMail，则需要设置为true 
            try
            {
                smtp.Send(mailMsg);
                return 1;
            }
            catch (SmtpException ex)
            {
                _logger.Error(ex);
                return 0;
            }
        }




        static void SendMailCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MailMessage mailMsg = (MailMessage)e.UserState;
            string subject = mailMsg.Subject;
            if (e.Cancelled) // 邮件被取消 
            {
                _logger.Error(subject + " 被取消。");
            }
            if (e.Error != null)
            {
                _logger.Error("错误：" + e.Error.ToString());
            }
        }
    }
}
