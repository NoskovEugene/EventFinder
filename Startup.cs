using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EventFinder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using EventFinder.Models.Repositories;

namespace EventFinder
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
            services.AddControllersWithViews();
            services.AddOptions();
            services.AddMvcCore(options=> options.EnableEndpointRouting= false).AddAuthorization();

            services.AddTransient<DbContext,EventFinderContext>();
            services.AddTransient(typeof(IRepositoryBase<>),typeof(RepositoryBase<>));
            
            string connectionstring = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<EventFinderContext>
            (
                options=>{
                    options.UseLazyLoadingProxies().UseNpgsql(
                        Configuration.GetConnectionString("DefaultConnection"),
                        x=>x.MigrationsHistoryTable("__EFMigrationsHistory","migr").CommandTimeout(600)
                    );
                }
            );

            services.AddControllers();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(Options =>{
                Options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
            });

            
            //check database
            /*var db = services.BuildServiceProvider().GetService<DbContext>();
            db.Database.Migrate();*/

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 
            });

            app.UseAuthentication();

            app.UseMvc(routes=>{
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });

        }
    }
}
