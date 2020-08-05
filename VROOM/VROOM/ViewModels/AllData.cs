using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VROOM.Models.DTOs;

namespace VROOM.ViewModels
{
    public class AllData
    {
        public List<EmployeeDTO> EmployeeList { get; set; }
        public List<EquipmentItemDTO> EquipmentList { get; set; }
    }
}
