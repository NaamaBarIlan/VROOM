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
                new Employee 
                { 
                    Id = 1, FirstName = "Michael" 
                }
            );

            builder.Entity<EmployeeEquipmentItem>().HasData(
                new EmployeeEquipmentItem
                {
                    EquipmentId = 1,
                    EmployeeId = 1,
                    Status = (int)EmployeeEquipmentStatus.Borrowed,
                    DateBorrowed = DateTime.Today.AddDays(-2.0),
                    DateReturned = DateTime.Today
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
