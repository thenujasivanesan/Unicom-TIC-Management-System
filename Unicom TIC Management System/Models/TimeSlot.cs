using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_TIC_Management_System.Models
{
    internal class TimeSlot
    {
        public int TimeSlotID { get; set; }
        public string StartTime { get; set; } // e.g., "10:00"
        public string EndTime { get; set; }   // e.g., "11:00"

        public string DisplayTime => $"{StartTime} - {EndTime}";
    }
}
