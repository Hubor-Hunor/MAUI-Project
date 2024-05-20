using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Owin.Security.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkinOut.Data.Database;
using WorkinOut.Data.IRepository;
using WorkinOut.Data.Repository;
using WorkinOut.Models;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

[assembly: OwinStartup(typeof(WorkinOut.Startup))]

namespace WorkinOut
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("localhost",
            //        builder =>
            //        {
            //            builder.WithOrigins("http://localhost:4200")
            //                .AllowAnyHeader()
            //                .AllowAnyMethod();
            //        });
            //});


            services.AddIdentity<Account, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
            })
                    .AddEntityFrameworkStores<WorkoutDbContext>()
                    .AddDefaultTokenProviders()
                    .AddSignInManager<SignInManager<Account>>();

            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });


            // Add JWT token authentication
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings.GetValue<string>("SecretKey"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                ValidAudience = jwtSettings.GetValue<string>("Audience"),
            };
            })  
            .AddCookie(options =>
            {
            options.LoginPath = "/Account/Login";
            })
            .AddFacebook(options =>
            {
            options.AppId = "1640601943015214";
            options.AppSecret = "889a423f6aeb18a67c27e42d9d3c6c59";
            // Additional Facebook options...
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    // Add any other requirements you may have for the "user" policy
                });
            });




            // Add repository dependencies
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IWorkoutRepository, WorkoutRepository>();
            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddControllers();

            // Add DbContext
            services.AddDbContext<WorkoutDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));


            // Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

                // Add JWT authentication support in Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                });
            });



        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {



            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
                });
                //app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
        //private async Task CreateRoles(IServiceProvider serviceProvider)
        //{
        //    using (var scope = serviceProvider.CreateScope())
        //    {
        //        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //        // Define the roles you want to create
        //        var roles = new[] { "User", "Trainer", "Admin" };

        //        foreach (var role in roles)
        //        {
        //            // Check if the role does not exist
        //            if (!await roleManager.RoleExistsAsync(role))
        //            {
        //                // Create the role
        //                await roleManager.CreateAsync(new IdentityRole(role));
        //            }
        //        }
        //    }
        //}
    }
}