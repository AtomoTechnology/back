using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Identity.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Int32 state { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
