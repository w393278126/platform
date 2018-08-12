namespace System.Web.Optimization
{
    public class CDNScriptBundle : Bundle
    {
        public CDNScriptBundle(string virtualPath)
            : this(virtualPath, null)
        {
        }

        public CDNScriptBundle(string virtualPath, string cdnPath)
            : base(PLURes.AppendVersion(virtualPath), cdnPath, new IBundleTransform[]
		{
			new JsMinify()
#if !DEBUG
            ,new MaxAgeCacheTransform()
#endif
		})
        {
            base.ConcatenationToken = ";" + Environment.NewLine;
        }
    }
}
