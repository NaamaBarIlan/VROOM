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
        public int EquipmentId { get; set; }

        public int EmployeeId { get; set; }

        public int Status { get; set; }

        public DateTime DateBorrowed { get; set; }

        public DateTime DateReturned { get; set; }

        //Nav props
        public List<Employee> Employees { get; set; }

        public List<EquipmentItem> EquipmentItems { get; set; }
    }
}
