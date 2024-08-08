using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity.Concrete
{
    public  class User: CommonEntity
    {
        public string UserName { get; set; }
        public byte[]? PasswordHash{ get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string Email { get; set; }
        public List<UserRole>? UserRoles { get; set; }
        public List<UserClaim>? UserClaims { get; set; }

    }
}
