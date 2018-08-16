using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain.Admin
{
    class SystemModel
    {
    }


    public enum SystemModelEnum
    {
        [Description("用户模块")]
        UserModel = 1,
        [Description("订单模块")]
        OrderModel = 2,
        [Description("产品模块")]
        ProductModel = 3,
        [Description("活动模块")]
        ActivityModel = 4,
        [Description("系统管理")]
        Systenm = 5,
    }
}
