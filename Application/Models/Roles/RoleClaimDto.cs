using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Roles
{
    public class RoleClaimDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
