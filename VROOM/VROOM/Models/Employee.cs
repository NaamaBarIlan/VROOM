using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VROOM.Models.DTOs;

namespace VROOM.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Dept { get; set; }
        public string Title { get; set; }
        public int StatusId { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string BranchPhone { get; set; }

        // Nav properties

        public List<EmployeeEquipmentItem> EmployeeEquipmentItems { get; set; }

    }

    public enum EmployeeStatus
    {
        Current = 0,
        Former,
        OnLeave,
        LaidOff,
        Fired,
        Retired,
    }
}
