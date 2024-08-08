using Application.Exceptions;
using Application.Helpers;
using Application.Models.User;
using Application.Models.Users;
using Core.Entity.Concrete;
using DataAccess.Repositories.Users;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

namespace Application.Services.Users
{
    public class UserService : BaseService, IUserService
    {
        protected readonly IUserRepository _userRepository;

        public UserService(IHttpContextAccessor httpContextAccessor, IUserRepository _userRepository) : base(httpContextAccessor)
        {
            this._userRepository = _userRepository;
        }

        public async Task CreateUser(CreateUserDto createUserDto)
        {
            int userId = UserId;
            User user = await _userRepository.GetByUsername(createUserDto.UserName);
            if (user != null) throw new BadRequestException("Bu istifadeci adi artiq istifade olunub");
            
            User newUser = new User();
            newUser.UserName = createUserDto.UserName;
            newUser.Email = createUserDto.Email;

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(createUserDto.Password, out passwordHash, out passwordSalt);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

           List<UserRole> roles = new List<UserRole>();

            foreach (var roleId in createUserDto.RoleIds )
            {
                UserRole role = new UserRole();
                role.RoleId = roleId;
                roles.Add(role);

            }
            newUser.UserRoles = roles;
            

            //List<UserClaim> claims = new List<UserClaim>();

            //foreach (var claimId in createUserDto.ClaimIds)
            //{
            //    UserClaim claim = new UserClaim();
            //    claim.ClaimId = claimId;
            //    claims.Add(claim);
            //}
            //newUser.UserClaims = claims;

            newUser.UserClaims=createUserDto.ClaimIds
                .Select(claimId => new UserClaim (){ ClaimId = claimId })
                .ToList();

            await _userRepository.AddAsync(newUser);
        }

        public async Task DeleteUser(int id)
        {
            User user=await _userRepository.GetById(id);
            await _userRepository.DeleteAsync(user);
        }

        public async Task<UserDto> GetUserById(int id)
        {
            User userDetail = await _userRepository.GetById(id);

            if (userDetail is null) throw new BadRequestException("Bele bir user tapilmadi");
            
            UserDto user = new UserDto();
            user.UserName = userDetail.UserName;
            user.Email = userDetail.Email;


           
            List<UserRoleDto> roles = new List<UserRoleDto>();
            foreach (var userRole in userDetail.UserRoles)
            {
                UserRoleDto role = new UserRoleDto();
                role.Id = userRole.Role.Id;
                role.Name = userRole.Role.Name;
            }
            user.Roles = roles;

            List<UserClaimDto> claim = new List<UserClaimDto>();
            foreach (var userClaim in userDetail.UserClaims)
            {
                UserClaimDto claimDto = new UserClaimDto();
                claimDto.Description = userClaim.Claim.Description;
                claimDto.Name = userClaim.Claim.Name;
                claimDto.Id = userClaim.Claim.Id;
                claim.Add(claimDto);
            }
                
           
            return user;

        }

        public async Task<List<UserDto>> GetUsers()
        {
            List<UserDto> userDtos = new List<UserDto>();
            IQueryable<User> users = await _userRepository.GetAllFromDatabase();

            foreach (var user in users)
            {
                UserDto userDto = new UserDto();

                userDto.Id = user.Id;
                userDto.UserName = user.UserName;
                userDto.Email = user.Email;
                
                List<UserRoleDto> roles = new List<UserRoleDto>();

                foreach (var role in user.UserRoles)
                {
                    UserRoleDto roleDto= new UserRoleDto();
                    roleDto.Id = role.Role?.Id;
                    roleDto.Name = role.Role?.Name;
                    roles.Add(roleDto);
                }
                userDto.Roles = roles;
               
                List<UserClaimDto> claims = new List<UserClaimDto>();

                foreach (var claim in user.UserClaims)
                {
                    UserClaimDto claimDto = new UserClaimDto();
                    claimDto.Description=claim.Claim?.Description;
                    claimDto.Name = claim.Claim?.Name;
                    claimDto.Id = claim.Claim?.Id;
                    claims.Add(claimDto);
                }
                userDto.ClaimTypes = claims;
                userDtos.Add(userDto);
            }

            return userDtos;
        }

        public async Task<UpdateUserDto> UpdateUser(UpdateUserRequest updateUserRequest)
        {
            User userCheck = await _userRepository.GetByUsername(updateUserRequest.UserName);
            if(userCheck != null) throw new BadImageFormatException("Bu istifadeci adi artiq istifade olunub");

            User user=await _userRepository.GetById(updateUserRequest.Id);

            if (user == null) throw new BadRequestException ("istifadeci tapilmadi.");
            
            user.UserName = updateUserRequest.UserName;
            user.Email = updateUserRequest.Email;

           await _userRepository.UpdateAsync(user);

            return new UpdateUserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
            };
            
        }
    }

    
}
