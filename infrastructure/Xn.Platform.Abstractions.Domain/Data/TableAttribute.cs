using System;

namespace Xn.Platform.Core.Data
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
    }
}