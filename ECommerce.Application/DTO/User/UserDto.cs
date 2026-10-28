using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.DTO.User
{
    public  class UserDto
    {
        public string Email { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public String Token { get; set; } = default!;

    }
}
