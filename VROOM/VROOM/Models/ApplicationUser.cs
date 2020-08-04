using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VROOM.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

    }

    public static class ApplicationRoles
    {
        public const string CEO = "CEO";

        public const string OfficeManager = "Office Manager";

        public const string Employee = "Employee";
    }
}
