using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VROOM.Models.Interfaces;
using VROOM.Data;
using VROOM.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace VROOM.Models.Services
{
    public class EmployeeEquipmentItemRepository : IEmployeeEquipmentItem
    {
        private VROOMDbContext _context;
        private IEmployee _employee;
        private IEquipmentItem _equipmentItem;

        public EmployeeEquipmentItemRepository(VROOMDbContext context, IEmployee employee, IEquipmentItem equipmentItem)
        {
            _context = context;
            _employee = employee;
            _equipmentItem = equipmentItem;
        }

        public async Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecords()
        {
            List<EmployeeEquipmentItemDTO> EEItemDTOs = await _context.EmployeeEquipmentItem
                .Include(x => x.Employee)
                .Include(x => x.EquipmentItem)
                .Select(x => new EmployeeEquipmentItemDTO
                {
                    EmployeeId = x.EmployeeId,
                    EquipmentItemId = x.EquipmentItemId,
                    Status = ((EmployeeEquipmentStatus)x.Status).ToString(),
                    DateBorrowed = x.DateBorrowed,
                    DateReturned = x.DateReturned
                })
                .ToListAsync();
            if (EEItemDTOs != null)
            {
                NestDTOsIn(EEItemDTOs);
                //foreach (EmployeeEquipmentItemDTO EEIDTO in EEItemDTOs)
                //{
                //    EEIDTO.Employee = await _employee.GetSingleEmployee(EEIDTO.EmployeeId);
                //    EEIDTO.EquipmentItem = await _equipmentItem.GetEquipmentItem(EEIDTO.EquipmentItemId);
                //}
            }
            return EEItemDTOs;
        }

        public async Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecordsForEmployee(int employeeId)
        {
            List<EmployeeEquipmentItemDTO> oneEmployeeEEItemsDTOs = await _context.EmployeeEquipmentItem
                .Where(x => x.EmployeeId == employeeId)
                .Include(x => x.Employee)
                .Include(x => x.EquipmentItem)
                .Select(x => new EmployeeEquipmentItemDTO
                {
                    EmployeeId = x.EmployeeId,
                    EquipmentItemId = x.EquipmentItemId,
                    Status = ((EmployeeEquipmentStatus)x.Status).ToString(),
                    DateBorrowed = x.DateBorrowed,
                    DateReturned = x.DateReturned
                })
                .ToListAsync();
            if (oneEmployeeEEItemsDTOs != null)
            {
                NestDTOsIn(oneEmployeeEEItemsDTOs);
                //foreach (EmployeeEquipmentItemDTO EEIDTO in oneEmployeeEEItemsDTOs)
                //{
                //    EEIDTO.Employee = await _employee.GetSingleEmployee(EEIDTO.EmployeeId);
                //    EEIDTO.EquipmentItem = await _equipmentItem.GetEquipmentItem(EEIDTO.EquipmentItemId);
                //}
            }
            return oneEmployeeEEItemsDTOs;
        }

        public async Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecordsForEquipmentItem(int equipmentItemId)
        {
            List<EmployeeEquipmentItemDTO> oneItemEEItemsDTOs = await _context.EmployeeEquipmentItem
                .Where(x => x.EquipmentItemId == equipmentItemId)
                .Include(x => x.Employee)
                .Include(x => x.EquipmentItem)
                .Select(x => new EmployeeEquipmentItemDTO
                {
                    EmployeeId = x.EmployeeId,
                    EquipmentItemId = x.EquipmentItemId,
                    Status = ((EmployeeEquipmentStatus)x.Status).ToString(),
                    DateBorrowed = x.DateBorrowed,
                    DateReturned = x.DateReturned
                })
                .ToListAsync();
            if (oneItemEEItemsDTOs != null)
            {
                NestDTOsIn(oneItemEEItemsDTOs);
                //foreach (EmployeeEquipmentItemDTO EEIDTO in oneItemEEItemsDTOs)
                //{
                //    EEIDTO.Employee = await _employee.GetSingleEmployee(EEIDTO.EmployeeId);
                //    EEIDTO.EquipmentItem = await _equipmentItem.GetEquipmentItem(EEIDTO.EquipmentItemId);
                //}
            }
            return oneItemEEItemsDTOs;
        }

        public async Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecordsFor(int employeeId, int equipmentItemId)
        {
            List<EmployeeEquipmentItemDTO> oneEmployeeAndItemEEItemsDTOs = await _context.EmployeeEquipmentItem
                .Where(x => x.EmployeeId == employeeId)
                .Where(x => x.EquipmentItemId == equipmentItemId)
                .Include(x => x.Employee)
                .Include(x => x.EquipmentItem)
                .Select(x => new EmployeeEquipmentItemDTO
                {
                    EmployeeId = x.EmployeeId,
                    EquipmentItemId = x.EquipmentItemId,
                    Status = ((EmployeeEquipmentStatus)x.Status).ToString(),
                    DateBorrowed = x.DateBorrowed,
                    DateReturned = x.DateReturned
                })
                .ToListAsync();
            if (oneEmployeeAndItemEEItemsDTOs != null)
            {
                NestDTOsIn(oneEmployeeAndItemEEItemsDTOs);
                //foreach (EmployeeEquipmentItemDTO EEIDTO in oneItemEEItemsDTOs)
                //{
                //    EEIDTO.Employee = await _employee.GetSingleEmployee(EEIDTO.EmployeeId);
                //    EEIDTO.EquipmentItem = await _equipmentItem.GetEquipmentItem(EEIDTO.EquipmentItemId);
                //}
            }
            return oneEmployeeAndItemEEItemsDTOs;
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

        private async void NestDTOsIn(List<EmployeeEquipmentItemDTO> EEItemDTOs)
        {
            foreach (EmployeeEquipmentItemDTO EEIDTO in EEItemDTOs)
            {
                EEIDTO.Employee = await _employee.GetSingleEmployee(EEIDTO.EmployeeId);
                EEIDTO.EquipmentItem = await _equipmentItem.GetEquipmentItem(EEIDTO.EquipmentItemId);
            }
        }
    }
}
