using Xn.Platform.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain.Sports
{
    [Table("SportSkin")]
    public class SportSkin : Entity
    {
        public int RoomId { get; set; }
        public string BarPic { get; set; }
        public string ChatPic { get; set; }
        public DateTime WriteTime { get; set; }
    }
}
