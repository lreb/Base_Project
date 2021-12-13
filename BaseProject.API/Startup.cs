using BaseProject.API.Infraestructure.Cors;
using BaseProject.API.Infraestructure.Database;
using BaseProject.API.Infraestructure.DependencyInjection;
using BaseProject.API.Infraestructure.Environment;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

namespace BaseProject.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #region Swagger service
            // enable swagger service
            //services.ConfigureSwaggerExtension(Configuration);
            #endregion

            #region Cors service
            // enable policy cors service
            services.ConfigureCors(Configuration);
            #endregion

            #region Database context service            
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options
            //    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            //);
            services.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            #endregion

            //services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            //services.AddScoped<IApplicationWriteDbConnection, ApplicationWriteDbConnection>();
            //services.AddScoped<IApplicationReadDbConnection, ApplicationReadDbConnection>();
            services.DatabaseContextInyectionExtension();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BaseProject.API", Version = "v1" });
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(CorsExtension.AllowSpecificOrigins);

            if (env.IsLocal())
            {
                app.UseDeveloperExceptionPage();
                //app.EnableSwaggerPipeline(Configuration);
            }
            else if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.EnableSwaggerPipeline(Configuration);
            }
            else if (env.IsStaging())
            {
                //app.EnableSwaggerPipeline(Configuration);
            }
            else
            {
                //app.EnableSwaggerPipeline(Configuration);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BaseProject.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
