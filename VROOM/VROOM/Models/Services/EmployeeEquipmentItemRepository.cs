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

        /// <summary>
        /// Instantiates a new EmployeeEquipmentItemRepository object.
        /// </summary>
        /// <param name="context">
        /// VROOMDbContext: an object that inherits from DbContext
        /// </param>
        /// <param name="employee">
        /// IEmployee: an object that implements IEmployee
        /// </param>
        /// <param name="equipmentItem">
        /// IEquipmentItem: an object that implements IEquipmentItem
        /// </param>
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
                    Status = EmployeeEquipmentStatusStringFrom(x.StatusId),
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
                    Status = EmployeeEquipmentStatusStringFrom(x.StatusId),
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
                    Status = EmployeeEquipmentStatusStringFrom(x.StatusId),
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
                    Status = EmployeeEquipmentStatusStringFrom(x.StatusId),
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
                    Status = EmployeeEquipmentStatusStringFrom(x.StatusId),
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
            if (!await CheckIfItemIsAvailable(EEItemDTO.EquipmentItemId))
            {
                //client is asking to borrow a piece of equipment not available, but there's no way of knowing that record's CKs
                return null;
            }
            EEItemDTO.StatusId = (int)EmployeeEquipmentStatus.Borrowed;
            EEItemDTO.DateBorrowed = DateTime.Now;
            EmployeeEquipmentItem EEItem = ConvertFromDTOtoEntity(EEItemDTO);
            _context.Entry(EEItem).State = EntityState.Added;
            await _context.SaveChangesAsync();
            EEItemDTO.Status = ((EmployeeEquipmentStatus)EEItemDTO.StatusId).ToString();
            return EEItemDTO;
        }

        public async Task<EmployeeEquipmentItemDTO> UpdateEmployeeEquipmentItemRecord(EmployeeEquipmentItemDTO EEItemDTO)
        {
            EmployeeEquipmentItem EEItem = await _context.FindAsync<EmployeeEquipmentItem>(EEItemDTO.EmployeeId, EEItemDTO.EquipmentItemId, EEItemDTO.DateBorrowed);
            if (EEItem.StatusId == (int)EmployeeEquipmentStatus.Returned)
            {
                return ConvertFromEntityToDTO(EEItem);
            }
            if (EEItemDTO.StatusId == (int)EmployeeEquipmentStatus.Returned)
            {
                EEItemDTO.DateReturned = DateTime.Now;
                EEItem.DateReturned = EEItemDTO.DateReturned;
            }
            EEItemDTO.Status = EmployeeEquipmentStatusStringFrom(EEItemDTO.StatusId);
            EEItem.StatusId = EEItemDTO.StatusId;
            _context.Entry(EEItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return EEItemDTO;
        }

        public async Task<EmployeeEquipmentItemDTO> ReturnItem(EmployeeEquipmentItemDTO EEItemDTO)
        {
            EEItemDTO.StatusId = (int)EmployeeEquipmentStatus.Returned;
            return await UpdateEmployeeEquipmentItemRecord(EEItemDTO);
        }

        public async Task<List<EmployeeEquipmentItemDTO>> ListOfUpdatableItemsFor(int employeeId, int equipmentItemId)
        {
            var allItemsForEmployeeNotYetReturned = await _context.EmployeeEquipmentItem
                .Where(x => x.EmployeeId == employeeId)
                .Where(x => x.EquipmentItemId == equipmentItemId)
                .Where(x => x.StatusId != (int)EmployeeEquipmentStatus.Returned)
                .Select(x => new EmployeeEquipmentItemDTO
                {
                    EmployeeId = x.EmployeeId,
                    EquipmentItemId = x.EquipmentItemId,
                    StatusId = x.StatusId,
                    Status = EmployeeEquipmentStatusStringFrom(x.StatusId),
                    DateBorrowed = x.DateBorrowed,
                    DateReturned = x.DateReturned
                })
                .ToListAsync();
            return allItemsForEmployeeNotYetReturned;
        }

        public async Task<bool> CheckIfItemIsAvailable(int equipmentItemId)
        {
            var EItemDTO = await _equipmentItem.GetEquipmentItem(equipmentItemId);
            if (EItemDTO == null)
            {
                return false;
            }
            var mostRecentActivityItem = await _context.EmployeeEquipmentItem
                .Where(x => x.EquipmentItemId == equipmentItemId)
                .OrderByDescending(x => x.DateBorrowed)
                .FirstOrDefaultAsync();
            if (mostRecentActivityItem == null)
            {
                return true;
            }
            else
            {
                return mostRecentActivityItem.StatusId == (int)EmployeeEquipmentStatus.Returned;
            }
        }

        /// <summary>
        /// Private helper method. Adds Employee and EquipmentItem DTOs to EmployeeEquipmentItem DTOs as nested objects.
        /// </summary>
        /// <param name="EEItemDTOs">
        /// List<EmployeeEquiptmentItemDTO>: a List of EmployeeEquipmentItemDTOs
        /// </param>
        /// <returns>
        /// List<EmployeeEquiptmentItemDTO>: a List of EmployeeEquipmentItemDTOs with Employee and EquipmentItem DTOs embedded
        /// </returns>
        private async Task<List<EmployeeEquipmentItemDTO>> NestDTOsIn(List<EmployeeEquipmentItemDTO> EEItemDTOs)
        {
            foreach (EmployeeEquipmentItemDTO EEIDTO in EEItemDTOs)
            {
                EEIDTO.Employee = await _employee.GetSingleEmployee(EEIDTO.EmployeeId);
                EEIDTO.EquipmentItem = await _equipmentItem.GetEquipmentItem(EEIDTO.EquipmentItemId);
            }
            return EEItemDTOs;
        }

        /// <summary>
        /// Private helper method. Converts from EmployeeEquipmentItemDTO to EmployeeEquipmentItem (entity) object.
        /// </summary>
        /// <param name="EEItemDTO">
        /// EmployeeEquipmentItemDTO: a DTO object
        /// </param>
        /// <returns>
        /// EmployeeEquipmentItem: an entity object
        /// </returns>
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

        /// <summary>
        /// Private helper method. Converts from EmployeeEquipmentItem (entity) to EmployeeEquipmentItemDTO object.
        /// </summary>
        /// <param name="EEItem">
        /// EmployeeEquipmentItem: an entity object
        /// </param>
        /// <returns>
        /// EmployeeEquipmentItemDTO: a DTO object
        /// </returns>
        private EmployeeEquipmentItemDTO ConvertFromEntityToDTO(EmployeeEquipmentItem EEItem)
        {
            return new EmployeeEquipmentItemDTO()
            {
                EmployeeId = EEItem.EmployeeId,
                EquipmentItemId = EEItem.EquipmentItemId,
                StatusId = EEItem.StatusId,
                DateBorrowed = EEItem.DateBorrowed,
                DateReturned = EEItem.DateReturned,
            };
        }

        /// <summary>
        /// Private helper method. Returns a string form of the EmployeeEquipmentStatus enum.
        /// </summary>
        /// <param name="statusId">
        /// int: the int form of the EmployeeEquipmentStatus enum
        /// </param>
        /// <returns>
        /// string: the string form of the EmployeeEquipmentStatus enum
        /// </returns>
        private static string EmployeeEquipmentStatusStringFrom(int statusId)
        {
            return ((EmployeeEquipmentStatus)statusId).ToString();
        }
    }
}