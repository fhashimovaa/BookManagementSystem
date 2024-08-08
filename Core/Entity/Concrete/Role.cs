using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity.Concrete
{
    public class Role : CommonEntity, ICreatedByEntity
    {
        
        public string Name { get; set; }

        public List<UserRole> UserRoles { get; set; }

        public List<RoleClaim> RoleClaims { get; set; }
        public int? CreatedById { get; set; }
        public User CreatedBy { get; set; }
    }
}
