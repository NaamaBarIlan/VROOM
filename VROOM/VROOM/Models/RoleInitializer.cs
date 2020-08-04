using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VROOM.Data;

namespace VROOM.Models
{
    public class RoleInitializer
    {
        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole{Name = ApplicationRoles.CEO, NormalizedName = ApplicationRoles.CEO.ToUpper(), ConcurrencyStamp=Guid.NewGuid().ToString()},
            new IdentityRole{Name = ApplicationRoles.OfficeManager, NormalizedName = ApplicationRoles.OfficeManager.ToUpper(), ConcurrencyStamp=Guid.NewGuid().ToString()},
            new IdentityRole{Name = ApplicationRoles.Employee, NormalizedName = ApplicationRoles.Employee.ToUpper(), ConcurrencyStamp=Guid.NewGuid().ToString()},
        };

        public static void SeedData(IServiceProvider serviceProvider, UserManager<ApplicationUser> users, IConfiguration _config)
        {
            using (var dbContext = new VROOMDbContext(serviceProvider.GetRequiredService<DbContextOptions<VROOMDbContext>>()))
            {
                dbContext.Database.EnsureCreated();
                AddRoles(dbContext);
                SeedUsers(users, _config);
            }
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager, IConfiguration _config)
        {
            if (userManager.FindByEmailAsync(_config["CEOEmail"]).Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = _config["CEOEmail"];
                user.Email = _config["CEOEmail"];
                user.FirstName = "David";
                user.LastName = "Wallace";

                IdentityResult result = userManager.CreateAsync(user, _config["CEOPassword"]).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, ApplicationRoles.CEO).Wait();
                }
            }

            if (userManager.FindByEmailAsync(_config["OfficeManagerEmail"]).Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = _config["OfficeManagerEmail"];
                user.Email = _config["OfficeManagerEmail"];
                user.FirstName = "Michael";
                user.LastName = "Scott";

                IdentityResult result = userManager.CreateAsync(user, _config["OfficeManagerPassword"]).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, ApplicationRoles.OfficeManager).Wait();
                }
            }

            if (userManager.FindByEmailAsync(_config["EmployeeEmail"]).Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = _config["EmployeeEmail"];
                user.Email = _config["EmployeeEmail"];
                user.FirstName = "James";
                user.LastName = "Halpert";

                IdentityResult result = userManager.CreateAsync(user, _config["EmployeePassword"]).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, ApplicationRoles.Employee).Wait();
                }
            }
        }

        private static void AddRoles(VROOMDbContext context)
        {
            if (context.Roles.Any()) return;

            foreach (var role in Roles)
            {
                context.Roles.Add(role);
                context.SaveChanges();
            }
        }
    }
}
