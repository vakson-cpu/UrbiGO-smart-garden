using Hangfire;
using Inf_api.Configuration;
using Inf_api.Jobs;
using Inf_api.Middlewares;
using Inf_api.SignalrRNotifications;
using Inf_Data;
using Inf_Data.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Services
builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", p =>
    p.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(_ => true).AllowCredentials()));
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddDatabaseConnection(configuration);

builder.Services
    .AddIdentity<AppUser, AppRoles>()
    .AddRoles<AppRoles>()
    .AddEntityFrameworkStores<InfDbContext>();

builder.Services.AddSignalR();
builder.Services.AddControllers();                     // <-- ensure controllers are registered
builder.Services.AddPresentation();                    // Swagger + MVC options (ok to keep)
builder.Services.AddAuthorizationConfiguration(configuration);
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddScopes();

builder.Services.AddScoped<IRandomizePLantHealthJob, RandomizePlantHealthJob>();
builder.Services.AddHangfire(conf =>
    conf.UseSqlServerStorage(configuration.GetConnectionString("InformacioniSistemi")));
builder.Services.AddHangfireServer();

var app = builder.Build();

// ---- PIPELINE ORDER ----
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseRouting();                   // 1) routing
app.UseCors("CorsPolicy");          // 2) cors (after routing, before auth)
app.UseAuthentication();            // 3) auth
app.UseAuthorization();             // 4) authorization

// --- Minimal test endpoints (anonymous) ---
app.MapGet("/", () => Results.Ok(new { status = "ok" })).AllowAnonymous();
app.MapGet("/healthz", () => Results.Ok(new
{
    status = "ok",
    env = app.Environment.EnvironmentName,
    timeUtc = DateTimeOffset.UtcNow
})).AllowAnonymous();
app.MapGet("/__debug/headers", (HttpRequest req) =>
    Results.Json(req.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()))
).AllowAnonymous();

// Map your real endpoints
app.MapControllers();                                   // attribute-routed controllers
app.MapHub<NotificationsHub>("/notificationsHub");      // SignalR hub

// Hangfire
app.UseHangfireDashboard("/hangfire");
// Recurring jobs
RecurringJob.AddOrUpdate<IRandomizePLantHealthJob>(
    "RandomizePlantHealth",
    job => job.RandomlyAdjustStateOfThePlant(),
    Cron.Hourly
);

// Swagger (enable temporarily in prod if you need)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
