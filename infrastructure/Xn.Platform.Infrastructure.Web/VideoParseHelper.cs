using System;
using System.Text.RegularExpressions;
using System.Xml;
using Xn.Platform.Core.Extensions;

namespace Xn.Platform.Infrastructure.Web
{

    /// <summary>
    /// 视频信息
    /// </summary>
    public class VideoResult
    {
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string Thumbnail { get; set; }
        /// <summary>
        /// 视频地址
        /// </summary>
        public string PlayerUrl { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 状态信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 播放时长
        /// </summary>
        public int PlayTime { get; set; }
    }

    /// <summary>
    /// 视频解析帮助类
    /// </summary>
    public class VideoParseHelper
    {
        #region Get Domain by Url
        public static string GetDomainByUrl(string url)
        {
            string pattern = "http://[a-z0-9]*.(?<domain>[a-z0-9]+).[com|cn]*/";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(url))
            {
                return regex.Match(url).Groups["domain"].Value.ToLower();
            }
            return null;
        }
        #endregion

        #region 获取视频信息
        /// <summary>
        /// 获取视频信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static VideoResult GetVideoImage(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return new VideoResult { Status = 2, Message = "地址不能为空." };
            }
            string domain = GetDomainByUrl(url);
            if (string.IsNullOrWhiteSpace(domain))
            {
                return new VideoResult { Status = 3, Message = "你输入的地址暂时不支持." };
            }
            if (url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }
            if (domain.ToLower() == "ku6")
            {
                url = url.Substring(0, url.IndexOf(".html") + 5);
            }
            var result = new VideoResult();
            switch (domain.ToLower())
            {
                case "youku":
                    result = GetYouKuVideoInfo(url);
                    break;
                case "ku6":
                    result = GetKu6VideoInfo(url);
                    break;
                case "56":
                    result = Get56VideoInfo(url);
                    break;
                case "tudou":
                    result = GetTuDouVideoInfo(url);
                    break;
                case "qq":
                    result = GetQQVideoInfo(url);
                    break;
                default:
                    result = new VideoResult { Status = 3, Message = "你输入的地址无法识别." };
                    break;
            }
            return result;
        }
        #endregion

        #region 获取优酷视频信息
        /// <summary>
        /// 如果是匹配模式1的情况，先通过匹配模式1取到数据分析出.html的Url地址
        /// 然后通过.html地址分析出接口所要使用的key，调用接口取到视频的缩略图.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static VideoResult GetYouKuVideoInfo(string url)
        {
            /*
            var result = new VideoResult();
            RequestHelper requestHelper = null;
            //匹配模式1
            string pThumbnail = @"screenshot=(?<image>.*)""\s?target";
            string pPlayerUrl = @"<input\s?type=""text""\s?class=""form_input\sform_input_s""\sid=""link2""\s?value=""(?<swf>.*)""";
            string pTitle = @"<meta\s?name=""title""\s?content=""(?<title>.*)"">";
            requestHelper = new RequestHelper();
            string reInfo = string.Empty;
            string text = requestHelper.GetContentToString(url, ref reInfo);
            if (!string.IsNullOrWhiteSpace(text))
            {
                Regex rThumbnail = new Regex(pThumbnail);
                Regex rPlayerUrl = new Regex(pPlayerUrl);
                Regex rTitle = new Regex(pTitle);
                if (rThumbnail.IsMatch(text) && rPlayerUrl.IsMatch(text) && rTitle.IsMatch(text))
                {
                    result.PlayTime = 0;

                    result.Status = 0;
                    result.Thumbnail = rThumbnail.Match(text).Groups["image"].Value;
                    result.PlayerUrl = rPlayerUrl.Match(text).Groups["swf"].Value;
                    result.Title = rTitle.Match(text).Groups["title"].Value.Replace(" - 视频 - 优酷视频 - 在线观看", string.Empty);
                    result.Desc = result.Title;
                    result.Message = "解析成功.";
                }
                else
                {
                    result.Status = 1;
                    result.Message = "你输入的地址无法识别.";
                }
            }
            else
            {
                result.Status = 1;
                result.Message = "你输入的地址无法识别.";
            }
            return result;
            * */



            var youkuUrl = "https://openapi.youku.com/v2/videos/show_basic.json?client_id=ddd3ac121a93f9dd&video_url=" + url;

            var result = new VideoResult();
            string reInfo = string.Empty;
            string text = youkuUrl.GetContentToString(ref reInfo);


            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    dynamic json = text.ToNewtonsoftObject<dynamic>();

                    result.PlayTime = Convert.ToInt32((double)json["duration"]);

                    result.Status = 0;
                    result.Thumbnail = json["thumbnail"];
                    result.PlayerUrl = json["player"];
                    result.Title = json["title"];
                    result.Desc = json["description"];
                    result.Message = "解析成功.";

                }
                catch (Exception)
                {
                    result.Status = 1;
                    result.Message = "你输入的地址无法识别.";
                }
            }
            else
            {
                result.Status = 1;
                result.Message = "你输入的地址无法识别.";
            }
            return result;



        }
        #endregion




        #region 获取土豆视频信息
        private static VideoResult GetTuDouVideoInfo(string url)
        {
            var result = new VideoResult();
            var tudou = new TuDouVideoAPI();
            string text = tudou.GetVideoInfo(url);
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    dynamic d = text.ToNewtonsoftObject<dynamic>();
                    var json = ((d["multiResult"])["results"])[0];
                    string image = json["picUrl"];
                    string swf = json["outerPlayerUrl"];
                    string title = json["title"];
                    if (!string.IsNullOrWhiteSpace(image) && !string.IsNullOrWhiteSpace(swf) && !string.IsNullOrWhiteSpace(title))
                    {
                        result.PlayTime = 0;

                        result.Status = 0;
                        result.Thumbnail = image;
                        result.PlayerUrl = swf;
                        result.Title = title;
                        result.Desc = result.Title;
                        result.Message = "解析成功.";
                    }
                    else
                    {
                        result.Status = 1;
                        result.Message = "你输入的地址无法识别.";
                    }
                }
                catch
                {
                    result.Status = 1;
                    result.Message = "你输入的地址无法识别.";
                }
            }
            else
            {
                result.Status = 1;
                result.Message = "你输入的地址无法识别.";
            }
            return result;
        }
        #endregion

        #region 获取56视频信息
        private static VideoResult Get56VideoInfo(string url)
        {
            var result = new VideoResult();
            Regex regex = null;
            string key = null;

            string patten = "v_(?<key>.*).html";

            regex = new Regex(patten);
            if (regex.IsMatch(url))
            {
                key = regex.Match(url).Groups["key"].Value;
            }

            patten = "vid-(?<key>.*).html";
            regex = new Regex(patten);
            if (regex.IsMatch(url))
            {
                key = regex.Match(url).Groups["key"].Value;
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                return new VideoResult { Status = 3, Message = "你输入的地址无法识别." };
            }

            //通过接口Api 和 key整合新的url地址获取数据
            string apiUrl = string.Format("http://vxml.56.com/json/{0}/?src=out", key);
            
            string reInfo = string.Empty;
            string text = apiUrl.GetContentToString(ref reInfo);
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    dynamic d = text.ToNewtonsoftObject<dynamic>();
                    var json = d["info"];
                    string image = json["img"];
                    string swf = string.Format("http://player.56.com/v_{0}.swf", key);
                    string title = json["Subject"];
                    if (!string.IsNullOrWhiteSpace(image) && !string.IsNullOrWhiteSpace(title))
                    {
                        result.PlayTime = 0;

                        result.Status = 0;
                        result.Thumbnail = image;
                        result.PlayerUrl = swf;
                        result.Title = title;
                        result.Desc = result.Title;
                        result.Message = "解析成功.";
                    }
                    else
                    {
                        result.Status = 1;
                        result.Message = "你输入的地址无法识别.";
                    }
                }
                catch
                {
                    result.Status = 1;
                    result.Message = "你输入的地址无法识别.";
                }
            }
            else
            {
                result.Status = 1;
                result.Message = "你输入的地址无法识别.";
            }
            return result;
        }
        #endregion

        #region 获取Ku6视频信息
        private static VideoResult GetKu6VideoInfo(string url)
        {
            var result = new VideoResult();
            //匹配模式1
            string pThumbnail = @"cover\s?:\s?""(?<image>.*)"",\s?data";
            string pPlayerUrl = @"<input\s?id=""swf_url""\s?name=""codes""\s?class=""radio_A""\s?type=""radio""\s?value=""(?<swf>.*.swf)""\s?checked";
            string pTitle = @"<meta\s?name=""title""\s?content=""(?<title>.*)""\s?/>";
            var reInfo = string.Empty;
            string text = url.GetContentToString("gb2312", ref reInfo);
            if (!string.IsNullOrWhiteSpace(text))
            {
                Regex rThumbnail = new Regex(pThumbnail);
                Regex rPlayerUrl = new Regex(pPlayerUrl);
                Regex rTitle = new Regex(pTitle);
                if (rThumbnail.IsMatch(text) && rPlayerUrl.IsMatch(text) && rTitle.IsMatch(text))
                {
                    result.PlayTime = 0;

                    result.Status = 0;
                    result.Thumbnail = rThumbnail.Match(text).Groups["image"].Value;
                    result.PlayerUrl = rPlayerUrl.Match(text).Groups["swf"].Value;
                    result.Title = rTitle.Match(text).Groups["title"].Value;
                    result.Desc = result.Title;
                    result.Message = "解析成功.";
                }
                else
                {
                    result.Status = 1;
                    result.Message = "你输入的地址无法识别.";
                }
            }
            else
            {
                result.Status = 1;
                result.Message = "你输入的地址无法识别.";
            }
            return result;
        }
        #endregion

        #region 获取腾讯视频信息
        private static VideoResult GetQQVideoInfo(string url)
        {
            var result = new VideoResult();

            string filename = "http://sns.video.qq.com/tvideo/fcgi-bin/vshare?_out=106&url=" + url;

            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.XmlResolver = null;
                xmlDoc.Load(filename);
                //读取Activity节点下的数据。SelectSingleNode匹配第一个Activity节点
                XmlNode root = xmlDoc.SelectSingleNode("//root/result");//当节点Workflow带有属性是，使用SelectSingleNode无法读取        
                if (root != null)
                {
                    if (root.SelectSingleNode("pic306x204") != null)
                    {
                        result.Thumbnail = (root.SelectSingleNode("pic306x204")).InnerText;
                    }
                    else
                    {
                        result.Thumbnail = (root.SelectSingleNode("coverurl")).InnerText;
                    }
                    
                    result.PlayerUrl = root.SelectSingleNode("flash").InnerText;
                    result.Title = root.SelectSingleNode("title").InnerText;
                    result.Desc = root.SelectSingleNode("desc").InnerText;
                    result.PlayTime = Convert.ToInt32(root.SelectSingleNode("playtime").InnerText);

                    result.Message = "解析成功.";
                    result.Status = 0;
                }
                else
                {
                }
            }
            catch (Exception)
            {
                result.Status = 1;
                result.Message = "你输入的地址无法识别.";
            }

            return result;


            /*//Obsolescence
            var pQQVideo = @" _hot=""cover.list_5.share_qzone"" href=""javascript:;"" title=""分享到QQ空间"" vid=""(?<swf>.*)"" info=""(?<title>.*)"" pic=""(?<image>.*)""";
            var request = new RequestHelper();
            var msg = string.Empty;
            var content = request.GetContentToString(url, "utf-8", ref msg);
            if (string.IsNullOrWhiteSpace(content))
            {
                result.Status = 1;
                result.Message = "你输入的地址无法识别.";
            }
            else
            {
                var rThumbnail = new Regex(pQQVideo);
                if (rThumbnail.IsMatch(content))
                {
                    var match = rThumbnail.Match(content);
                    result.Status = 0;
                    result.Thumbnail = match.Groups["image"].Value;
                    result.PlayerUrl = string.Format(QQ_PLAYER_URL, match.Groups["swf"].Value);
                    result.Title = match.Groups["title"].Value;
                    result.Message = "解析成功.";
                }
                else
                {
                    result.Status = 1;
                    result.Message = "你输入的地址无法识别.";
                }
            }
            */

            /*
            var client = new WebClient();
            byte[] contentData = client.DownloadData(url);
            string content = Encoding.UTF8.GetString(contentData);
            string vid=string.Empty;
            if (regPageUrl.IsMatch(url))
                vid = regPageUrl.Match(url).Groups["vid"].Value;
            else
                vid = signalPageUrl.Match(url).Groups["vid"].Value;
            result.PlayerUrl = string.Format(QQ_PLAYER_URL, vid);

            var matchInfos = regPic.Matches(content);
            foreach (Match m in matchInfos)
            {
                if (m.Groups["id"].Value == vid)
                {
                    result.Thumbnail = m.Groups["pic"].Value;
                    result.Title = m.Groups["title"].Value;
                    break;
                }
            }
            if (string.IsNullOrEmpty(result.Thumbnail)) result.Thumbnail = string.Format(QQ_THUMBNAIL_URL, vid);
            return result;

             * */

        }
        #endregion

    }

    /// <summary>
    /// 土豆视频API
    /// </summary>
    internal class TuDouVideoAPI
    {
        public TuDouVideoAPI()
        { }

        private string Appkey = "0686cddf72b72368";
        private string apiUrl = "http://api.tudou.com/v3/gw?method=item.info.get&appKey={0}&format=json&itemCodes={1}";

        public string Url { get; set; }

        public static string GetItemCode(string url)
        {
            string itemCode = null;
            string reInfo = string.Empty;
            Regex regV = new Regex(@"\/view\/([\w-]+)/?", RegexOptions.IgnoreCase);
            Regex regV2 = new Regex(@"\/listplay\/([\w-_]+)/?", RegexOptions.IgnoreCase);
            if (regV.IsMatch(url))
            {
                try
                {
                    itemCode = regV.Match(url).Result("$1");
                }
                catch { }
            }
            else if (regV2.IsMatch(url))
            {
                try
                {
                    itemCode = regV2.Match(url).Result("$1");
                }
                catch
                { }
            }
            else
            {
                Regex regP = new Regex(@"\/p\/[a-z]\d+i(\d+).*\.html", RegexOptions.IgnoreCase);
                if (regP.IsMatch(url))
                {
                    string iid = regP.Match(url).Result("$1");
                    try
                    {
                        string tudouHtml = url.GetContentToString("GBK", ref reInfo);
                        Regex regPCode = new Regex("(?is)iid:" + iid + ".*?icode:\"([\\w-]+)\".*?,cid:", RegexOptions.IgnoreCase);
                        if (regPCode.IsMatch(tudouHtml))
                        {
                            return regPCode.Match(tudouHtml).Result("$1");
                        }
                    }
                    catch { }
                }
                else
                {
                    Regex regPfirst = new Regex(@"\/p\/[a-z]\d+\.html", RegexOptions.IgnoreCase);
                    if (regPfirst.IsMatch(url))
                    {
                        try
                        {
                            string tudouHtml = url.GetContentToString("GBK", ref reInfo);
                            Regex regPCode = new Regex("(?is)icode:\"([\\w-]+)\".*?,cid:", RegexOptions.IgnoreCase);
                            if (regPCode.IsMatch(tudouHtml))
                            {
                                return regPCode.Match(tudouHtml).Result("$1");
                            }
                        }
                        catch { }
                    }
                }
            }
            return itemCode;
        }

        public string GetVideoInfo(string url)
        {
            string reInfo = string.Empty;
            if (!string.IsNullOrEmpty(url) && url.StartsWith("http://"))
            {
                string itemcode = GetItemCode(url);
                if (itemcode != null)
                {
                    return string.Format(apiUrl, this.Appkey, itemcode).GetContentToString(ref reInfo);
                }
            }
            return null;
        }
        
    }

}
