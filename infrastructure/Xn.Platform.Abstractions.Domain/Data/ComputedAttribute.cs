using System;

namespace Xn.Platform.Core.Data
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ComputedAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ExcludeStatusAttribute : Attribute
    {
    }
}