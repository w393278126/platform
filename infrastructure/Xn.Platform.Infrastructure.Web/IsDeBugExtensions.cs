using System.Web.Mvc;

namespace Xn.Platform.Infrastructure.Web
{
    public static class IsDeBugExtensions
    {
        public static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}
