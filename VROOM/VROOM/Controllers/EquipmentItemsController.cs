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
    public class EquipmentItemsController : ControllerBase
    {
        private readonly IEquipmentItem _equipmentItem;

        public EquipmentItemsController(IEquipmentItem equipmentItem)
        {
            _equipmentItem = equipmentItem;
        }

        // GET: api/EquipmentItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentItemDTO>>> GetEquipmentItems()
        {
            return await _equipmentItem.GetEquipmentItems();
        }

        // GET: api/EquipmentItems/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<EquipmentItemDTO>> GetEquipmentItem(int Id)
        {
            EquipmentItemDTO equipmentItem = await _equipmentItem.GetEquipmentItem(Id);

            if (equipmentItem == null)
            {
                return NotFound();
            }

            return equipmentItem;
        }

        // PUT: api/EquipmentItems/5
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutEquipmentItem(int Id, EquipmentItem equipmentItem)
        {
            if (Id != equipmentItem.Id)
            {
                return BadRequest();
            }

            var updatedEquipmentItem = await _equipmentItem.Update(equipmentItem);

            return Ok(updatedEquipmentItem);
        }

        // POST: api/EquipmentItems
        [HttpPost]
        public async Task<ActionResult<EquipmentItemDTO>> PostEquipmentItem(EquipmentItemDTO equipmentItem)
        {
            await _equipmentItem.Create(equipmentItem);

            return CreatedAtAction("GetEquipmentItem", new { id = equipmentItem.Id }, equipmentItem);
        }

        // DELETE: api/EquipmentItems/5
        [HttpDelete("{Id}")]
        public async Task<ActionResult<EquipmentItem>> DeleteEquipmentItem(int Id)
        {
            await _equipmentItem.Delete(Id);
            return NoContent();
        }
    }
}
