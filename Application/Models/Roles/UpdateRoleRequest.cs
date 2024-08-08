using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Roles
{
    public class UpdateRoleRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> ClaimIds { get; set; }
    }
}
