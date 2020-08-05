using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VROOM.Models
{
    public enum EmployeeEquipmentStatus
    {
        Borrowed = 0,
        Returned,
        Lost,
        Stolen,
        Destroyed,
        Sold
    }

    public class EmployeeEquipmentItem
    {
        //CK
        public int EmployeeId { get; set; }

        //CK
        public int EquipmentItemId { get; set; }

        //CK
        public DateTime DateBorrowed { get; set; }

        public int StatusId { get; set; }

        public DateTime DateReturned { get; set; }

        //Navigation properties
        public Employee Employee { get; set; }

        public EquipmentItem EquipmentItem { get; set; }
    }
}
