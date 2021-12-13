using BaseProject.API.Contract.DbContext;
using BaseProject.API.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace BaseProject.API.Infraestructure.DependencyInjection
{
    public static class DependecnyInyectionExtension
    {
        /// <summary>
        /// Register db context 
        /// </summary>
        /// <param name="services">Application services<see cref="IServiceCollection"/></param>
        public static void DatabaseContextInyectionExtension(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IApplicationWriteDbConnection, ApplicationWriteDbConnection>();
            services.AddScoped<IApplicationReadDbConnection, ApplicationReadDbConnection>();
        }
    }
}
