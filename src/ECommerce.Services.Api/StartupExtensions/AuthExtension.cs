using ECommerce.Infra.CrossCutting.Identity.Authorization;
using ECommerce.Infra.CrossCutting.Identity.Data;
using ECommerce.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Api.StartupExtensions
{
    public static class AuthExtension
    {
        public static IServiceCollection AddCustomizedAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration.GetValue<string>("SecretKey");
            var _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanWriteCampaignData", new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .RequireRole(Roles.Admin)
                    .AddRequirements(new ClaimRequirement("Campaigns_Write", "Write"))
                    .Build());

                options.AddPolicy("CanWriteProductData", new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .RequireRole(Roles.Admin)
                    .AddRequirements(new ClaimRequirement("Products_Write", "Write"))
                    .Build());
            });

            return services;
        }

        public static IApplicationBuilder UseCustomizedAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            var scope = app.ApplicationServices.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            CheckAnnCreateRole(roleManager, Roles.Admin).Wait();
            CheckAnnCreateRole(roleManager, Roles.User).Wait();
            return app;
        }

        private static async Task CheckAnnCreateRole(RoleManager<IdentityRole> roleManager, string role)
        {
            var roleExists = await roleManager.RoleExistsAsync(role);
            if (roleExists)
            {
                return;
            }
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
