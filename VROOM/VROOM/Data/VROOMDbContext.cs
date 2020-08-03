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
            
            builder.Entity<EmployeeEquipmentItem>().HasKey(x => new { x.EmployeeId, x.EquipmentId });

            // Seeding Employee Data:
            builder.Entity<Employee>().HasData(
                new Employee 
                { 
                    Id = 1,
                    FirstName = "Michael",
                    LastName = "Scott",
                    Email = "mscott@vroom.com",
                    Phone = "(570)-348-4178",
                    Dept = "Management",
                    Title = "Regional Manager",
                    BranchName = "Scranton Branch",
                    BranchAddress = "1725 Slough Avenue, Scranton, PA",
                    BranchPhone = "(570) 348-4100"
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Pamela",
                    LastName = "Beesly",
                    Email = "pbeesly@vroom.com",
                    Phone = "(570) 348-4118",
                    Dept = "Administration",
                    Title = "Office Manager",
                    BranchName = "Scranton Branch",
                    BranchAddress = "1725 Slough Avenue, Scranton, PA",
                    BranchPhone = "(570) 348-4100"
                },
                new Employee
                {
                    Id = 3,
                    FirstName = "James",
                    LastName = "Halpert",
                    Email = "jhalpert@vroom.com",
                    Phone = "(570) 348-4186",
                    Dept = "Sales",
                    Title = "Sales Representative",
                    BranchName = "Scranton Branch",
                    BranchAddress = "1725 Slough Avenue, Scranton, PA",
                    BranchPhone = "(570) 348-4100"
                }
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
