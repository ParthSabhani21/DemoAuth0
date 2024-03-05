using Auth0.AspNetCore.Authentication;
using DemoAuth0.Config;
using DemoAuth0.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddTransient<IUserService, UserService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthenticationConfig(builder.Configuration);

// DatabaseConnection
builder.Services.AddSqlServerConfig(builder.Configuration);

// Swagger
builder.Services.AddSwaggerGen(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
        c.OAuthClientId(builder.Configuration["Authentication:ClientId"]);
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseAuthentication();
app.UseAuthorization();

//app.UseMvc();

app.MapControllers();


app.Run();
