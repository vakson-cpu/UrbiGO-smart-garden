using System.Security.Claims;
using System.Text;
// using System.Text;
using System.Text.Json.Serialization;
using Inf_api.Helpers;
using Inf_api.Jobs;
using Inf_api.Services.Auth;
using Inf_api.Services.Devices;
using Inf_api.Services.Notifications;
using Inf_api.Services.Plant;
using Inf_api.Services.PlantSpecs;
using Inf_api.SignalrRNotifications;
using Inf_Data;
using Inf_Repository.Repository.Devices;
using Inf_Repository.Repository.Notifications;
using Inf_Repository.Repository.Plant;
using Inf_Repository.Repository.PlantSpecs;
using Inf_Repository.Repository.UnitOfWork;
using Inf_Repository.Repository.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Inf_api.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabaseConnection(
            this IServiceCollection Services,
            IConfiguration configuration
        )
        {
            Services.AddDbContext<InfDbContext>(
                options =>
                    options.UseSqlServer(configuration.GetConnectionString("InformacioniSistemi"))
            );
            return Services;
        }

        public static IServiceCollection AddPresentation(this IServiceCollection Services)
        {
            Services
                .AddControllers()
                .AddJsonOptions(
                    x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
                );

            Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    }
                );
                option.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                    }
                );
            });
            return Services;
        }

        public static IServiceCollection AddScopes(this IServiceCollection Services)
        {
            Services.AddScoped<IUserRepository, IUserRepository>();
            Services.AddTransient<IPlantRepository, PlantRepository>();
            Services.AddScoped<IPlantService, PlantService>();  
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<ITokenService,TokenService>();
            Services.AddTransient<IAuthService, AuthService>();
            Services.AddScoped<IPlantSpecificationRepository, PlantSpecificationRepostiory>();
            Services.AddScoped<IPlantSpecService,PlantSpecService>();
            Services.AddScoped<IDeviceService,DeviceService>();
            Services.AddTransient<IDeviceRepository, DeviceRepository>();
            Services.AddTransient<IPlantHelper, PlantHelper>();
            Services.AddScoped<IRandomizePLantHealthJob, RandomizePlantHealthJob>();
            Services.AddScoped<Inf_Repository.Repository.Notifications.INotificationRepository, NotificationRepository>();
            Services.AddScoped<ISignalRService, SignalRService>();
            return Services;
        }

        public static IServiceCollection AddAuthorizationConfiguration(
            this IServiceCollection Services,
            IConfiguration Configuration
        )
        {
            Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])
                        ),
                        RoleClaimType = ClaimTypes.Role,
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                    };
                });
            Services.AddAuthorization();

            return Services;
        }
    }
}
