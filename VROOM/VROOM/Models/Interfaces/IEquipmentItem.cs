using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VROOM.Models.DTOs;

namespace VROOM.Models.Interfaces
{
    public interface IEquipmentItem
    {
        /// <summary>
        /// Create - allows us to create a new piece of equipment for the employees.
        /// </summary>
        /// <param name="equipmentItem">The name of the equipment we want to create.</param>
        /// <returns>The new equipment object.</returns>
        // CREATE
        Task<EquipmentItemDTO> Create(EquipmentItemDTO equipmentItem);

        /// <summary>
        /// GetEquipmentItems - retrieves a list of equipment items.
        /// </summary>
        /// <returns>A list of all pieces of equipment.</returns>
        // READ
        Task<List<EquipmentItemDTO>> GetEquipmentItems();
        /// <summary>
        /// GetEquipmentItem - gets a single piece of equipment by Id.
        /// </summary>
        /// <returns>The requested equipment object.</returns>
        Task<EquipmentItemDTO> GetEquipmentItem(int Id);

        /// <summary>
        /// Update - allows the ability to update details on a piece of equipment.
        /// </summary>
        /// <param name="equipmentItem">The name of piece of equipment we want to modify.</param>
        /// <returns>The modified equipment object.</returns>
        // UPDATE
        Task<EquipmentItem> Update(EquipmentItem equipmentItem);

        /// <summary>
        /// Delete - allows the ability to delete a piece of equipment from the database.
        /// </summary>
        /// <param name="Id">The unique identifier of the piece of equipment we want to delete.</param>
        /// <returns>The task complete - the piece of equipment was deleted.</returns>
        // DELETE
        Task Delete(int Id);

    }
}
