using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VROOM.Models;
using VROOM.Models.DTOs;
using VROOM.Models.Interfaces;
using VROOM.Models.Services;
using Xunit;

namespace XUnitTestProject1
{
    public class EquipmentItemTests : DatabaseTest
    {
        private IEquipmentItem BuildRepository()
        {
            return new EquipmentItemRepository(_db);
        }

        [Fact]
        public async Task CanCheckIfTheDBIsEmpty()
        {
            // arrange
            var service = BuildRepository();

            await service.Delete(1);
            await service.Delete(2);
            await service.Delete(3);
            await service.Delete(4);
            await service.Delete(5);
            await service.Delete(6);
            await service.Delete(7);
            await service.Delete(8);
            await service.Delete(9);

            // act
            List<EquipmentItemDTO> result = await service.GetEquipmentItems();

            // assert
            Assert.Empty(result);

        }

        [Fact]
        public async Task CanSaveAndGetEquipmentItems()
        {
            // arrange
            var newEquipment = new EquipmentItemDTO()
            {
                Id = 10,
                Name = "Chair Model",
            };

            var repository = BuildRepository();

            // act

            var saved = await repository.Create(newEquipment);

            // assert
            Assert.NotNull(saved);
            Assert.NotEqual(0, newEquipment.Id);
            Assert.Equal(saved.Id, newEquipment.Id);
            Assert.Equal(saved.Name, newEquipment.Name);

        }

        [Fact]
        public async Task GetSingleEquipmentItems()
        {
            // arrange & act
            var service = BuildRepository();

            var result1 = await service.GetEquipmentItem(1);
            var result2 = await service.GetEquipmentItem(2);
            var result3 = await service.GetEquipmentItem(3);
            var result4 = await service.GetEquipmentItem(4);
            var result5 = await service.GetEquipmentItem(5);
            var result6 = await service.GetEquipmentItem(6);
            var result7 = await service.GetEquipmentItem(7);
            var result8 = await service.GetEquipmentItem(8);
            var result9 = await service.GetEquipmentItem(9);

            // assert
            Assert.Equal("World's Best Boss Mug", result1.Name);
            Assert.Equal("Copy Machine", result2.Name);
            Assert.Equal("Stapler", result3.Name);
            Assert.Equal("Megaphone", result4.Name);
            Assert.Equal("Paper Shredder", result5.Name);
            Assert.Equal("Fax Machine", result6.Name);
            Assert.Equal("Lenovo ThinkPad", result7.Name);
            Assert.Equal("Apple MacBook Pro", result8.Name);
            Assert.Equal("HP Pavilion", result9.Name);

        }

        [Fact]
        public async Task GetAllEquipmentItems()
        {
            // arrange & act
            var service = BuildRepository();

            List<EquipmentItemDTO> result = await service.GetEquipmentItems();

            // assert
            Assert.Equal(9, result.Count);

        }

        [Fact]
        public async Task UpdateEquipmentItem()
        {
            // arrange & act
            var service = BuildRepository();

            EquipmentItem updatedEquipmentItem = new EquipmentItem
            {
                Id = 8,
                Name = "Apple iPad Pro",
            };

            EquipmentItem result = await service.Update(updatedEquipmentItem);

            // assert
            Assert.Equal("Apple iPad Pro", result.Name);
            Assert.NotEqual("Apple MacBook Pro", result.Name);

        }

        [Fact]
        public async Task DeleteEquipment()
        {
            // arrange & act
            var service = BuildRepository();
            List<EquipmentItemDTO> result = await service.GetEquipmentItems();

            await service.Delete(1);

            List<EquipmentItemDTO> result2 = await service.GetEquipmentItems();

            // assert
            Assert.Equal(9, result.Count);
            Assert.Equal(8, result2.Count);
            Assert.NotEqual(9, result2.Count);

        }

        [Fact]
        public async Task CannotDeleteFromAnEmptyDB()
        {
            // arrange
            var service = BuildRepository();
            // 9 items are already in the service

            await service.Delete(1);
            await service.Delete(2);
            await service.Delete(3);
            await service.Delete(4);
            await service.Delete(5);
            await service.Delete(6);
            await service.Delete(7);
            await service.Delete(8);
            await service.Delete(9);
            // service is now empty

            // act
            List<EquipmentItemDTO> result = await service.GetEquipmentItems();
            await service.Delete(10);

            // assert
            Assert.Empty(result);

        }
    }
}
