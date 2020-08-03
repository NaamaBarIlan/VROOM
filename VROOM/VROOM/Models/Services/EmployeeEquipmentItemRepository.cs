using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VROOM.Models.Interfaces;
using VROOM.Data;
using VROOM.Models.DTOs;

namespace VROOM.Models.Services
{
    public class EmployeeEquipmentItemRepository : IEmployeeEquipmentItem
    {
        private VROOMDbContext _context;

        public EmployeeEquipmentItemRepository(VROOMDbContext context)
        {
            _context = context;
        }

        public Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecords()
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecordsForEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecordsForEquipmentItem(int equipmentItemId)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecordsFor(int employeeId, int equipmentItemId)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecordsWithStatus(int statusId)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeEquipmentItemDTO> Create(EmployeeEquipmentItemDTO EEItemDTO)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeEquipmentItemDTO> Update(EmployeeEquipmentItemDTO EEItemDTO)
        {
            throw new NotImplementedException();
        }
    }
}
