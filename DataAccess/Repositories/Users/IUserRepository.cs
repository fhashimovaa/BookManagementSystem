using Core.Entity.Concrete;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccess.Repositories.Users
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IQueryable<User>> GetAllFromDatabase();
        //Task CreateUser(User user);

        Task<User> GetById(int id);

        //Task DeleteUser(User user);

        //Task<User> UpdateUser(User user);

        Task<User> GetByUsername(string userName);
    }
}
