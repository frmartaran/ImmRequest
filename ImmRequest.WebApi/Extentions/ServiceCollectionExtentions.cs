using ImmRequest.BusinessLogic.Helpers.Inputs;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Logic;
using ImmRequest.BusinessLogic.Logic.Finders;
using ImmRequest.BusinessLogic.Logic.ImporterLogic;
using ImmRequest.BusinessLogic.Validators;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using ImmRequest.Domain.UserManagement;
using ImmRequest.WebApi.Helpers;
using ImmRequest.WebApi.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddDataAccessScope(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Session>, SessionRepository>();
            services.AddScoped<IRepository<Administrator>, AdministratorRepository>();
            services.AddScoped<IRepository<CitizenRequest>, CitizenRequestRepository>();
            services.AddScoped<IRepository<Area>, AreaRepository>();
            services.AddScoped<IRepository<Topic>, TopicRepository>();
            services.AddScoped<IRepository<TopicType>, TopicTypeRepository>();
            services.AddScoped<IRepository<BaseField>, FieldsRepository>();
        }

        public static void AddBusinessLogicScope(this IServiceCollection services)
        {
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

            services.AddScoped<AreaImporterInput, AreaImporterInput>();
            services.AddScoped<IImporterLogic, ImporterLogic>();
        }

        public static void AddWebApiScope(this IServiceCollection services)
        {
            services.AddScoped<SessionControllerInputHelper, SessionControllerInputHelper>();
            services.AddScoped<IContextHelper, CurrentSessionInfo>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ImmRequest", Version = "v1" });
                options.IncludeXmlComments(xmlPath);
                options.OperationFilter<AuthorizationFilterOperationFilter>();
            });
        }
    }
}
