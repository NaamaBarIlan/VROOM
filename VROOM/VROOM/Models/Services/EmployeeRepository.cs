using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VROOM.Data;
using VROOM.Models.DTOs;
using VROOM.Models.Interfaces;

namespace VROOM.Models.Services
{
    public class EmployeeRepository : IEmployee
    {
        private VROOMDbContext _context;

        public EmployeeRepository(VROOMDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeDTO> CreateEmployee(EmployeeDTO employeeDTO)
        {
            Employee employee = ConvertDTOIntoEmployee(employeeDTO);

            _context.Entry(employee).State = EntityState.Added;
            await _context.SaveChangesAsync();

            employeeDTO.Id = employee.Id;

            return employeeDTO;
        }

        public async Task DeleteEmployee(int id)
        {
            Employee employee = await _context.Employee.FindAsync(id);

            if(employee == null)
            {
                return;
            }
            else
            {
                _context.Entry(employee).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<EmployeeDTO>> GetEmployees()
        {
            var employees = await _context.Employee.ToListAsync();

            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();
            foreach (var item in employees)
            {
                EmployeeDTO employeeDTO = ConvertEmployeeIntoDTO(item);
                employeeDTOs.Add(employeeDTO);
            };

            return employeeDTOs;
        }

        public async Task<EmployeeDTO> GetSingleEmployee(int id)
        {
            Employee employee = await _context.Employee.FindAsync(id);

            if(employee == null)
            {
                return null;
            }
            else
            {
                EmployeeDTO employeeDTO = ConvertEmployeeIntoDTO(employee);

                return employeeDTO;
            }
        }

        public async Task<EmployeeDTO> UpdateEmployee(int id, EmployeeDTO employeeDTO)
        {
            Employee employee = ConvertDTOIntoEmployee(employeeDTO);

            if(employee == null)
            {
                return null;
            }
            else
            {
                _context.Entry(employee).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return employeeDTO;
            }
            
        }

        private EmployeeDTO ConvertEmployeeIntoDTO(Employee employee)
        {
            EmployeeDTO employeeDTO = new EmployeeDTO
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Phone = employee.Phone,
                Dept = employee.Dept,
                Title = employee.Title,
                BranchName = employee.BranchName,
                BranchAddress = employee.BranchAddress,
                BranchPhone = employee.BranchPhone
            };

            return employeeDTO;
        }

        private Employee ConvertDTOIntoEmployee(EmployeeDTO employeeDTO)
        {
            Employee employee = new Employee
            {
                Id = employeeDTO.Id,
                FirstName = employeeDTO.FirstName,
                LastName = employeeDTO.LastName,
                Email = employeeDTO.Email,
                Phone = employeeDTO.Phone,
                Dept = employeeDTO.Dept,
                Title = employeeDTO.Title,
                BranchName = employeeDTO.BranchName,
                BranchAddress = employeeDTO.BranchAddress,
                BranchPhone = employeeDTO.BranchPhone
            };

            return employee;
        }
    }
}
