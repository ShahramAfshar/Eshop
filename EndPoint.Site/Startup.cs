using EStore.Application.Interfaces.Contexts;
using EStore.Application.Interfaces.FacadPatern;
using EStore.Application.Services.Products.FacadPatern;
using EStore.Application.Services.Users.Commands.EditUser;
using EStore.Application.Services.Users.Commands.LoginUser;
using EStore.Application.Services.Users.Commands.RegisterUser;
using EStore.Application.Services.Users.Commands.RemoveUser;
using EStore.Application.Services.Users.Commands.UserStatusChange;
using EStore.Application.Services.Users.Queries.GetRoles;
using EStore.Application.Services.Users.Queries.GetUsers;
using EStore.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site
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
            //برای انجام Authentication بر اساس کوکی
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = new PathString("/");
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
            });

            services.AddScoped<IDataBaseContext, DataBaseContext>();
            services.AddScoped<IGetUsersService, GetUsersService>();
            services.AddScoped<IRegisterUserService, RegisterUserService>();
            services.AddScoped<IGetRolesService, GetRolesService>();
            services.AddScoped<IRemoveUserService, RemoveUserService>();
            services.AddScoped<IUserStatusChangeService,UserStatusChangeService>();
            services.AddScoped<IEditUserService, EditUserService>();
            services.AddScoped<IRegisterUserService, RegisterUserService>();
            services.AddScoped<ILoginUserService, LoginUserService>();

            //FACAD Injection            
            services.AddScoped<IProductFasad, ProductFasad>();



            string connectionString = @"Data Source=SHAHRAM-PC\SQLEXPRESS ; Initial Catalog= bugetoShop; Integrated Security=True";
            services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(options=>options.UseSqlServer(connectionString));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
            });
        }
    }
}
