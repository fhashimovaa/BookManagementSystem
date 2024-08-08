using Application.Models.Roles;
using DataAccess.Repositories.Roles;
using Core.Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Application.Exceptions;

namespace Application.Services.Roles
{
    public class RoleService : IRoleService
    {
        protected readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task CreateRole(CreateRoleRequest createRoleRequest)
        {
            Role newRole = new Role();
            newRole.Name = createRoleRequest.Name;

            List<RoleClaim> claims = new List<RoleClaim>();

            foreach (var claimId in createRoleRequest.ClaimIds)
            {
                RoleClaim claim = new RoleClaim();
                claim.ClaimId = claimId;
                claims.Add(claim);
            }
            newRole.RoleClaims = claims;
            await _roleRepository.CreateRole(newRole);
        }

        public async Task DeleteRole(int id)
        {
            Role role = await _roleRepository.GetById(id);
            await _roleRepository.DeleteRole(role);
        }

        public async Task<RoleDto> GetRole(int id)
        {
            Role role= await _roleRepository.GetById(id);

            if (role is null) throw new  BadRequestException("Bele bir role yoxdur");

            RoleDto roleDto = new RoleDto();
            roleDto.Id = id;
            roleDto.Name = role.Name;
            
            List<RoleClaimDto> claimDtos = new List<RoleClaimDto>();

            foreach (var claim in role.RoleClaims)
            {
                RoleClaimDto claimDto = new RoleClaimDto();
                claimDto.Id = claim.Claim.Id;
                claimDto.Name= claim.Claim.Name;
                claimDto.Description = claim.Claim.Description;
                claimDtos.Add(claimDto);
            }
            roleDto.Claims = claimDtos;
            return roleDto;
        }

        public async Task<List<RoleDto>> GetRoles()
        {
            List<RoleDto> roleDtos = new List<RoleDto>();
            List<Role> roles = await _roleRepository.GetAll();

            foreach (var  role in roles)
            {
                RoleDto roleDto = new RoleDto();
                roleDto.Id = role.Id;
                roleDto.Name = role.Name;


                List<RoleClaimDto> roleClaims = new List<RoleClaimDto>();

                foreach (var claim in role.RoleClaims)
                {
                    RoleClaimDto roleClaim = new RoleClaimDto();
                    roleClaim.Id = claim.Claim.Id;
                    roleClaim.Name = claim.Claim.Name;
                    roleClaim.Description = claim.Claim.Description;
                    roleClaims.Add(roleClaim);
                }
                roleDto.Claims = roleClaims;
                roleDtos.Add(roleDto);
            }
            return roleDtos;
        }

        public async Task<UpdateRoleDto> UpdateRole(UpdateRoleRequest updateRoleRequest)
        {
            Role role = await _roleRepository.GetById(updateRoleRequest.Id);
            if (role is null) throw new BadRequestException("Bele bir role yoxdur");

            role.Name = updateRoleRequest.Name;

            List<RoleClaim> roleClaims = new List<RoleClaim>();

            foreach (var claimId in updateRoleRequest.ClaimIds)
            {
                RoleClaim roleClaim = new RoleClaim();
                roleClaim.ClaimId = claimId;
                roleClaims.Add(roleClaim) ;
            }
            role.RoleClaims = roleClaims;

            await _roleRepository.UpdateRole(role);

            return new UpdateRoleDto()
            {
                Name = updateRoleRequest.Name,
                ClaimIds=role.RoleClaims.Select(x => x.ClaimId).ToList()
            };



        }
    }
}
