using BaseProject.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BaseProject.API.Infraestructure.Database
{
    /// <summary>
    /// The Service Collection Extensions
    /// </summary>
    /// <seealso cref="IServiceCollection"/>
    public static class MicrosoftSqlExtension
    {
        /// <summary>
        /// Set up the Service SQL DB Context
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/></param>
        //public static void UseInMemoryDatabase(this IServiceCollection serviceCollection)
        //{
        //    // TODO: use your context
        //    serviceCollection.AddDbContext<ApplicationDbContext>(opts =>
        //      opts.UseInMemoryDatabase("AlbumsDB")
        //      .EnableSensitiveDataLogging());
        //}

        /// <summary>
        /// Set up the Service SQL DB Context
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="connectionString">The data migration connection string</param>
        public static void UseSqlServer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options
                .UseSqlServer(connectionString)
            );
        }
    }
}
