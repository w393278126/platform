using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain.Sports
{
    public class ZcryGuessRoomEntity
    {
        public string Room_Id { get; set; }
        public decimal Money { get; set; }
    }

    public class ZcryGuessUserEntity
    {
        public string User_Id { get; set; }
        public string User_Name { get; set; }
        public decimal Money { get; set; }
    }
}
