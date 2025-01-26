using Hangfire;
using Inf_api.Configuration;
using Inf_api.Jobs;
using Inf_api.Middlewares;
using Inf_api.SignalrRNotifications;
using Inf_Data;
using Inf_Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add CORS policy
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((host) => true)
                   .AllowCredentials();
        }));
builder.Services.AddLogging();
builder.Services.AddDatabaseConnection(configuration); // Configuring Db connection

builder.Services
    .AddIdentity<AppUser, AppRoles>()
    .AddRoles<AppRoles>()
    .AddEntityFrameworkStores<InfDbContext>();
builder.Services.AddSignalR();

builder.Services.AddPresentation(); // Configuring Swagger and API controller
builder.Services.AddAuthorizationConfiguration(configuration); // Configure Jwt Options
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddScopes(); // Adding dependency injection
builder.Services.AddScoped<IRandomizePLantHealthJob, RandomizePlantHealthJob>(); // Register the job in DI
builder.Services.AddHangfire(conf =>
{
    conf.UseSqlServerStorage(configuration.GetConnectionString("InformacioniSistemi"));
});
builder.Services.AddHangfireServer();

var app = builder.Build();

// Apply CORS policy
app.UseCors("CorsPolicy");
// Ensure the CORS middleware is added before UseAuthorization and UseAuthentication
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseAuthentication();

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

app.UseHangfireDashboard("/hangfire");
// Register Hangfire recurring job
RecurringJob.AddOrUpdate<IRandomizePLantHealthJob>(
    "RandomizePlantHealth",
    job => job.RandomlyAdjustStateOfThePlant(),
    Cron.Hourly // Adjust frequency as needed
);

app.UseRouting(); // Add routing middleware here
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Map controllers
    endpoints.MapHub<NotificationsHub>("/notificationsHub"); // Map SignalR hub
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
