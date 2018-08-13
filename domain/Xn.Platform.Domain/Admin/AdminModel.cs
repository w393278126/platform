using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core.Data;
using Xn.Platform.Domain;

namespace Plu.Platform.Domain.Admin
{
    [Table("XnAdminRole")]
    public class AdminRoleModel: OperationEntity
    {
        public string Name { get; set; }

        public int Type { get; set; }
    }

    [Table("XnAdminSystem")]
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

    [Table("XnAdminRoleSystemModel")]
    public class AdminRoleResourceModel : OperationEntity
    {
        public int RoleId { get; set; }
        public int ModelId { get; set; }
    }

}
