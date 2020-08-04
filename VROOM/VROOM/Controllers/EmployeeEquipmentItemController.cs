using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VROOM.Data;
using VROOM.Models;
using VROOM.Models.Interfaces;
using VROOM.Models.DTOs;

namespace VROOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeEquipmentItemController : ControllerBase
    {
        private readonly VROOMDbContext _context;
        private readonly IEmployeeEquipmentItem _employeeEquipmentItem;

        public EmployeeEquipmentItemController(IEmployeeEquipmentItem employeeEquipmentItem)
        {
            _employeeEquipmentItem = employeeEquipmentItem;
        }

        // GET: api/EmployeeEquipmentItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeEquipmentItemDTO>>> GetEmployeeEquipmentItems()
        {
            var EEItems = await _employeeEquipmentItem.GetAllEmployeeEquipmentRecords();
            if (EEItems == null)
            {
                return NotFound();
            }
            return EEItems;
        }

        // GET: api/EmployeeEquipmentItem/Employee/{employeeId}
        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<EmployeeEquipmentItemDTO>>> GetEmployeeEquipmentItemForEmployee(int employeeId)
        {
            var EEItemsForEmployee = await _employeeEquipmentItem.GetAllEmployeeEquipmentRecordsForEmployee(employeeId);
            if (EEItemsForEmployee == null)
            {
                return NotFound();
            }
            return EEItemsForEmployee;
        }

        // GET: api/EmployeeEquipmentItem/EquipmentItem/{equipmentId}
        [HttpGet("equipmentitem/{equipmentItemId}")]
        public async Task<ActionResult<IEnumerable<EmployeeEquipmentItemDTO>>> GetAllEmployeeEquipmentRecordsForEquipmentItem(int equipmentItemId)
        {
            var EEItemsForEquipmentItem = await _employeeEquipmentItem.GetAllEmployeeEquipmentRecordsForEquipmentItem(equipmentItemId);
            if (EEItemsForEquipmentItem == null)
            {
                return NotFound();
            }
            return EEItemsForEquipmentItem;
        }

        // GET: api/EmployeeEquipmentItem/Employee/{employeeId}/EquipmentItem/{equipmentId}
        [HttpGet("employee/{employeeId}/equipmentitem/{equipmentItemId}")]
        public async Task<ActionResult<IEnumerable<EmployeeEquipmentItemDTO>>> GetAllEmployeeEquipmentRecordsFor(int employeeId, int equipmentItemId)
        {
            var EEItemsForEmployeeAndEItem = await _employeeEquipmentItem.GetAllEmployeeEquipmentRecordsFor(employeeId, equipmentItemId);
            if (EEItemsForEmployeeAndEItem == null)
            {
                return NotFound();
            }
            return EEItemsForEmployeeAndEItem;
        }

        public async Task<ActionResult<IEnumerable<EmployeeEquipmentItemDTO>>> GetAllEmployeeEquipmentRecordsWithStatus(string status)
        {
            return null;
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

        private bool EmployeeEquipmentItemExists(int id)
        {
            return _context.EmployeeEquipmentItem.Any(e => e.EmployeeId == id);
        }

        //private EmployeeEquipmentStatus StatusStringToEnum(string status)
        //{
        //    switch (status.ToPascale())
        //    {
        //        case EmployeeEquipmentStatus.Available.Nam
        //            return EmployeeEquipmentStatus.Available;
        //        default:
        //            break;
        //    }
        //}
    }
}
