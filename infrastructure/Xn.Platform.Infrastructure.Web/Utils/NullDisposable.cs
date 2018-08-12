using System;

namespace Xn.Platform.Infrastructure.Web.Utils
{
    /// <summary>
    /// 默认Disposable类（空对象模式）.
    /// </summary>
    internal sealed class NullDisposable : IDisposable
    {
        public static NullDisposable Instance { get; } = new NullDisposable();

        private NullDisposable()
        {

        }

        public void Dispose()
        {

        }
    }
}
