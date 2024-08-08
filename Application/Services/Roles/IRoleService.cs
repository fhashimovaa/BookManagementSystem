using Application.Models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Roles
{
    public interface IRoleService
    {
       Task<List<RoleDto>> GetRoles();
       Task<RoleDto> GetRole(int id);
       Task CreateRole(CreateRoleRequest createRoleRequest);
       Task DeleteRole(int id);
       Task<UpdateRoleDto> UpdateRole(UpdateRoleRequest updateRoleRequest);
    }
}
