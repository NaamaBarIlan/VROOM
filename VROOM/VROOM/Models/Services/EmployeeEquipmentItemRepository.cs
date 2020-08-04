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
                    StatusId = x.StatusId,
                    Status = ((EmployeeEquipmentStatus)x.StatusId).ToString(),
                    DateBorrowed = x.DateBorrowed,
                    DateReturned = x.DateReturned
                })
                .ToListAsync();
            if (EEItemDTOs != null)
            {
                EEItemDTOs = await NestDTOsIn(EEItemDTOs);
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
                    StatusId = x.StatusId,
                    Status = ((EmployeeEquipmentStatus)x.StatusId).ToString(),
                    DateBorrowed = x.DateBorrowed,
                    DateReturned = x.DateReturned
                })
                .ToListAsync();
            if (oneEmployeeEEItemsDTOs != null)
            {
                oneEmployeeEEItemsDTOs = await NestDTOsIn(oneEmployeeEEItemsDTOs);
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
                    StatusId = x.StatusId,
                    Status = ((EmployeeEquipmentStatus)x.StatusId).ToString(),
                    DateBorrowed = x.DateBorrowed,
                    DateReturned = x.DateReturned
                })
                .ToListAsync();
            if (oneItemEEItemsDTOs != null)
            {
                oneItemEEItemsDTOs = await NestDTOsIn(oneItemEEItemsDTOs);
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
                    StatusId = x.StatusId,
                    Status = ((EmployeeEquipmentStatus)x.StatusId).ToString(),
                    DateBorrowed = x.DateBorrowed,
                    DateReturned = x.DateReturned
                })
                .ToListAsync();
            if (oneEmployeeAndItemEEItemsDTOs != null)
            {
                oneEmployeeAndItemEEItemsDTOs = await NestDTOsIn(oneEmployeeAndItemEEItemsDTOs);
            }
            return oneEmployeeAndItemEEItemsDTOs;
        }

        public async Task<List<EmployeeEquipmentItemDTO>> GetAllEmployeeEquipmentRecordsWith(EmployeeEquipmentStatus status)
        {
            List<EmployeeEquipmentItemDTO> EEItemsDTOsWithStatus = await _context.EmployeeEquipmentItem
                .Where(x => x.StatusId == (int)status)
                .Include(x => x.Employee)
                .Include(x => x.EquipmentItem)
                .Select(x => new EmployeeEquipmentItemDTO
                {
                    EmployeeId = x.EmployeeId,
                    EquipmentItemId = x.EquipmentItemId,
                    StatusId = x.StatusId,
                    Status = ((EmployeeEquipmentStatus)x.StatusId).ToString(),
                    DateBorrowed = x.DateBorrowed,
                    DateReturned = x.DateReturned
                })
                .ToListAsync();
            if (EEItemsDTOsWithStatus != null)
            {
                EEItemsDTOsWithStatus = await NestDTOsIn(EEItemsDTOsWithStatus);
            }
            return EEItemsDTOsWithStatus;
        }

        public async Task<EmployeeEquipmentItemDTO> SetEquipmentItemAsBorrowedBy(int employeeId, EmployeeEquipmentItemDTO EEItemDTO)
        {
            EEItemDTO.StatusId = (int)EmployeeEquipmentStatus.Borrowed;
            EEItemDTO.DateBorrowed = DateTime.Now;
            EmployeeEquipmentItem EEItem = ConvertFromDTOtoEntity(EEItemDTO);
            _context.Entry(EEItem).State = EntityState.Added;
            await _context.SaveChangesAsync();
            EEItemDTO.Status = ((EmployeeEquipmentStatus)EEItemDTO.StatusId).ToString();
            return EEItemDTO;
        }

        public Task<EmployeeEquipmentItemDTO> UpdateEmployeeEquipmentItemRecord(int employeeId, EmployeeEquipmentItemDTO EEItemDTO)
        {
            throw new NotImplementedException();
        }

        private async Task<List<EmployeeEquipmentItemDTO>> NestDTOsIn(List<EmployeeEquipmentItemDTO> EEItemDTOs)
        {
            foreach (EmployeeEquipmentItemDTO EEIDTO in EEItemDTOs)
            {
                EEIDTO.Employee = await _employee.GetSingleEmployee(EEIDTO.EmployeeId);
                EEIDTO.EquipmentItem = await _equipmentItem.GetEquipmentItem(EEIDTO.EquipmentItemId);
            }
            return EEItemDTOs;
        }

        private EmployeeEquipmentItem ConvertFromDTOtoEntity(EmployeeEquipmentItemDTO EEItemDTO)
        {
            return new EmployeeEquipmentItem()
            {
                EmployeeId = EEItemDTO.EmployeeId,
                EquipmentItemId = EEItemDTO.EquipmentItemId,
                StatusId = EEItemDTO.StatusId,
                DateBorrowed = EEItemDTO.DateBorrowed,
                DateReturned = EEItemDTO.DateReturned,
            };
        }
    }
}
