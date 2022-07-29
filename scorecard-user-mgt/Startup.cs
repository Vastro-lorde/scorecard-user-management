using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using scorecard_user_mgt.Data;
using scorecard_user_mgt.DTOs.Mapping;
using scorecard_user_mgt.Interfaces;
using scorecard_user_mgt.Models;
using scorecard_user_mgt.Repositories;
using scorecard_user_mgt.Services;
using scorecard_user_mgt.Services.Extensions;
using static scorecard_user_mgt.Seeders;

namespace scorecard_user_mgt
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Add Jwt Authentication and Authorization
            services.ConfigureAuthentication(Configuration);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenGen, TokenGen>();
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IConfirmationMailService, ConfirmationMailService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddAutoMapper(typeof(UserMappings));
            services.Configure<ImageUploadSettings>(Configuration.GetSection("ImageUploadSettings"));
            services.Configure<MailSettings>(Configuration.GetSection("MailSetting"));

            services.AddDefaultIdentity<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();
            var imageUploadConfig = Configuration
                .GetSection("ImageUploadSettings")
                .Get<ImageUploadSettings>();
            services.AddSingleton(imageUploadConfig);
            services.AddCors(e => e.AddDefaultPolicy(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            ));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "scorecard_user_mgt", Version = "v1" });
               
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the input below. \r\n\r\n Example : 'Bearer 124fsfs'"
                }
                );
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                        new string[] {}
                    }
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager, AppDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "scorecard_user_mgt v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            Seeder.Seed(roleManager, userManager, dbContext).GetAwaiter().GetResult();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}