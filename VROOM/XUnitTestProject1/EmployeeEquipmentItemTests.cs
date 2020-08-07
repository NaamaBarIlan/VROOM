using System;
using System.Collections.Generic;
using System.Linq;
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
            var employee02 = await _employee.CreateEmployee(
                new EmployeeDTO
                {
                    FirstName = "FirstName02",
                    LastName = "LastName02",
                    Email = "email02@testemail.com",
                    Phone = "555-555-5555",
                    Dept = "Dept02",
                    Title = "Title02",
                    StatusId = (int)EmployeeStatus.Current,
                    Status = (EmployeeStatus.Current).ToString(),
                    BranchName = "Branch02",
                    BranchAddress = "Branch Address 02",
                    BranchPhone = "555-555-5555"
                }
            );
            var item01 = await _equipmentItem.Create(
                new EquipmentItemDTO
                {
                    Name = "EquipmentItem01",
                    Value = 10.01m
                }
            );
            var item02 = await _equipmentItem.Create(
                new EquipmentItemDTO
                {
                    Name = "EquipmentItem02",
                    Value = 10.02m
                }
            );
            var item03 = await _equipmentItem.Create(
                new EquipmentItemDTO
                {
                    Name = "EquipmentItem03",
                    Value = 10.03m
                }
            );
            EmployeeEquipmentItemDTO EEItemDTOE01I01 = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee01.Id,
                EquipmentItemId = item01.Id,
            };
            EmployeeEquipmentItemDTO EEItemDTOE01I02 = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee01.Id,
                EquipmentItemId = item02.Id,
            };
            EmployeeEquipmentItemDTO EEItemDTOE02I03 = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee02.Id,
                EquipmentItemId = item03.Id,
            };
            var EEItemRepo = BuildEEItemRepo();

            //Act
            var EEItem01 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee01.Id, EEItemDTOE01I01);
            var EEItem02 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee01.Id, EEItemDTOE01I02);
            var EEItem03 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee02.Id, EEItemDTOE02I03);

            var EEItemsForEmployee01 = await EEItemRepo.GetAllEmployeeEquipmentRecordsForEmployee(employee01.Id);
            var EEItemsForEmployee02 = await EEItemRepo.GetAllEmployeeEquipmentRecordsForEmployee(employee02.Id);

            //Assert
            Assert.NotNull(EEItem01);
            Assert.NotNull(EEItem02);
            Assert.NotNull(EEItem03);

            Assert.Equal(employee01.Id, EEItem01.EmployeeId);
            Assert.Equal(item01.Id, EEItem01.EquipmentItemId);

            Assert.True(EEItemsForEmployee01.Count == 2);
            bool EEE01ContainsItem01 = false;
            bool EEE01ContainsItem02 = false;
            foreach (EmployeeEquipmentItemDTO oneEEItemDTO in EEItemsForEmployee01)
            {
                if (oneEEItemDTO.EmployeeId == employee01.Id &&
                    oneEEItemDTO.EquipmentItemId == item01.Id &&
                    oneEEItemDTO.StatusId == (int)EmployeeEquipmentStatus.Borrowed)
                {
                    EEE01ContainsItem01 = true;
                }
                if (oneEEItemDTO.EmployeeId == employee01.Id &&
                    oneEEItemDTO.EquipmentItemId == item02.Id &&
                    oneEEItemDTO.StatusId == (int)EmployeeEquipmentStatus.Borrowed)
                {
                    EEE01ContainsItem02 = true;
                }
            }
            Assert.True(EEE01ContainsItem01);
            Assert.True(EEE01ContainsItem02);

            Assert.True(EEItemsForEmployee02.Count == 1);
            bool EEE02ContainsItem03 = false;
            foreach (EmployeeEquipmentItemDTO oneEEItemDTO in EEItemsForEmployee02)
            {
                if (oneEEItemDTO.EmployeeId == employee02.Id &&
                    oneEEItemDTO.EquipmentItemId == item03.Id &&
                    oneEEItemDTO.StatusId == (int)EmployeeEquipmentStatus.Borrowed)
                {
                    EEE02ContainsItem03 = true;
                }
            }
            Assert.True(EEE02ContainsItem03);
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
            var employee02 = await _employee.CreateEmployee(
                new EmployeeDTO
                {
                    FirstName = "FirstName02",
                    LastName = "LastName02",
                    Email = "email02@testemail.com",
                    Phone = "555-555-5555",
                    Dept = "Dept02",
                    Title = "Title02",
                    StatusId = (int)EmployeeStatus.Current,
                    Status = (EmployeeStatus.Current).ToString(),
                    BranchName = "Branch02",
                    BranchAddress = "Branch Address 02",
                    BranchPhone = "555-555-5555"
                }
            );
            var item01 = await _equipmentItem.Create(
                new EquipmentItemDTO
                {
                    Name = "EquipmentItem01",
                    Value = 10.01m
                }
            );
            var item02 = await _equipmentItem.Create(
                new EquipmentItemDTO
                {
                    Name = "EquipmentItem02",
                    Value = 10.02m
                }
            );
            var item03 = await _equipmentItem.Create(
                new EquipmentItemDTO
                {
                    Name = "EquipmentItem03",
                    Value = 10.03m
                }
            );
            EmployeeEquipmentItemDTO EEItemDTOE01I01 = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee01.Id,
                EquipmentItemId = item01.Id,
            };
            EmployeeEquipmentItemDTO EEItemDTOE01I02 = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee01.Id,
                EquipmentItemId = item02.Id,
            };
            EmployeeEquipmentItemDTO EEItemDTOE02I03 = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee02.Id,
                EquipmentItemId = item03.Id,
            };
            var EEItemRepo = BuildEEItemRepo();

            //Act
            var EEBorrowedE01I01 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee01.Id, EEItemDTOE01I01);
            var EEBorrowedE01I02 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee01.Id, EEItemDTOE01I02);
            var EEBorrowedE02I03 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee02.Id, EEItemDTOE02I03);

            var EEReturnedE01I01 = await EEItemRepo.ReturnItem(EEBorrowedE01I01);
            EmployeeEquipmentItemDTO EEItemDTOE02I01 = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee02.Id,
                EquipmentItemId = item01.Id,
            };
            var EEBorrowedE02I01 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee02.Id, EEItemDTOE02I01);

            var EEReturnedE01I02 = await EEItemRepo.ReturnItem(EEBorrowedE01I02);

            //expected Count = 2
            var EEItemsForItem01 = await EEItemRepo.GetAllEmployeeEquipmentRecordsForEquipmentItem(item01.Id);

            //expected Count = 1
            var EEItemsForItem02 = await EEItemRepo.GetAllEmployeeEquipmentRecordsForEquipmentItem(item02.Id);

            //expected Count = 1
            var EEItemsForItem03 = await EEItemRepo.GetAllEmployeeEquipmentRecordsForEquipmentItem(item03.Id);

            //Assert
            Assert.NotNull(EEItemsForItem01);
            Assert.NotNull(EEItemsForItem02);
            Assert.NotNull(EEItemsForItem03);

            Assert.True(EEItemsForItem01.Count >= 2);
            Assert.True(EEItemsForItem02.Count >= 1);
            Assert.True(EEItemsForItem03.Count >= 1);
        }

        [Fact]
        public async void CanGetAnEEItemByEmployeeAndEquipmentItemId()
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
            var employee02 = await _employee.CreateEmployee(
                new EmployeeDTO
                {
                    FirstName = "FirstName02",
                    LastName = "LastName02",
                    Email = "email02@testemail.com",
                    Phone = "555-555-5555",
                    Dept = "Dept02",
                    Title = "Title02",
                    StatusId = (int)EmployeeStatus.Current,
                    Status = (EmployeeStatus.Current).ToString(),
                    BranchName = "Branch02",
                    BranchAddress = "Branch Address 02",
                    BranchPhone = "555-555-5555"
                }
            );
            var item01 = await _equipmentItem.Create(
                new EquipmentItemDTO
                {
                    Name = "EquipmentItem01",
                    Value = 10.01m
                }
            );
            var item02 = await _equipmentItem.Create(
                new EquipmentItemDTO
                {
                    Name = "EquipmentItem02",
                    Value = 10.02m
                }
            );
            EmployeeEquipmentItemDTO EEItemDTOE01I01 = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee01.Id,
                EquipmentItemId = item01.Id,
            };
            EmployeeEquipmentItemDTO EEItemDTOE02I02 = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee02.Id,
                EquipmentItemId = item02.Id,
            };
            var EEItemRepo = BuildEEItemRepo();

            //Act
            var EEBorrowedE01I01 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee01.Id, EEItemDTOE01I01);
            var EEBorrowedE02I02 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee02.Id, EEItemDTOE02I02);

            var EEReturnedE01I01 = await EEItemRepo.ReturnItem(EEBorrowedE01I01);
            var EEBorrowed2ndE01I01 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee01.Id, EEReturnedE01I01);

            //expected Count = 2
            var EEItemsForE01I01 = await EEItemRepo.GetAllEmployeeEquipmentRecordsFor(employee01.Id, item01.Id);

            //expected Count = 1
            var EEItemsForE02I02 = await EEItemRepo.GetAllEmployeeEquipmentRecordsFor(employee02.Id, item02.Id);


            //Assert
            Assert.NotNull(EEItemsForE01I01);
            Assert.NotNull(EEItemsForE02I02);

            Assert.True(EEItemsForE01I01.Count == 2);
            Assert.True(EEItemsForE02I02.Count == 1);
        }

        [Fact]
        public async void CanGetAnEEItemByStatus()
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
            var employee02 = await _employee.CreateEmployee(
                new EmployeeDTO
                {
                    FirstName = "FirstName02",
                    LastName = "LastName02",
                    Email = "email02@testemail.com",
                    Phone = "555-555-5555",
                    Dept = "Dept02",
                    Title = "Title02",
                    StatusId = (int)EmployeeStatus.Current,
                    Status = (EmployeeStatus.Current).ToString(),
                    BranchName = "Branch02",
                    BranchAddress = "Branch Address 02",
                    BranchPhone = "555-555-5555"
                }
            );
            var item01 = await _equipmentItem.Create(
                new EquipmentItemDTO
                {
                    Name = "EquipmentItem01",
                    Value = 10.01m
                }
            );
            var item02 = await _equipmentItem.Create(
                new EquipmentItemDTO
                {
                    Name = "EquipmentItem02",
                    Value = 10.02m
                }
            );
            var item03 = await _equipmentItem.Create(
                new EquipmentItemDTO
                {
                    Name = "EquipmentItem03",
                    Value = 10.03m
                }
            );
            EmployeeEquipmentItemDTO EEItemDTOE01I01 = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee01.Id,
                EquipmentItemId = item01.Id,
            };
            EmployeeEquipmentItemDTO EEItemDTOE01I02 = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee01.Id,
                EquipmentItemId = item02.Id,
            };
            EmployeeEquipmentItemDTO EEItemDTOE02I03 = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee02.Id,
                EquipmentItemId = item03.Id,
            };
            var EEItemRepo = BuildEEItemRepo();

            //Act
            var EEBorrowedE01I01 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee01.Id, EEItemDTOE01I01);
            var EEBorrowedE01I02 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee01.Id, EEItemDTOE01I02);
            var EEBorrowedE02I03 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee02.Id, EEItemDTOE02I03);

            EEBorrowedE01I01.StatusId = (int)EmployeeEquipmentStatus.Destroyed;
            var EEDestroyedE01I01 = await EEItemRepo.UpdateEmployeeEquipmentItemRecord(EEBorrowedE01I01);

            var EEReturnedE0I02 = await EEItemRepo.ReturnItem(EEBorrowedE01I02);

            //expected Count >= 1
            var EEItemsForStatusBorrowed = await EEItemRepo.GetAllEmployeeEquipmentRecordsWith(EmployeeEquipmentStatus.Borrowed);

            //expected Count >= 1
            var EEItemsForStatusDestroyed = await EEItemRepo.GetAllEmployeeEquipmentRecordsWith(EmployeeEquipmentStatus.Destroyed);

            //expected Count >= 1
            var EEItemsForStatusReturned = await EEItemRepo.GetAllEmployeeEquipmentRecordsWith(EmployeeEquipmentStatus.Returned);

            //Assert
            Assert.NotNull(EEItemsForStatusBorrowed);
            Assert.NotNull(EEItemsForStatusDestroyed);
            Assert.NotNull(EEItemsForStatusReturned);

            Assert.True(EEItemsForStatusBorrowed.Count >= 1);
            Assert.True(EEItemsForStatusDestroyed.Count >= 1);
            Assert.True(EEItemsForStatusReturned.Count >= 1);
        }

        [Fact]
        public async void CanSetItemAsBorrowed()
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
                    Value = 10.01m
                }
            );
            EmployeeEquipmentItemDTO EEItemDTOE01I01 = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee01.Id,
                EquipmentItemId = item01.Id,
            };
            var EEItemRepo = BuildEEItemRepo();

            //Act
            var EEBorrowedE01I01 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee01.Id, EEItemDTOE01I01);

            //Assert
            Assert.NotNull(EEBorrowedE01I01);
            Assert.Equal(employee01.Id, EEBorrowedE01I01.EmployeeId);
            Assert.Equal(item01.Id, EEBorrowedE01I01.EquipmentItemId);
            Assert.Equal((int)EmployeeEquipmentStatus.Borrowed, EEBorrowedE01I01.StatusId);
            Assert.Equal((EmployeeEquipmentStatus.Borrowed).ToString(), EEBorrowedE01I01.Status);
        }

        [Fact]
        public async void CannotBorrowABorrowedItem()
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
            var employee02 = await _employee.CreateEmployee(
                new EmployeeDTO
                {
                    FirstName = "FirstName02",
                    LastName = "LastName02",
                    Email = "email02@testemail.com",
                    Phone = "555-555-5555",
                    Dept = "Dept02",
                    Title = "Title02",
                    StatusId = (int)EmployeeStatus.Current,
                    Status = (EmployeeStatus.Current).ToString(),
                    BranchName = "Branch02",
                    BranchAddress = "Branch Address 02",
                    BranchPhone = "555-555-5555"
                }
            );
            var item01 = await _equipmentItem.Create(
                new EquipmentItemDTO
                {
                    Name = "EquipmentItem01",
                    Value = 10.01m
                }
            );
            EmployeeEquipmentItemDTO EEItemDTOE01I01 = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee01.Id,
                EquipmentItemId = item01.Id,
            };
            EmployeeEquipmentItemDTO EEItemDTOE02I01 = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee02.Id,
                EquipmentItemId = item01.Id,
            };
            var EEItemRepo = BuildEEItemRepo();

            //Act
            var EEBorrowedE01I01 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee01.Id, EEItemDTOE01I01);
            var EENotBorrowedE02I01 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee02.Id, EEItemDTOE02I01);
            var EEStillBorrowedE01I01List = await EEItemRepo.GetAllEmployeeEquipmentRecordsFor(employee01.Id, item01.Id);
            var EEStillBorrowedE01I01 = EEStillBorrowedE01I01List.First();

            //Assert
            Assert.Null(EENotBorrowedE02I01);
            Assert.NotEqual(employee02.Id, EEStillBorrowedE01I01.EmployeeId);

            Assert.NotNull(EEStillBorrowedE01I01);
            Assert.Equal(employee01.Id, EEStillBorrowedE01I01.EmployeeId);
            Assert.Equal(item01.Id, EEStillBorrowedE01I01.EquipmentItemId);
            Assert.Equal((int)EmployeeEquipmentStatus.Borrowed, EEStillBorrowedE01I01.StatusId);
            Assert.Equal((EmployeeEquipmentStatus.Borrowed).ToString(), EEStillBorrowedE01I01.Status);
        }

        [Fact]
        public async void CanUpdateAStatus()
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
                    Value = 10.01m
                }
            );
            EmployeeEquipmentItemDTO EEItemDTOE01I01 = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee01.Id,
                EquipmentItemId = item01.Id,
            };
            var EEItemRepo = BuildEEItemRepo();

            //Act
            var EEBorrowedE01I01 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee01.Id, EEItemDTOE01I01);
            EEBorrowedE01I01.StatusId = (int)EmployeeEquipmentStatus.Lost;
            var EELostE01I01 = await EEItemRepo.UpdateEmployeeEquipmentItemRecord(EEBorrowedE01I01);

            //Assert
            Assert.NotNull(EELostE01I01);
            Assert.Equal(employee01.Id, EELostE01I01.EmployeeId);
            Assert.Equal(item01.Id, EELostE01I01.EquipmentItemId);
            Assert.Equal((int)EmployeeEquipmentStatus.Lost, EELostE01I01.StatusId);
        }

        [Fact]
        public async void CanReturnAnItem()
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
                    Value = 10.01m
                }
            );
            EmployeeEquipmentItemDTO EEItemDTOE01I01 = new EmployeeEquipmentItemDTO
            {
                EmployeeId = employee01.Id,
                EquipmentItemId = item01.Id,
            };
            var EEItemRepo = BuildEEItemRepo();

            //Act
            var EEBorrowedE01I01 = await EEItemRepo.SetEquipmentItemAsBorrowedBy(employee01.Id, EEItemDTOE01I01);
            var EEReturnedE01I01 = await EEItemRepo.ReturnItem(EEBorrowedE01I01);

            //Assert
            Assert.NotNull(EEReturnedE01I01);
            Assert.Equal(employee01.Id, EEReturnedE01I01.EmployeeId);
            Assert.Equal(item01.Id, EEReturnedE01I01.EquipmentItemId);
            Assert.Equal((int)EmployeeEquipmentStatus.Returned, EEReturnedE01I01.StatusId);
            Assert.Equal(EmployeeEquipmentStatus.Returned.ToString(), EEReturnedE01I01.Status);
        }
    }
}