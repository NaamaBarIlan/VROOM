using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VROOM.Models.DTOs;

namespace VROOM.Models.Interfaces
{
    public interface IEmployee
    {
        /// <summary>
        /// Returns a list of all the employees in the
        /// Employee database table, converted into EmployeeDTOs
        /// </summary>
        /// <returns>A list of all employees</returns>
        Task<List<EmployeeDTO>> GetEmployees();

        /// <summary>
        /// Returns a specific Employee from the Employee database table,
        /// by employee Id, converted into an EmployeeDTO.
        /// </summary>
        /// <param name="Id">A unique integer employee Id value</param>
        /// <returns>A specific EmployeeDTO object</returns>
        Task<EmployeeDTO> GetSingleEmployee(int Id);

        /// <summary>
        /// Updates a specific employee in the Employee database table, 
        /// by employee Id, into the new employeeDTO passed as parameter.
        /// </summary>
        /// <param name="id">A unique integer employee Id value</param>
        /// <param name="employeeDTO">An instance of an EmployeeDTO</param>
        /// <returns>The updated employeeDTO object</returns>
        Task<EmployeeDTO> UpdateEmployee(int id, EmployeeDTO employeeDTO);

        /// <summary>
        /// Creates a new entry in the Employee database table
        /// based on the employeeDTO parameter, and returns the same
        /// employeeDTO with its db assigned Id.
        /// </summary>
        /// <param name="employeeDTO">An instance of an EmployeeDTO</param>
        /// <returns>A specific EmployeeDTO object with the employee.Id</returns>
        Task<EmployeeDTO> CreateEmployee(EmployeeDTO employeeDTO);

        /// <summary>
        /// Deletes an employee from the Employee database table, 
        /// based on the employee Id parameter.
        /// </summary>
        /// <param name="id">A unique integer employee Id value</param>
        /// <returns>An empty task object</returns>
        Task DeleteEmployee(int id);
    }
}
