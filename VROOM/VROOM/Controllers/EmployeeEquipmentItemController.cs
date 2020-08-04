using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IEmployeeEquipmentItem _employeeEquipmentItem;

        public EmployeeEquipmentItemController(IEmployeeEquipmentItem employeeEquipmentItem)
        {
            _employeeEquipmentItem = employeeEquipmentItem;
        }

        // GET: api/EmployeeEquipmentItem
        [HttpGet]
        [Authorize(Policy = "BronzeLevel")]
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
        [Authorize(Policy = "BronzeLevel")]
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
        [Authorize(Policy = "BronzeLevel")]
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
        [Authorize(Policy = "BronzeLevel")]
        public async Task<ActionResult<IEnumerable<EmployeeEquipmentItemDTO>>> GetAllEmployeeEquipmentRecordsFor(int employeeId, int equipmentItemId)
        {
            var EEItemsForEmployeeAndEItem = await _employeeEquipmentItem.GetAllEmployeeEquipmentRecordsFor(employeeId, equipmentItemId);
            if (EEItemsForEmployeeAndEItem == null)
            {
                return NotFound();
            }
            return EEItemsForEmployeeAndEItem;
        }

        // GET: api/EmployeeEquipmentItem/Status/{statusId}
        [HttpGet("status/{statusId}")]
        [Authorize(Policy = "BronzeLevel")]
        public async Task<ActionResult<IEnumerable<EmployeeEquipmentItemDTO>>> GetAllEmployeeEquipmentRecordsWithStatus(int statusId)
        {
            EmployeeEquipmentStatus status = (EmployeeEquipmentStatus)statusId;
            var EEItemsWithStatus = await _employeeEquipmentItem.GetAllEmployeeEquipmentRecordsWith(status);
            if (EEItemsWithStatus == null)
            {
                return NotFound();
            }
            return EEItemsWithStatus;
        }

        // POST: api/EmployeeEquipmentItem
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("{employeeId}")]
        [Authorize(Policy = "SilverLevel")]
        public async Task<ActionResult<EmployeeEquipmentItemDTO>> SetEquipmentItemAsBorrowedBy(int employeeId, EmployeeEquipmentItemDTO EEItemDTO)
        {
            if (employeeId != EEItemDTO.EmployeeId)
            {
                return BadRequest("EmployeeIDs must match.");
            }
            if (!await _employeeEquipmentItem.CheckIfItemIsAvailable(EEItemDTO.EquipmentItemId))
            {
                return BadRequest("Item is not available to be borrowed.");
            }
            else
            {
                EEItemDTO = await _employeeEquipmentItem.SetEquipmentItemAsBorrowedBy(employeeId, EEItemDTO);
                return EEItemDTO;
            }
        }

        // PUT: api/EmployeeEquipmentItem/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{equipmentItemId}")]
        [Authorize(Policy = "BronzeLevel")]
        public async Task<ActionResult<EmployeeEquipmentItemDTO>> UpdateEmployeeEquipmentItem(int equipmentItemId, EmployeeEquipmentItemDTO EEItemDTO)
        {
            if (equipmentItemId != EEItemDTO.EquipmentItemId)
            {
                return BadRequest("EquipmentItemIDs must match.");
            }
            var returnAbleItemForEmployeeDTOs = await _employeeEquipmentItem.ListOfUpdatableItemsFor(EEItemDTO.EmployeeId, equipmentItemId);
            //for a given EmployeeID-EquipmentItemID combination, there should only ever be 0 or 1 returnable items
            if (returnAbleItemForEmployeeDTOs.Count < 1)
            {
                return BadRequest("No updatable items found for that EquipmentItemID and EmployeeID combination.");
            }
            else if (returnAbleItemForEmployeeDTOs.Count > 1)
            {
                return BadRequest("Database state invalid. More than one returnable item found for EquipmentItemID and EmployeeID combination.");
            }
            else
            {
                var EEItemDTOToBeUpdated = returnAbleItemForEmployeeDTOs.First();
                EEItemDTOToBeUpdated.StatusId = EEItemDTO.StatusId;
                var updatedEEItemDTO = await _employeeEquipmentItem.UpdateEmployeeEquipmentItemRecord(EEItemDTOToBeUpdated);
                return updatedEEItemDTO;
            }
        }

        // PUT: api/EmployeeEquipmentItem/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("/Employee/{employeeId}/ReturnItem/{equipmentItemId}/")]
        [Authorize(Policy = "BronzeLevel")]
        public async Task<ActionResult<EmployeeEquipmentItemDTO>> ReturnEquipmentItem(int employeeId, int equipmentItemId)
        {
            var returnAbleItemForEmployeeDTOs = await _employeeEquipmentItem.ListOfUpdatableItemsFor(employeeId, equipmentItemId);
            //for a given EmployeeID-EquipmentItemID combination, there should only ever be 0 or 1 returnable items
            if (returnAbleItemForEmployeeDTOs.Count < 1)
            {
                return BadRequest("Entered data does not match any returnable items.");
            }
            else if (returnAbleItemForEmployeeDTOs.Count > 1)
            {
                return BadRequest("Database state invalid. More than one returnable item found for EquipmentItemID and EmployeeID combination.");
            }
            else
            {
                var EEItemDTOToBeReturned = returnAbleItemForEmployeeDTOs.First();
                var updatedEEItemDTO = await _employeeEquipmentItem.ReturnItem(EEItemDTOToBeReturned);
                return updatedEEItemDTO;
            }
        }
    }
}
