using DemoAuth0.Config;
using Microsoft.OpenApi.Models;

namespace DemoAuth0.Config;
public static class Swagger
{
    public static void AddSwaggerGen(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Demo Auth0",
                        Version = "v1",
                        Description = "A REST API",
                        TermsOfService = new Uri("https://lmgtfy.com/?q=i+like+pie")
                    });
            //var ab = $"{configuration["Authentication:Domain"]}/authorize?audience={configuration["Authentication:Audience"]}";
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        Scopes = new Dictionary<string, string>
                {
                    { "openid", "Open Id" }
                },
                        AuthorizationUrl = new Uri($"https://{configuration["Authentication:Domain"]}/authorize?audience={configuration["Authentication:Audience"]}"),
                        TokenUrl = new Uri($"https://{configuration["Authentication:Domain"]}/oauth/token")
                    }
                }
            });
            c.OperationFilter<SecurityRequirementsOperationFilter>();
        });

    }

}
