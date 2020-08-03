using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using VROOM.Data;
using VROOM.Models.DTOs;
using VROOM.Models.Interfaces;
using VROOM.Models;

namespace VROOM.Models.Services
{
    public class EquipmentItemRepository : IEquipmentItem
    {
        private VROOMDbContext _context;

        public EquipmentItemRepository(VROOMDbContext context)
        {
            _context = context;
        }

        public async Task<EquipmentItemDTO> Create(EquipmentItemDTO equipmentItem)
        {
            EquipmentItem entity = new EquipmentItem()
            {
                Name = equipmentItem.Name
            };

            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return equipmentItem;
        }

        public async Task Delete(int Id)
        {
            EquipmentItem equipmentItem = await _context.EquipmentItem.FindAsync(Id);

            if (equipmentItem == null)
            {
                return;
            }
            else
            {
                _context.Entry(equipmentItem).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<EquipmentItemDTO> GetEquipmentItem(int Id)
        {
            EquipmentItem equipmentItem = await _context.EquipmentItem.FindAsync(Id);

            var employeeEquipmentItems = await _context.EmployeeEquipmentItem
                                                        .Where(x => x.EquipmentItemId == Id)
                                                        .ToListAsync();

            if (equipmentItem == null)
            {
                return null;
            }
            else
            {
                equipmentItem.EmployeeEquipmentItems = employeeEquipmentItems;

                EquipmentItemDTO dto = new EquipmentItemDTO()
                {
                    Id = equipmentItem.Id,
                    Name = equipmentItem.Name
                };

                return dto;
            }
        }

        public async Task<List<EquipmentItemDTO>> GetEquipmentItems()
        {
            var list = await _context.EquipmentItem.ToListAsync();

            var equipmentItems = new List<EquipmentItemDTO>();

            foreach (var item in list)
            {
                equipmentItems.Add(await GetEquipmentItem(item.Id));
            }

            return equipmentItems;
        }

        public async Task<EquipmentItem> Update(EquipmentItem equipmentItem)
        {
            if (equipmentItem == null)
            {
                throw new Exception("The value of equipmentItem was null");
            }
            else
            {
                _context.Entry(equipmentItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return equipmentItem;
            }
        }
    }
}
