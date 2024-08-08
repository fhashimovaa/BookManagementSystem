using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Concrete;

namespace DataAccess.Repositories.Roles
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAll();
        Task<Role> GetById(int id);
        Task CreateRole(Role role);
        Task DeleteRole(Role role);
        Task<Role> UpdateRole(Role role);
    }
}
