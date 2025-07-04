﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodePluse.API.Dependecis
{
    public class SwaggerFileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var fileUploadMime = "multipart/form-data";
            if (operation.RequestBody == null || !operation.RequestBody.Content.Any(x => x.Key.Equals(fileUploadMime)))
                return;

            var fileParams = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType == typeof(IFormFile));

            operation.RequestBody.Content[fileUploadMime].Schema.Properties =
                fileParams.ToDictionary(
                    k => k.Name,
                    v => new OpenApiSchema
                    {
                        Type = "string",
                        Format = "binary"
                    });
        }
    }
}
