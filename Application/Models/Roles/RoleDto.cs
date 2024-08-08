using Application.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Roles
{
    public class RoleDto : BaseDto
    {
        public string Name { get; set; }
        public List<RoleClaimDto> Claims { get; set; }
    }
}
