namespace System.Web.Optimization
{
    public class CDNStyleBundle:Bundle
    {   
        public CDNStyleBundle(string virtualPath) 
            : base(PLURes.AppendVersion(virtualPath), new IBundleTransform[]
		{
			new CssMinify()
#if !DEBUG
            ,new MaxAgeCacheTransform()
#endif
		})
        {
		}

        public CDNStyleBundle(string virtualPath, string cdnPath)
            : base(PLURes.AppendVersion(virtualPath), cdnPath, new IBundleTransform[]
		{
			new CssMinify()
#if !DEBUG
            ,new MaxAgeCacheTransform()
#endif
		})
		{
		}
    }
}