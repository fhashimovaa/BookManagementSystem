using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Users
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        
    }
}
