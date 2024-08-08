using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity.Concrete
{
    public class Claim :KeyValueBase, IDeletedEntity
    {
       
        public string Description { get; set; }

        public List<RoleClaim> RoleClaims { get; set;}

        public List<UserClaim> UserClaims { get; set;}
        public bool Deleted { get; set; }
    }
}
