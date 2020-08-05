using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VROOM.Models;
using VROOM.Models.DTOs;
using VROOM.Models.Interfaces;

namespace VROOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployee _employee;

        private IEmailSender _emailSenderService;

        private IConfiguration _config;

        public EmployeeController(IEmployee employee, IEmailSender emailSenderService, IConfiguration configuration)
        {
            _employee = employee;
            _emailSenderService = emailSenderService;
            _config = configuration;
        }

        // GET: api/employee
        [HttpGet]
        [Authorize(Policy = "BronzeLevel")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            return await _employee.GetEmployees();
        }

        // GET: api/employee/3
        [HttpGet("{id}")]
        [Authorize(Policy = "BronzeLevel")]
        public async Task<ActionResult<EmployeeDTO>> GetSingleEmployee(int id)
        {
            EmployeeDTO employeeDTO = await _employee.GetSingleEmployee(id);
            return employeeDTO;
        }

        // PUT: api/employee/3
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Policy = "BronzeLevel")]

        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDTO employeeDTO)
        {
            if (id != employeeDTO.Id)
            {
                return BadRequest();
            }

            // TODO: need to make sure that employees can only update their own data and that CEOs and Managers can update everyone's
            //if (User.IsInRole("Employee"))
            //{
            //    if (employeeDTO.Id == User.)
            //    {

            //    }
            //}

            EmployeeDTO updatedEmployeeDTO = await _employee.UpdateEmployee(id, employeeDTO);

            return Ok(updatedEmployeeDTO);
        }

        // POST: api/employee
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Policy = "SilverLevel")]
        public async Task<ActionResult<EmployeeDTO>> CreateEmployee(EmployeeDTO employeeDTO)
        {
            await _employee.CreateEmployee(employeeDTO);

            // When we create a new employee, lets email the new employee to welcome them to the company
            string subject = "Welcome to VROOM!";
            string htmlMessage = $"<h1>Welcome to the family {employeeDTO.FirstName}!!</h1>";

            await _emailSenderService.SendEmailAsync(employeeDTO.Email, subject, htmlMessage);

            // Send the CEO an email to notify them of new employee
            string subject2 = "New employee alert";
            string htmlMessage2 = $"<p>We have a new employee by the name of {employeeDTO.FirstName}</p>";

            await _emailSenderService.SendEmailAsync(_config["CorporateEmail"], subject2, htmlMessage2);

            return CreatedAtAction("GetEmployee", new { id = employeeDTO.Id }, employeeDTO);

        }

        // DELETE: api/employee/3
        //[HttpDelete("{id}")]
        //public async Task <ActionResult<Employee>> DeleteEmployee(int id)
        //{
        //    await _employee.DeleteEmployee(id);
        //    return NoContent();
        //}

    }
}
