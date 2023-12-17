using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_estate.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserDto
    {
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
