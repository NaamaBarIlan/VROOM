using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VROOM.Models.DTOs
{
    public class EmployeeEquipmentItemDTO
    {
        public int EquipmentId { get; set; }

        public int EmployeeId { get; set; }

        public string Status { get; set; }

        public DateTime DateBorrowed { get; set; }

        public DateTime DateReturned { get; set; }

        public EquipmentItem EquipmentItem { get; set; }
    }
}
