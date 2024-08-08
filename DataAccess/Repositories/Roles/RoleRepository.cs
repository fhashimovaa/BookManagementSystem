using Core.Entity.Concrete;
using DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Roles
{
    public class RoleRepository : IRoleRepository
    {
        protected readonly BookManagemenContext _context;

        public RoleRepository(BookManagemenContext bookManagemenContext)
        {
            _context = bookManagemenContext;
        }

        public async Task CreateRole(Role role)
        {
            _context.Add(role);
            _context.SaveChanges();
        }

        public async Task DeleteRole(Role role)
        {
           _context.Roles.Remove(role);
           _context.SaveChanges();
        }

        public async Task<List<Role>> GetAll()
        {
          return _context.Roles
           .Include(e => e.RoleClaims)
           .ThenInclude(e => e.Claim)
           .ToList();
        }

        public async Task<Role> GetById(int id)
        {
          return _context.Roles
          .Include(e => e.RoleClaims)
          .ThenInclude(e => e.Claim)
          .FirstOrDefault(e => e.Id == id);   
        }

        public async Task<Role> UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
            return role;
        }
    }
}
