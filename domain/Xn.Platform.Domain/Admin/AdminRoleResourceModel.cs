using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core.Data;
using Xn.Platform.Domain;

namespace Xn.Platform.Domain.Admin
{
    [Table("XnAdminRole")]
    public class AdminRoleModel : OperationEntity
    {
        public string Name { get; set; }
        /// <summary>
        /// 管理员类型
        /// </summary>
        public AdminType Type { get; set; }
    }

    [Table("XnAdminResource")]
    public class AdminResourceModel : OperationEntity
    {
        public string ModelName { get; set; }
        public string ModelCode { get; set; }
        public string Controller { get; set; }
        public string ControllerDes { get; set; }
        public string Action { get; set; }
        public string ActionDes { get; set; }
        public int ParentId { get; set; }
    }

    [Table("XnAdminRoleResource")]
    public class AdminRoleResourceModel : OperationEntity
    {
        public int RoleId { get; set; }
        public int ModelId { get; set; }
    }

    public enum AdminType
    {
        /// <summary>
        /// 超级管理员
        /// </summary>
        SuperAdmin = 1,
        /// <summary>
        /// 模块管理员
        /// </summary>
        //ModelAdmin = 2,
        /// <summary>
        /// 功能管理员
        /// </summary>
        FunctionAdmin = 3,
    }

    public class AdminRoleAuth
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public AdminType Type { get; set; }

        public List<AdminResourceModel> Resource { get; set; }
    }
}
