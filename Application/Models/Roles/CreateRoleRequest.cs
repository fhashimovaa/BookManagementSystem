using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Roles
{
    public class CreateRoleRequest 
    {
        public string Name { get; set; }
        public List<int> ClaimIds { get; set; }
    }
}
