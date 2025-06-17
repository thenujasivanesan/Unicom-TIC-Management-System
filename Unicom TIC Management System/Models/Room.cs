using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_TIC_Management_System.Models
{
    internal class Room
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public string RoomType { get; set; } // "Lab" or "Hall"

        public string DisplayRoom => $"{RoomName} ({RoomType})";
    }
}
