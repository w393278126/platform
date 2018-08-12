using System.Text;
using System.Text.RegularExpressions;

namespace System.Web.Optimization
{
    public static class PLURes
    {
        public static string AppendVersion(string path)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.MinorRevision;
            path += "_v" + version.ToString();
            return path;
        }

        static void _AppendVersion(string[] paths)
        {
            for (var i = 0; i < paths.Length; i++)
            {
                paths[i] = AppendVersion(paths[i]);
            }
        }

#if DEBUG
        const string prefix = "";
#else
        const string prefix = "";
#endif
        public static IHtmlString RenderScripts(params string[] paths)
        {
            _AppendVersion(paths);
#if DEBUG
            return Scripts.Render(paths);
#else
            return ResScripts.Render(prefix, paths);
#endif
        }

        public static IHtmlString RenderStyles(params string[] paths)
        {
            _AppendVersion(paths);
#if DEBUG
            return Styles.Render(paths);
#else
            return ResStyles.Render(prefix, paths);
#endif
        }

        private static class ResScripts
        {
            //<script src=\"/Scripts/jquery-1.7.1.js\"></script>"
            static Regex regScript = new Regex(@"<script src=""(?<src>[^""]+)"">[^<]*</script>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            const string script_temp = @"<script type=""text/javascript"" src=""{0}{1}""></script>";
            /// <summary>
            /// 返回带前缀（域名）的资源地址
            /// </summary>
            /// <param name="prefix">前缀，例如http://res2.plures.net，注意后面不跟/</param>
            /// <param name="paths">bundle中注册的地址</param>
            /// <returns>资源引用内容结果</returns>
            public static IHtmlString Render(string prefix, params string[] paths)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var path in paths)
                {
                    IHtmlString temp = Scripts.Url(path);
                    string s = temp.ToHtmlString();
                    sb.AppendFormat(script_temp, prefix,s);
                }
                return new HtmlString(sb.ToString());
            }
        }


        private static class ResStyles
        {
            //<link href="/Content/site.css" rel="stylesheet"/>
            static Regex regScript = new Regex(@"<link href=""(?<href>[^""]+)"" rel=""stylesheet""/>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
          
            const string css_temp = @"<link href=""{0}{1}"" rel=""stylesheet""/>";
            /// <summary>
            /// 返回带前缀（域名）的资源地址
            /// </summary>
            /// <param name="prefix">前缀，例如http://res2.plures.net，注意后面不跟/</param>
            /// <param name="paths">bundle中注册的地址</param>
            /// <returns>资源引用内容结果</returns>
            public static IHtmlString Render(string prefix, params string[] paths)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var path in paths)
                {
                    IHtmlString temp = Scripts.Url(path);
                    string s = temp.ToHtmlString();
                    sb.AppendFormat(css_temp, prefix, s);
                }
                return new HtmlString(sb.ToString());
            }
        }
    }
}