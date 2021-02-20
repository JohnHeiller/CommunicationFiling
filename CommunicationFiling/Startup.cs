using CommunicationFiling.DAL.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using CommunicationFiling.DAL.Contracts;
using CommunicationFiling.DAL.Repositories;
using System.IO;
using System.Reflection;

namespace CommunicationFiling
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
            services.AddControllers();

            #region Automapper
            //Mapper para la aplicacion.
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddCors();
            services.AddMvc();
            #endregion

            #region BDConnection
            //var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CommFilingContext>(config =>
            {
                config.UseInMemoryDatabase("CommFilingDB");//.UseSqlServer("DBConnection");
            });
            #endregion

            #region Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "COMMUNICATION FILING API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.EnableAnnotations();
                options.CustomSchemaIds(type => type.ToString());
            });
            #endregion

            #region RepositoriesImplementation
            services.AddScoped<IActionRepo, ActionRepo>();
            services.AddScoped<IAuditRepo, AuditRepo>();
            services.AddScoped<ICorrespondenceTypeRepo, CorrespondenceTypeRepo>();
            services.AddScoped<IFilingRepo, FilingRepo>();
            services.AddScoped<IRoleRepo, RoleRepo>();
            services.AddScoped<IRoleActionRepo, RoleActionRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUserRoleRepo, UserRoleRepo>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region Logger
            loggerFactory.AddFile("Logs/commfiling_log-{Date}.txt");
            #endregion

            #region Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "COMMUNICATION FILING API V1");
            });
            #endregion
        }
    }
}
