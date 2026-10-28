using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Application.DTO.User
{
    public  class ResgisterDto
    {
        [Required,EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
        [Required]
        public string UserName { get; set; } = default!;
        [Required]
        public string DisplayName { get; set; } = default!;
        public int  PhoneNumber { get; set; } = default!;






    }
}
