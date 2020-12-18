using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_autonews.Article;
using asp_autonews.Domain;
using asp_autonews.Domain.Repositories.Abstract;
using asp_autonews.Domain.Repositories.Framework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using DbContext = asp_autonews.Domain.DbContext;

namespace asp_autonews
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
            // из appsettings.json
            Configuration.Bind("Project", new Config());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
            
            // подключаем нужные функции, AddTransient = за один запрос можно создать несколько объектов
            services.AddTransient<IInfoFieldRepository, FInfoFieldRepository>();
            services.AddTransient<IArticleRepository, FArticleRepository>();
            services.AddTransient<Manager>();
            
            // контекст БД
            services.AddDbContext<DbContext>(x =>
                x.UseSqlServer(Config.ConnectionString));
            
            // настройка identity
            services.AddIdentity<IdentityUser, IdentityRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
                option.Password.RequiredLength = 8;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<DbContext>().AddDefaultTokenProviders();
            
            // настройка куки-файлов
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "NewsAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseCookiePolicy();
            
            app.UseAuthentication();
            
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
