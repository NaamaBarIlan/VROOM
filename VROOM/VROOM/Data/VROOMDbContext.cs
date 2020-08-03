using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VROOM.Models;

namespace VROOM.Data
{
    public class VROOMDbContext : IdentityDbContext<ApplicationUser>
    {
        public VROOMDbContext(DbContextOptions<VROOMDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Employee>().HasData(
                new Employee { Id = 1, FirstName = "Michael" }
                );
            builder.Entity<EquipmentItem>().HasData(
                new EquipmentItem
                {
                    Id = 1,
                    Name = "World's Best Boss Mug",
                    Value = 20
                },
                new EquipmentItem
                {
                    Id = 2,
                    Name = "Copy Machine",
                    Value = 8000
                },
                new EquipmentItem
                {
                    Id = 3,
                    Name = "Stapler",
                    Value = 15
                },
                new EquipmentItem
                {
                    Id = 4,
                    Name = "Megaphone",
                    Value = 50
                },
                new EquipmentItem
                {
                    Id = 5,
                    Name = "Paper Shredder",
                    Value = 100
                },
                new EquipmentItem
                {
                    Id = 6,
                    Name = "Fax Machine",
                    Value = 200
                },
                new EquipmentItem
                {
                    Id = 7,
                    Name = "Lenovo ThinkPad",
                    Value = 700
                },
                new EquipmentItem
                {
                    Id = 8,
                    Name = "Apple MacBook Pro",
                    Value = 1500
                },
                new EquipmentItem
                {
                    Id = 9,
                    Name = "HP Pavilion",
                    Value = 900
                }
                );
            builder.Entity<EmployeeEquipmentItem>().HasKey(x => new { x.EmployeeId, x.EquipmentId });
        }

        // db sets
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EquipmentItem> EquipmentItem { get; set; }
        public DbSet<EmployeeEquipmentItem> EmployeeEquipmentItem { get; set; }

    }
}
