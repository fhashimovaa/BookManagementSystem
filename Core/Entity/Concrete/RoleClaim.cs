using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity.Concrete
{
    public  class RoleClaim : EntityBase, IDeletedEntity
    {
        

        public int RoleId { get; set; }
        public int ClaimId { get; set; }
        public Role Role { get; set; }

        public Claim Claim { get; set; }
        public bool Deleted { get; set; }
    }
}
