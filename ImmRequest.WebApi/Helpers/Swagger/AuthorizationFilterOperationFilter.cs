using ImmRequest.WebApi.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi
{
    public class AuthorizationFilterOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<AuthorizationFilter>();

            if (authAttributes.Any())
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "Access token",
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "Guid"
                    }
                });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Token invalido o vacio" });
            }                
        }
    }
}
