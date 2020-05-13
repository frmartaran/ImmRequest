using ImmRequest.BusinessLogic;
using ImmRequest.BusinessLogic.Extensions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Logic;
using ImmRequest.BusinessLogic.Logic.Finders;
using ImmRequest.BusinessLogic.Validators;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.DataAccess.Repostories;
using ImmRequest.Domain;
using ImmRequest.Domain.UserManagement;
using ImmRequest.WebApi.Helpers;
using ImmRequest.WebApi.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace ImmRequest.WebApi
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    );
            });

            services.AddControllers();

            services.AddDbContext<ImmDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddScoped<SessionControllerInputHelper, SessionControllerInputHelper>();
            services.AddScoped<IContextHelper, CurrentSessionInfo>();
            services.AddScoped<IRepository<Session>, SessionRepository>();
            services.AddScoped<IValidator<Session>, SessionValidator>();
            services.AddScoped<IRepository<Administrator>, AdministratorRepository>();
            services.AddScoped<IValidator<Administrator>, AdministratorValidator>();
            services.AddScoped<ISessionLogic, SessionLogic>();
            services.AddScoped<IAdministratorLogic, AdministratorLogic>();

            services.AddScoped<IRepository<Area>, AreaRepository>();
            services.AddScoped<IRepository<Topic>, TopicRepository>();

            services.AddScoped<IFinder<Area>, AreaFinder>();
            services.AddScoped<IFinder<Topic>, TopicFinder>();

            services.AddScoped<ILogic<CitizenRequest>, CitizenRequestLogic>();
            services.AddScoped<IRepository<CitizenRequest>, CitizenRequestRepository>();
            services.AddScoped<IValidator<CitizenRequest>, CitizenRequestValidator>();
            services.AddScoped<CitizenRequestValidatorInput, CitizenRequestValidatorInput>();

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ImmRequest", Version = "v1" });
                options.IncludeXmlComments(xmlPath);
                options.OperationFilter<AuthorizationFilterOperationFilter>();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Imm Request");
            });
        }
    }
}
