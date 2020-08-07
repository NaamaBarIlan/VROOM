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
        /// int: the id of the EmployeeDTO
        /// </param>
        /// <returns>
        /// List<EmployeeEquipmentDTO>: a list of EmployeeEquipmentItemDTOs
        /// </returns>
        Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecordsForEmployee(int employeeId);

        /// <summary>
        /// Gets all EmployeeEquipmentItem logs for a specific piece of equipment.
        /// </summary>
        /// <param name="equipmentItemId">
        /// int: the id of an EquipmentItemDTO
        /// </param>
        /// <returns>
        /// List<EmployeeEquipmentDTO>: a list of EmployeeEquipmentItemDTOs
        /// </returns>
        Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecordsForEquipmentItem(int equipmentItemId);

        /// <summary>
        /// Gets all EmployeeEquipmentItem logs for a specific employee and piece of equipment.
        /// </summary>
        /// <param name="employeeId">
        /// int: the id of the EmployeeDTO
        /// </param>
        /// <param name="equipmentItemId">
        /// int: the id of an EquipmentItemDTO
        /// </param>
        /// <returns>
        /// List<EmployeeEquipmentDTO>: a list of EmployeeEquipmentItemDTOs
        /// </returns>
        Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecordsFor(int employeeId, int equipmentItemId);

        /// <summary>
        /// Gets all EmployeeEquipmentItem logs with an EmployeeEquipmentStatus.
        /// </summary>
        /// <param name="status">
        /// EmployeeEquipmentStatus: an EmployeeEquipmentStatus enum
        /// </param>
        /// <returns>
        /// List<EmployeeEquipmentDTO>: a list of EmployeeEquipmentItemDTOs
        /// </returns>
        Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecordsWith(EmployeeEquipmentStatus status);

        /// <summary>
        /// Creates a new log entry and sets the DateBorrowed to DateTime.Now.
        /// </summary>
        /// <param name="employeeId">
        /// int: the id of EmployeeDTO borrowing the piece of equipment
        /// </param>
        /// <param name="EEItemDTO">
        /// EmployeeEquipmentItemDTO: an EmployeeEquipmentDTO linking the EmployeeDTO to the EquipmentItemDTO
        /// </param>
        /// <returns>
        /// EmployeeEquipmentDTO: the newly created log
        /// </returns>
        Task<EmployeeEquipmentItemDTO> SetEquipmentItemAsBorrowedBy(int employeeId, EmployeeEquipmentItemDTO EEItemDTO);

        /// <summary>
        /// Updates a log entry for a piece of borrowed equipment. If EEItemDTO.StatusId is 2 (returned), updates the DateReturned to DateTime.Now.
        /// </summary>
        /// <param name="EEItemDTO">
        /// EmployeeEquipmentItemDTO: a DTO with updated information
        /// </param>
        /// <returns>
        /// EmployeeEquipmentItemDTO: the DTO with updated information after being saved
        /// </returns>
        Task<EmployeeEquipmentItemDTO> UpdateEmployeeEquipmentItemRecord(EmployeeEquipmentItemDTO EEItemDTO);

        /// <summary>
        /// Updates the log for a specific EEDTO to returned. Only allows setting the status to returned.
        /// </summary>
        /// <param name="EEItemDTO">
        /// EmployeeEquipmentItemDTO: a DTO with updated information
        /// </param>
        /// <returns>
        /// EmployeeEquipmentItemDTO: the DTO with updated information after being saved
        /// </returns>
        Task<EmployeeEquipmentItemDTO> ReturnItem(EmployeeEquipmentItemDTO EEItemDTO);

        /// <summary>
        /// Public helper method. Checks whether a given EquipmentItemDTO is available to borrow.
        /// </summary>
        /// <param name="equipmentItemId">
        /// int: an EquipmentItem id
        /// </param>
        /// <returns>
        /// bool: true if item is available, false otherwise
        /// </returns>
        Task<bool> CheckIfItemIsAvailable(int equipmentItemId);

        /// <summary>
        /// Gets a returnable item for a given employeeId and equipmentId. Returns null if there is no returnable item for that combination.
        /// </summary>
        /// <param name="employeeId">
        /// int: an employeeId
        /// </param>
        /// <param name="equipmentItemId">
        /// int: an equipmentId
        /// </param>
        /// <returns>
        /// EmployeeEquipmentItemDTO: a returnable DTO
        /// </returns>
        Task<EmployeeEquipmentItemDTO> GetReturnableItem(int employeeId, int equipmentItemId);

        /// <summary>
        /// Gets an unpdatable item for a given employeeId and equipmentId. Returns null if there is no updatable item for that combination.
        /// </summary>
        /// <param name="employeeId">
        /// int: an employeeId
        /// </param>
        /// <param name="equipmentItemId">
        /// int: an equipmentId
        /// </param>
        /// <returns>
        /// EmployeeEquipmentItemDTO: a undatable DTO
        /// </returns>
        Task<EmployeeEquipmentItemDTO> GetUpdatableItem(int employeeId, int equipmentItemId);
    }
}
