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
        Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecords();

        Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecordsForEmployee(int employeeId);

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
