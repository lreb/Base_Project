using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BaseProject.API.Infraestructure.Cors
{
	/// <summary>
	/// Extension to setup CORS configuration
	/// </summary>
	public static class CorsExtension
	{
		/// <summary>
		/// Policy cors name
		/// </summary>
		public static readonly string AllowSpecificOrigins = "AllowSpecificOrigins";

		/// <summary>
		/// CORS configurations
		/// </summary>
		/// <param name="services">application service <see cref="IServiceCollection"/></param>
		/// <param name="configuration">app settings configuration <see cref="IConfiguration"/></param>
		public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddCors(options =>
			{
				options.AddPolicy(AllowSpecificOrigins,
					builder =>
					{
						builder.WithOrigins(configuration.GetSection("Cors:AllowedOrigin").Get<string[]>())
							.AllowAnyHeader()
							.AllowAnyMethod()
							.AllowCredentials();
					});
			});
		}
	}
}
