using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Entitys
{
    public  class ApplicationUser : IdentityUser    {
        public string DisplayName { get; set; } = default!;
        public Address? Address { get; set; }
    }
}
