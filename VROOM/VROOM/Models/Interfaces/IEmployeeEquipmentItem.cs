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

        Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecordsWithStatus(int statusId);

        Task<EmployeeEquipmentItemDTO> Create(EmployeeEquipmentItemDTO EEItemDTO);

        Task<EmployeeEquipmentItemDTO> Update(EmployeeEquipmentItemDTO EEItemDTO);
    }
}
