using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VROOM.Data;
using VROOM.Models.DTOs;

namespace VROOM.Models.Interfaces
{
    public interface IEmployeeEquipmentItem
    {
        /// <summary>
        /// Gets all EmployeeEquipmentItem logs.
        /// </summary>
        /// <returns>
        /// List<EmployeeEquipmentDTO>: a list of EmployeeEquipmentItemDTOs
        /// </returns>
        Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecords();

        /// <summary>
        /// Gets all EmployeeEquipmentItem logs for a specific employee.
        /// </summary>
        /// <param name="employeeId">
        /// int: the id of the employee
        /// </param>
        /// <returns>
        /// List<EmployeeEquipmentDTO>: a list of EmployeeEquipmentItemDTOs
        /// </returns>
        Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecordsForEmployee(int employeeId);

        /// <summary>
        /// Gets all EmployeeEquipmentItem logs for a specific piece of equipment.
        /// </summary>
        /// <param name="equipmentItemId">
        /// 
        /// </param>
        /// <returns>
        /// List<EmployeeEquipmentDTO>: a list of EmployeeEquipmentItemDTOs
        /// </returns>
        Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecordsForEquipmentItem(int equipmentItemId);

        Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecordsFor(int employeeId, int equipmentItemId);

        Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecordsWith(EmployeeEquipmentStatus status);

        Task<EmployeeEquipmentItemDTO> SetEquipmentItemAsBorrowedBy(int employeeId, EmployeeEquipmentItemDTO EEItemDTO);

        Task<EmployeeEquipmentItemDTO> UpdateEmployeeEquipmentItemRecord(EmployeeEquipmentItemDTO EEItemDTO);

        Task<EmployeeEquipmentItemDTO> ReturnItem(EmployeeEquipmentItemDTO EEItemDTO);

        Task<bool> CheckIfItemIsAvailable(int equipmentItemId);

        Task<List<EmployeeEquipmentItemDTO>> ListOfUpdatableItemsFor(int employeeId, int equipmentItemId);
    }
}
