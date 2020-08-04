using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VROOM.Models.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Dept { get; set; }
        public string Title { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string BranchPhone { get; set; }

        public List<EmployeeEquipmentItemDTO> EmployeeEquipmentItemDTOs { get; set; }
    }
}
