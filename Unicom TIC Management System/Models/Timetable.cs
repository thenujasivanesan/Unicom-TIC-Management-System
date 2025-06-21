﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_TIC_Management_System.Models
{
    internal class Timetable
    {
        public int TimetableID { get; set; }
        public int SubjectID { get; set; }
        public int RoomID { get; set; }
        public string TimeSlot { get; set; }
        public DateTime Date { get; set; }
    }
}
