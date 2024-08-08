using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Users
{
    public  class UserClaimDto :BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
