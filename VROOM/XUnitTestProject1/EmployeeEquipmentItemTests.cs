using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VROOM.Models;
using VROOM.Models.DTOs;
using VROOM.Models.Interfaces;
using VROOM.Models.Services;
using Xunit;

namespace XUnitTestProject1
{
    public class EmployeeEquipmentItemTests : DatabaseTest
    {
        private readonly IEmployee _employee;
        private readonly IEquipmentItem _equipmentItem;

        public EmployeeEquipmentItemTests() : base()
        {
            _employee = new EmployeeRepository(_db);
            _equipmentItem = new EquipmentItemRepository(_db);
        }

        private IEmployeeEquipmentItem BuildEEItemRepo()
        {
            return new EmployeeEquipmentItemRepository(_db, _employee, _equipmentItem);
        }

        [Fact]
        public async void CanGetAnEEItem()
        {
            //Arrange
            var employee01 = await _employee.CreateEmployee(
                new EmployeeDTO
                {
                    FirstName = "FirstName01",
                    LastName = "LastName01",
                    Email = "email01@testemail.com",
                    Phone = "555-555-5555",
                    Dept = "Dept01",
                    Title = "Title01",
                    StatusId = (int)EmployeeStatus.Current,
                    Status = (EmployeeStatus.Current).ToString(),
                    BranchName = "Branch01",
                    BranchAddress = "Branch Address 01",
                    BranchPhone = "555-555-5555"
                }
            );
            var item01 = await _equipmentItem.Create(
                new EquipmentItemDTO
                {
                    Name = "EquipmentItem01",
                    Value = 10.11m
                }
            );
            EmployeeEquipmentItemDTO EEItemDTO = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee01.Id,
                EquipmentItemId = item01.Id,
            };
            var EEItemRepo = BuildEEItemRepo();

            //Act
            var EEItem01 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee01.Id, EEItemDTO);
            var EEItems = await EEItemRepo.GetAllEmployeeEquipmentRecords();

            //Assert
            Assert.NotNull(EEItem01);
            Assert.Equal(employee01.Id, EEItem01.EmployeeId);
            Assert.Equal(item01.Id, EEItem01.EquipmentItemId);
            Assert.True(EEItems.Count >= 1);
            bool containsTestItem = false;
            foreach (EmployeeEquipmentItemDTO oneEEItemDTO in EEItems)
            {
                if (oneEEItemDTO.EmployeeId == employee01.Id &&
                    oneEEItemDTO.EquipmentItemId == item01.Id &&
                    oneEEItemDTO.StatusId == (int)EmployeeEquipmentStatus.Borrowed)
                {
                    containsTestItem = true;
                }
            }
            Assert.True(containsTestItem);
        }

        [Fact]
        public async void CanGetAnEEItemByEmployeeId()
        {
            //Arrange
            var employee01 = await _employee.CreateEmployee(
                new EmployeeDTO
                {
                    FirstName = "FirstName01",
                    LastName = "LastName01",
                    Email = "email01@testemail.com",
                    Phone = "555-555-5555",
                    Dept = "Dept01",
                    Title = "Title01",
                    StatusId = (int)EmployeeStatus.Current,
                    Status = (EmployeeStatus.Current).ToString(),
                    BranchName = "Branch01",
                    BranchAddress = "Branch Address 01",
                    BranchPhone = "555-555-5555"
                }
            );
            var item01 = await _equipmentItem.Create(
                new EquipmentItemDTO
                {
                    Name = "EquipmentItem01",
                    Value = 10.11m
                }
            );
            EmployeeEquipmentItemDTO EEItemDTO = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee01.Id,
                EquipmentItemId = item01.Id,
            };
            var EEItemRepo = BuildEEItemRepo();

            //Act
            var EEItem01 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee01.Id, EEItemDTO);
            var EEItems = await EEItemRepo.GetAllEmployeeEquipmentRecordsForEmployee(employee01.Id);

            //Assert
            Assert.NotNull(EEItem01);
            Assert.Equal(employee01.Id, EEItem01.EmployeeId);
            Assert.Equal(item01.Id, EEItem01.EquipmentItemId);
            Assert.True(EEItems.Count >= 1);
            bool containsTestItem = false;
            foreach (EmployeeEquipmentItemDTO oneEEItemDTO in EEItems)
            {
                if (oneEEItemDTO.EmployeeId == employee01.Id &&
                    oneEEItemDTO.EquipmentItemId == item01.Id &&
                    oneEEItemDTO.StatusId == (int)EmployeeEquipmentStatus.Borrowed)
                {
                    containsTestItem = true;
                }
            }
            Assert.True(containsTestItem);
        }

        [Fact]
        public async void CanGetAnEEItemByEquipmentItemId()
        {
            //Arrange
            var employee01 = await _employee.CreateEmployee(
                new EmployeeDTO
                {
                    FirstName = "FirstName01",
                    LastName = "LastName01",
                    Email = "email01@testemail.com",
                    Phone = "555-555-5555",
                    Dept = "Dept01",
                    Title = "Title01",
                    StatusId = (int)EmployeeStatus.Current,
                    Status = (EmployeeStatus.Current).ToString(),
                    BranchName = "Branch01",
                    BranchAddress = "Branch Address 01",
                    BranchPhone = "555-555-5555"
                }
            );
            var item01 = await _equipmentItem.Create(
                new EquipmentItemDTO
                {
                    Name = "EquipmentItem01",
                    Value = 10.11m
                }
            );
            EmployeeEquipmentItemDTO EEItemDTO = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee01.Id,
                EquipmentItemId = item01.Id,
            };
            var EEItemRepo = BuildEEItemRepo();

            //Act
            var EEItem01 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee01.Id, EEItemDTO);
            var EEItems = await EEItemRepo.GetAllEmployeeEquipmentRecordsForEquipmentItem(item01.Id);

            //Assert
            Assert.NotNull(EEItem01);
            Assert.Equal(employee01.Id, EEItem01.EmployeeId);
            Assert.Equal(item01.Id, EEItem01.EquipmentItemId);
            Assert.True(EEItems.Count >= 1);
            bool containsTestItem = false;
            foreach (EmployeeEquipmentItemDTO oneEEItemDTO in EEItems)
            {
                if (oneEEItemDTO.EmployeeId == employee01.Id &&
                    oneEEItemDTO.EquipmentItemId == item01.Id &&
                    oneEEItemDTO.StatusId == (int)EmployeeEquipmentStatus.Borrowed)
                {
                    containsTestItem = true;
                }
            }
            Assert.True(containsTestItem);
        }
    }
}
