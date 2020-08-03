using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VROOM.Models
{
    public enum EmployeeEquipmentStatus
    {
        Available = 0,
        Borrowed,
        Returned,
        Lost,
        Stolen,
        Destroyed,
        Sold
    }

    public class EmployeeEquipmentItem
    {
        public int EquipmentItemId { get; set; }

        public int EmployeeId { get; set; }

        public int Status { get; set; }

        public DateTime DateBorrowed { get; set; }

        public DateTime DateReturned { get; set; }

        //Nav props
        public Employee Employee { get; set; }

        public EquipmentItem EquipmentItem { get; set; }
    }
}
