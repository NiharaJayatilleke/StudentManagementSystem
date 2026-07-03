using Swashbuckle.AspNetCore.SwaggerGen;

namespace TestProject
{
    /// <summary>
    /// Extension methods for Swagger/OpenAPI configuration.
    /// Handles JWT Bearer token authentication in Swagger UI.
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Adds JWT Bearer token security definition to Swagger.
        /// Works around .NET 10 compatibility issues by using dynamic instantiation.
        /// </summary>
        public static void AddJwtBearerSecurity(this SwaggerGenOptions options)
        {
            try
            {
                // Dynamically configure JWT security without direct type references
                var openApiAssembly = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(a => a.GetName().Name == "Microsoft.OpenApi");

                if (openApiAssembly == null)
                {
                    System.Diagnostics.Debug.WriteLine("Microsoft.OpenApi assembly not found");
                    return;
                }

                // Get the types from the assembly
                var securitySchemeType = openApiAssembly.GetType("Microsoft.OpenApi.Models.OpenApiSecurityScheme");
                var securityRequirementType = openApiAssembly.GetType("Microsoft.OpenApi.Models.OpenApiSecurityRequirement");
                var referenceType = openApiAssembly.GetType("Microsoft.OpenApi.Models.OpenApiReference");
                var parameterLocationEnum = openApiAssembly.GetType("Microsoft.OpenApi.Models.ParameterLocation");
                var securitySchemeTypeEnum = openApiAssembly.GetType("Microsoft.OpenApi.Models.SecuritySchemeType");
                var referenceTypeEnum = openApiAssembly.GetType("Microsoft.OpenApi.Models.ReferenceType");

                if (securitySchemeType == null)
                {
                    System.Diagnostics.Debug.WriteLine("OpenApiSecurityScheme type not found");
                    return;
                }

                // Create and configure OpenApiSecurityScheme
                var scheme = Activator.CreateInstance(securitySchemeType);
                securitySchemeType.GetProperty("Name")?.SetValue(scheme, "Authorization");
                securitySchemeType.GetProperty("Type")?.SetValue(scheme, Enum.Parse(securitySchemeTypeEnum!, "Http"));
                securitySchemeType.GetProperty("Scheme")?.SetValue(scheme, "bearer");
                securitySchemeType.GetProperty("BearerFormat")?.SetValue(scheme, "JWT");
                securitySchemeType.GetProperty("In")?.SetValue(scheme, Enum.Parse(parameterLocationEnum!, "Header"));
                securitySchemeType.GetProperty("Description")?.SetValue(scheme, "Enter: Bearer {your JWT token}");

                // Add security definition
                var addSecurityDefMethod = options.GetType().GetMethod("AddSecurityDefinition");
                addSecurityDefMethod?.Invoke(options, new[] { "Bearer", scheme });

                // Create security requirement with reference
                var securityReq = Activator.CreateInstance(securityRequirementType!);
                var reference = Activator.CreateInstance(referenceType!);
                referenceType.GetProperty("Type")?.SetValue(reference, Enum.Parse(referenceTypeEnum!, "SecurityScheme"));
                referenceType.GetProperty("Id")?.SetValue(reference, "Bearer");

                var schemeWithRef = Activator.CreateInstance(securitySchemeType);
                securitySchemeType.GetProperty("Reference")?.SetValue(schemeWithRef, reference);

                // Add scheme to requirement
                var addMethod = securityRequirementType.GetMethod("Add");
                addMethod?.Invoke(securityReq, new[] { schemeWithRef, Array.Empty<string>() });

                // Add security requirement
                var addSecurityReqMethod = options.GetType().GetMethod("AddSecurityRequirement");
                addSecurityReqMethod?.Invoke(options, new[] { securityReq });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Warning: Could not configure JWT security in Swagger: {ex.Message}");
            }
        }
    }
}
