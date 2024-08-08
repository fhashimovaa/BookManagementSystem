using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity.Concrete
{
    public class UserRole : EntityBase, IDeletedEntity
    {
       
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public User User { get; set; }
        public bool Deleted { get; set; }
    }
}
