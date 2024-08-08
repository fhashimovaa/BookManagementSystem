using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity.Concrete
{
    public  class UserClaim : EntityBase, IDeletedEntity
    {
        public int UserId { get; set; }
        public int ClaimId { get; set; }
        public User User { get; set; }
        public Claim Claim { get; set; }
        public bool Deleted { get; set; }
    }
}
