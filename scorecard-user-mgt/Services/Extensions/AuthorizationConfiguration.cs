using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using scorecard_user_mgt.Utilities;
using System;
using System.Text;

namespace scorecard_user_mgt.Services.Extensions
{
    public static class AuthorizationConfiguration
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuration["JWTSettings:Audience"],
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };

            });

            services.AddAuthorization(options =>
                    options.AddPolicy("RequireAdminOnly", policy => policy.RequireRole(UserRoles.Admin)))
                .AddAuthorization(options => options.AddPolicy("RequirePAOnly", policy => policy.RequireRole(UserRoles.PA)))
                .AddAuthorization(options => options.AddPolicy("RequireDevOnly", policy => policy.RequireRole(UserRoles.Dev)))
                .AddAuthorization(options => options.AddPolicy("RequirePAAndDev", policy => policy.RequireRole(UserRoles.PA, UserRoles.Dev)));
        }
    }
}
