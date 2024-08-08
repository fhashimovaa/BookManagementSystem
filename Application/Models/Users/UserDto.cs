using Application.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.User
{
    public class UserDto : BaseDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public List<UserRoleDto> Roles { get; set; }
        public  List<UserClaimDto> ClaimTypes { get; set; }
    }
}
