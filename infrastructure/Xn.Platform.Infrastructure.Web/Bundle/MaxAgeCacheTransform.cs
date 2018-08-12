namespace System.Web.Optimization
{
    class MaxAgeCacheTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            //context.EnableInstrumentation = true;
            //context.HttpContext.Response.CacheControl = "public,max-age=31536000";
            //response.Cacheability = HttpCacheability.Public;
            /*
            var cache = context.HttpContext.Response.Cache;
            cache.SetCacheability(HttpCacheability.Public);
            cache.SetMaxAge(new System.TimeSpan(365, 0, 0, 0, 0));
            cache.SetExpires(DateTime.Now.AddYears(1));
             * */
        }
    }
}