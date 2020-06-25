using ImmRequest.BusinessLogic;
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
using ImmRequest.WebApi.Extentions;
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
            services.AddControllers().AddNewtonsoftJson();
            services.AddDbContext<ImmDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDataAccessScope();
            services.AddBusinessLogicScope();
            services.AddWebApiScope();
            services.ConfigureSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

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
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Imm Request");
            });
        }
    }
}
