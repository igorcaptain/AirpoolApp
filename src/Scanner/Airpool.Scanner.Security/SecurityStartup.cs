using Airpool.Scanner.Security.Contract;
using Airpool.Scanner.Security.Data;
using Airpool.Scanner.Security.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Airpool.Scanner.Security
{
    public static class SecurityStartup
    {
        public static IServiceCollection AddAirpoolSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });

            services.AddDbContext<SecurityContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("ScannerConnection")), ServiceLifetime.Singleton);
                //c.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ScannerDb;Trusted_Connection=True;MultipleActiveResultSets=true"), ServiceLifetime.Singleton);

            services.AddTransient<IUserService, UserService>();

            services.AddControllers()
                .AddApplicationPart(typeof(SecurityStartup).Assembly);

            return services;
        }
    }
}
