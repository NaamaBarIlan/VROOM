using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VROOM.Models.DTOs;
using VROOM.Models.Interfaces;
using VROOM.Models.Services;
using Xunit;

namespace XUnitTestProject1
{
    public class EmployeeTests : DatabaseTest
    {

        private IEmployee BuildRepository()
        {
            return new EmployeeRepository(_db);
        }

        [Fact]
        public async Task CanCheckIfTheDBIsEmpty()
        {
            // Arrange

            var service = BuildRepository();

            await service.DeleteEmployee(1);
            await service.DeleteEmployee(2);
            await service.DeleteEmployee(3);

            // Act

            List<EmployeeDTO> result = await service.GetEmployees();

            // Assert
            Assert.Empty(result);

        }

        [Fact]
        public async Task CanSaveAndGetEmployee()
        {
            // Arrange

            EmployeeDTO newEmployee = new EmployeeDTO()
            {
                Id = 99,
                FirstName = "First",
                LastName = "Last",
                Email = "test@test.com",
                Phone = "(222)333-4545",
                Dept = "Testing",
                Title = "Tester",
                BranchName = "Test",
                BranchAddress = "1234 test st",
                BranchPhone = "(222)333-4545",
            };

            var repository = BuildRepository();

            // Act

            var saved = await repository.CreateEmployee(newEmployee);

            // Assert
            Assert.NotNull(saved);
            Assert.NotEqual (0, newEmployee.Id);
            Assert.Equal(saved.Id, newEmployee.Id);
            Assert.Equal(saved.FirstName, newEmployee.FirstName);
        }

        [Fact]
        public async Task CanGetSingleEmployeeItem()
        {
            // Arrange & act
            var service = BuildRepository();

            var result1 = await service.GetSingleEmployee(1);
            var result2 = await service.GetSingleEmployee(2);
            var result3 = await service.GetSingleEmployee(3);

            // Assert
            Assert.Equal("Michael", result1.FirstName);
            Assert.Equal("Pamela", result2.FirstName);
            Assert.Equal("James", result3.FirstName);
        }

        [Fact]
        public async Task CanGetAllEmployees()
        {
            // Arrange & act

            var service = BuildRepository();

            List<EmployeeDTO> result = await service.GetEmployees();

            // Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task CanUpdateEmployee()
        {
            // Arrange & act
            var service = BuildRepository();

            EmployeeDTO newEmployee = new EmployeeDTO()
            {
                Id = 1,
                FirstName = "Michael",
                LastName = "Plot",
                Email = "mscott@vroom.com",
                Phone = "(570)-348-4178",
                Dept = "Management",
                Title = "Regional Manager",
                BranchName = "Scranton Branch",
                BranchAddress = "1725 Slough Avenue, Scranton, PA",
                BranchPhone = "(570) 348-4100",
            };

            EmployeeDTO result = await service.UpdateEmployee(newEmployee.Id, newEmployee);

            // Assert
            Assert.Equal("Plot", result.LastName);
        }

        [Fact]
        public async Task DeleteEmployee()
        {
            // Arrange & act
            var service = BuildRepository();
            List<EmployeeDTO> result = await service.GetEmployees();

            await service.DeleteEmployee(1);

            List<EmployeeDTO> result2 = await service.GetEmployees();

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(2, result2.Count);

        }

        [Fact]
        public async Task CannotDeleteFromAnEmptyDB()
        {
            // Arrange
            var service = BuildRepository();

            await service.DeleteEmployee(1);
            await service.DeleteEmployee(2);
            await service.DeleteEmployee(3);

            // Act
            List<EmployeeDTO> result = await service.GetEmployees();
            await service.DeleteEmployee(4);

            // Assert

            Assert.Empty(result);
        }

    }
}
