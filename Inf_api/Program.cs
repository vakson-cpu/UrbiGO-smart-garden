using Inf_api.Configuration;
using Inf_api.Middlewares;
using Inf_Data;
using Inf_Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddLogging();
builder.Services.AddDatabaseConnection(configuration); //Configuring Db connection
builder.Services
    .AddIdentity<AppUser, AppRoles>()
    .AddRoles<AppRoles>()
    .AddEntityFrameworkStores<InfDbContext>();

builder.Services.AddPresentation(); //Configuring swagger and api controller
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddScopes(); //Adding dependency Injection
builder.Services.AddAuthorizationConfiguration(configuration); //Configure Jwt Options

var app = builder.Build();
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
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseCors(
    x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials()
);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
