using Application.Models.Roles;
using Application.Services.Roles;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RoleController : BaseController
    {
        protected readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet ("/roles")]

        public async Task<List<RoleDto>> GetAll()
        {
            return await _roleService.GetRoles();
        }


        [HttpGet ("/role/{id}")]

        public async Task<RoleDto> Get(int id)
        {
            return await _roleService.GetRole(id);
        }

        [HttpPost ("/role")]

        public async Task<IActionResult> Create([FromBody]CreateRoleRequest createRoleRequest)
        {
            await _roleService.CreateRole(createRoleRequest);
            return Ok(createRoleRequest);
        }

        [HttpDelete ("/role/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            await _roleService.DeleteRole(id);
            return Ok();
        }

        [HttpPut ("/role")]

        public async Task<UpdateRoleDto> Update([FromBody]UpdateRoleRequest updateRoleRequest)
        {
            UpdateRoleDto updateRole=await _roleService.UpdateRole(updateRoleRequest);
            return updateRole;
        }
    }
}
