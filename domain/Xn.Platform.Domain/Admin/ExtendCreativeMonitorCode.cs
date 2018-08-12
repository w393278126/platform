using Xn.Platform.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain.Admin
{
    public class ExtendCreativeMonitorCode : Entity
    {
        public int CreativeId { get; set; }
        public string TtpMonitorCode { get; set; }
        public string PlatformMonitorCode { get; set; }
    }
}
