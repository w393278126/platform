using System;

namespace Xn.Platform.Domain.Admin
{
    public class AdMonitorCode : Entity
    {
        public string AdName { get; set; }
        public string MonitorCode { get; set; }
        public string TargetUrl { get; set; }
        public int Status { get; set; }
        public string Material { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastChange { get; set; }

        public string Desc { get; set; }
    }
}
