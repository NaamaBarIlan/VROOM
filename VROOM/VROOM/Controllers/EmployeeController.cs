using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IEmployee _employee;

        private IEmailSender _emailSenderService;

        private IConfiguration _config;

        public EmployeeController(IEmployee employee, IEmailSender emailSenderService, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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

            // An employees can only update their own data:
            // TODO - pull the data directly from from User via _userManager
            if (User.IsInRole("Employee"))
            {
                string userId =  _userManager.GetUserId(User);
                //var user = await _userManager.FindByIdAsync(userId);
                //string userEmail = await _userManager.GetEmailAsync(user);

                if (employeeDTO.Email != userId)
                {
                    return BadRequest();
                }
            }

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

            // When we create a new employee, an email is sent to the new employee to welcome them to the company
            string subject = "Welcome to VROOM!";
            string htmlMessage = $"<h2>Congratulations {employeeDTO.FirstName}!</h2><p> Welcome to the team! We are excited to have you at the {employeeDTO.BranchName}. We know you’re going to be a valuable asset to our company and are looking forward to the positive impact you’re going to have here in your new { employeeDTO.Title } role.</p><p>We are looking forward to your start date of Monday, September 1st. Please arrive by 9:30 a.m. at {employeeDTO.BranchAddress}, and park anywhere in our parking lot. Once you arrive, check in at the reception desk. Your HR representative will meet you in the lobby to set you up with all of your new equipment.</p><p>If you have any questions before Monday, feel free to email or call your office manager at {employeeDTO.BranchPhone}.</p><p> Looking forward to working with you!</p>";

            await _emailSenderService.SendEmailAsync(employeeDTO.Email, subject, htmlMessage);

            // Send the CEO an email to notify them of new employee
            string subject2 = "New Employee Alert";
            string htmlMessage2 = $"<h2>New Employee Alert</h2><p> A new employee was added to the system, please see more details below:</p><h4> Employee Information:</h4><ul><li> First Name: { employeeDTO.FirstName}</li><li> Last Name: { employeeDTO.LastName}</li><li> Email: { employeeDTO.Email}</li><li> Phone: { employeeDTO.Phone}</li><li> Dept: { employeeDTO.Dept}</li><li> Title: { employeeDTO.Title}</li></ul><h4> Branch Information:</h4><ul><li> BranchName: { employeeDTO.BranchName}</li><li> BranchAddress: { employeeDTO.BranchAddress}</li><li> BranchPhone: { employeeDTO.BranchPhone}</li></ul><p> For more details and to update the employee information, please login to your VROOM account.</p> ";

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
