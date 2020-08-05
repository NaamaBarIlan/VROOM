using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VROOM.Data;
using VROOM.Models;
using VROOM.Models.Interfaces;
using VROOM.Models.Services;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace VROOM
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                //Make all routes by default autorized to require login:
                options.Filters.Add(new AuthorizeFilter());

            })
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            services.AddDbContext<VROOMDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<VROOMDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWTIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(Configuration["JWTKey"]))

                };
            });

            // Policies:

            services.AddAuthorization(options =>
            {
                options.AddPolicy("GoldLevel", policy => policy.RequireRole(ApplicationRoles.CEO));
                options.AddPolicy("SilverLevel", policy => policy.RequireRole(ApplicationRoles.CEO, ApplicationRoles.OfficeManager));
                options.AddPolicy("BronzeLevel", policy => policy.RequireRole(ApplicationRoles.CEO, ApplicationRoles.OfficeManager, ApplicationRoles.Employee));
            });


            // MAPPING - register my Dependency Injection Services
            services.AddTransient<IEmployee, EmployeeRepository>();
            services.AddTransient<IEquipmentItem, EquipmentItemRepository>();
            services.AddTransient<IEmployeeEquipmentItem, EmployeeEquipmentItemRepository>();
            services.AddTransient<IEmailSender, EmailSenderService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // VERY IMPORTANT
            app.UseStaticFiles();

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleInitializer.SeedData(serviceProvider, userManager, Configuration);

            app.UseEndpoints(endpoints =>
            {
                // Set our default routing for our request within the API application
                // By default, our convention is {sit}/[controller]/[action]/[id]
                // id is not required, allowing it to be null-able
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
