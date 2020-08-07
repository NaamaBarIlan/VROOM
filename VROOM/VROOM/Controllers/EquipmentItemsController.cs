using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VROOM.Models;
using VROOM.Models.DTOs;
using VROOM.Models.Interfaces;

namespace VROOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // AuthorizeFilter added in Startup.cs
    public class EquipmentItemsController : ControllerBase
    {
        private readonly IEquipmentItem _equipmentItem;

        public EquipmentItemsController(IEquipmentItem equipmentItem)
        {
            _equipmentItem = equipmentItem;
        }

        // GET: api/EquipmentItems
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EquipmentItemDTO>>> GetEquipmentItems()
        {
            return await _equipmentItem.GetEquipmentItems();
        }

        // GET: api/EquipmentItems/5
        [HttpGet("{EquipmentItemId}")]
        [AllowAnonymous]
        public async Task<ActionResult<EquipmentItemDTO>> GetEquipmentItem(int EquipmentItemId)
        {
            EquipmentItemDTO equipmentItem = await _equipmentItem.GetEquipmentItem(EquipmentItemId);

            if (equipmentItem == null)
            {
                return NotFound();
            }

            return equipmentItem;
        }

        // PUT: api/EquipmentItems/5
        [HttpPut("{EquipmentItemId}")]
        [Authorize(Policy = "SilverLevel")]
        public async Task<IActionResult> PutEquipmentItem(int EquipmentItemId, EquipmentItem equipmentItem)
        {
            if (EquipmentItemId != equipmentItem.Id)
            {
                return BadRequest();
            }

            var updatedEquipmentItem = await _equipmentItem.Update(equipmentItem);

            return Ok(updatedEquipmentItem);
        }

        // POST: api/EquipmentItems
        [HttpPost]
        [Authorize(Policy = "GoldLevel")]
        public async Task<ActionResult<EquipmentItemDTO>> PostEquipmentItem(EquipmentItemDTO equipmentItem)
        {
            await _equipmentItem.Create(equipmentItem);

            return CreatedAtAction("GetEquipmentItem", new { id = equipmentItem.Id }, equipmentItem);
        }

        // DELETE: api/EquipmentItems/5
        [HttpDelete("{EquipmentItemId}")]
        [Authorize(Policy = "GoldLevel")]
        public async Task<ActionResult<EquipmentItem>> DeleteEquipmentItem(int EquipmentItemId)
        {
            await _equipmentItem.Delete(EquipmentItemId);
            return NoContent();
        }
    }
}
