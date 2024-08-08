using Core.Entity.Concrete;
using DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Repositories.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        //protected readonly BookManagemenContext _context;

        public UserRepository(BookManagemenContext _context) : base(_context)
        {
            //this._context = _context;
        }

        //public async Task CreateUser(User user)
        //{
        //    _context.Users.Add(user);
        //    _context.SaveChanges();
        //}

        public async Task<User> GetById(int id)
        {
            return DbSet
                .Include(e => e.UserRoles).ThenInclude(e => e.Role)
                .Include(e => e.UserClaims).ThenInclude(e => e.Claim)
                .Where(e => e.Id == id).FirstOrDefault();
        }

        public async Task<IQueryable<User>> GetAllFromDatabase()
        {
            return GetQuery()
                .Include(e => e.UserRoles).ThenInclude(e => e.Role)
                .Include(e => e.UserClaims).ThenInclude(e => e.Claim)
                .AsQueryable();
        }

        public async Task<User> GetByUsername(string userName)
        {
            return DbSet
                .Include( e => e.UserRoles).ThenInclude( e=>e.Role)
                .FirstOrDefault(e => e.UserName == userName);
        }

        //public async Task DeleteUser(User user)
        //{
        //    _context.Users .Remove(user);
        //    _context.SaveChanges();
        //}

        //public async Task<User> UpdateUser(User user)
        //{
        //    _context.Update(user);
        //    _context.SaveChanges();
        //    return user;
           
        //}

        
    }
}
