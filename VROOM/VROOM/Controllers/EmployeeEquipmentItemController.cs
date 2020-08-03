using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VROOM.Data;
using VROOM.Models;

namespace VROOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeEquipmentItemController : ControllerBase
    {
        private readonly VROOMDbContext _context;

        public EmployeeEquipmentItemController(VROOMDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeEquipmentItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeEquipmentItem>>> GetEmployeeEquipmentItem()
        {
            return await _context.EmployeeEquipmentItem.ToListAsync();
        }

        // GET: api/EmployeeEquipmentItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeEquipmentItem>> GetEmployeeEquipmentItem(int id)
        {
            var employeeEquipmentItem = await _context.EmployeeEquipmentItem.FindAsync(id);

            if (employeeEquipmentItem == null)
            {
                return NotFound();
            }

            return employeeEquipmentItem;
        }

        // PUT: api/EmployeeEquipmentItem/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeEquipmentItem(int id, EmployeeEquipmentItem employeeEquipmentItem)
        {
            if (id != employeeEquipmentItem.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employeeEquipmentItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeEquipmentItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EmployeeEquipmentItem
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EmployeeEquipmentItem>> PostEmployeeEquipmentItem(EmployeeEquipmentItem employeeEquipmentItem)
        {
            _context.EmployeeEquipmentItem.Add(employeeEquipmentItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeEquipmentItemExists(employeeEquipmentItem.EmployeeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmployeeEquipmentItem", new { id = employeeEquipmentItem.EmployeeId }, employeeEquipmentItem);
        }

        // DELETE: api/EmployeeEquipmentItem/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeEquipmentItem>> DeleteEmployeeEquipmentItem(int id)
        {
            var employeeEquipmentItem = await _context.EmployeeEquipmentItem.FindAsync(id);
            if (employeeEquipmentItem == null)
            {
                return NotFound();
            }

            _context.EmployeeEquipmentItem.Remove(employeeEquipmentItem);
            await _context.SaveChangesAsync();

            return employeeEquipmentItem;
        }

        private bool EmployeeEquipmentItemExists(int id)
        {
            return _context.EmployeeEquipmentItem.Any(e => e.EmployeeId == id);
        }
    }
}
