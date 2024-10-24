using Inf_api.Configuration;
using Inf_api.Middlewares;
using Inf_Data;
using Inf_Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddLogging();
builder.Services.AddDatabaseConnection(configuration); // Configuring Db connection

builder.Services
    .AddIdentity<AppUser, AppRoles>()
    .AddRoles<AppRoles>()
    .AddEntityFrameworkStores<InfDbContext>();

builder.Services.AddPresentation(); // Configuring Swagger and API controller
builder.Services.AddAuthorizationConfiguration(configuration); // Configure Jwt Options
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddScopes(); // Adding dependency injection

var app = builder.Build();

// Apply CORS policy
app.UseCors("AllowAllOrigins");

// Ensure the CORS middleware is added before UseAuthorization and UseAuthentication
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

using (var serviceScope = app.Services.CreateScope())
{
    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRoles>>();

    if (!await roleManager.RoleExistsAsync("User"))
    {
        var userRole = new AppRoles("User");
        await roleManager.CreateAsync(userRole);
    }

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        var adminRole = new AppRoles("Admin");
        await roleManager.CreateAsync(adminRole);
    }
}

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
