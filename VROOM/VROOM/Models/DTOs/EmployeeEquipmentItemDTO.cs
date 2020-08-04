using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VROOM.Models.DTOs
{
    public class EmployeeEquipmentItemDTO
    {
        public int EmployeeId { get; set; }

        public int EquipmentItemId { get; set; }

        public int StatusId { get; set; }

        public string Status { get; set; }

        public DateTime DateBorrowed { get; set; }

        public DateTime DateReturned { get; set; }

        public EmployeeDTO Employee { get; set; }

        public EquipmentItemDTO EquipmentItem { get; set; }
    }
}
