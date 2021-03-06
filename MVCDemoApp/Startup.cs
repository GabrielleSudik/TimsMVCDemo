using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MVCDemoApp
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

            //add this one:
            //ConnectionStringData is from our Common / DataLibrary project.
            services.AddSingleton(new ConnectionStringData
            {
                SqlConnectionName = "Default" //remember you named/ID'd this somewhere.
            });

            //add this too:
            services.AddSingleton<IDataAccess, SqlDb>(); //also from our DataLibrary project.

            //and this:
            services.AddSingleton<IFoodData, FoodData>();
            services.AddSingleton<IOrderData, OrderData>();
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

            app.UseRouting(); //this starts use of routing

            app.UseAuthorization();

            app.UseEndpoints(endpoints => //this sets up endpoints for routing
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"); //controller, action, optional id
                           //when no other route is written, it will default to Home/Index
                           //you can change this to whatever you want. Like Orders/Create or Food/List.
                           //you can also add more to the path, like "something/{controller- ..."                    
            });
        }
    }
}
