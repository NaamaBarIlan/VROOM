using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VROOM.Models
{
    public class EquipmentItem
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Equipment Name: ")]
        public string Name { get; set; }
        public decimal Value { get; set; }

        // Nav properties
        public ICollection<EmployeeEquipmentItem> EmployeeEquipmentItems { get; set; }
    }
}
