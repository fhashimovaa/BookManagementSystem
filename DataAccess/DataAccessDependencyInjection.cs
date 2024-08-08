using DataAccess.Persistence;
using DataAccess.Repositories;
using DataAccess.Repositories.Roles;
using DataAccess.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class DataAccessDependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories();
            services.AddDatabase(configuration);

            return services;
        }
        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookManagemenContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        }
    }
}
