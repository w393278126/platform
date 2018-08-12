using System;

namespace Xn.Platform.Extensions.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal CutDecimalWithN(this decimal d, int n)
        {
            var temp = Math.Round(d, n);

            if (d >= temp)
                return temp;

            return (temp - (decimal)Math.Pow(10, -n));
        }
    }
}
