using ImmRequest.BusinessLogic;
using ImmRequest.BusinessLogic.Extensions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Logic;
using ImmRequest.BusinessLogic.Logic.Finders;
using ImmRequest.BusinessLogic.Validators;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
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

            services.AddControllers().AddNewtonsoftJson();

            services.AddDbContext<ImmDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddScoped<SessionControllerInputHelper, SessionControllerInputHelper>();
            services.AddScoped<CitizenRequestValidatorInput, CitizenRequestValidatorInput>();
            services.AddScoped<IContextHelper, CurrentSessionInfo>();

            services.AddScoped<IRepository<Session>, SessionRepository>();
            services.AddScoped<IRepository<Administrator>, AdministratorRepository>();
            services.AddScoped<IRepository<CitizenRequest>, CitizenRequestRepository>();
            services.AddScoped<IRepository<Area>, AreaRepository>();
            services.AddScoped<IRepository<Topic>, TopicRepository>();
            services.AddScoped<IRepository<TopicType>, TopicTypeRepository>();
            services.AddScoped<IRepository<BaseField>, FieldsRepository>();

            services.AddScoped<IValidator<Session>, SessionValidator>();
            services.AddScoped<IValidator<Administrator>, AdministratorValidator>();
            services.AddScoped<IValidator<CitizenRequest>, CitizenRequestValidator>();
            services.AddScoped<IValidator<TopicType>, TopicTypeValidator>();

            services.AddScoped<ISessionLogic, SessionLogic>();
            services.AddScoped<IAdministratorLogic, AdministratorLogic>();
            services.AddScoped<ILogic<TopicType>, TopicTypeLogic>();
            services.AddScoped<ILogic<CitizenRequest>, CitizenRequestLogic>();

            services.AddScoped<IFinder<Area>, AreaFinder>();
            services.AddScoped<IFinder<Topic>, TopicFinder>();


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ImmRequest", Version = "v1" });
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
