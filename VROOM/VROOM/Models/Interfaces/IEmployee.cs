using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VROOM.Models.DTOs;

namespace VROOM.Models.Interfaces
{
    public interface IEmployee
    {
        Task<List<EmployeeDTO>> GetEmployees();

        Task<EmployeeDTO> GetSingleEmployee(int Id);

        Task<EmployeeDTO> UpdateEmployee(int id, EmployeeDTO employeeDTO);

        Task<EmployeeDTO> CreateEmployee(EmployeeDTO employeeDTO);

        Task DeleteEmployee(int id);
    }
}
