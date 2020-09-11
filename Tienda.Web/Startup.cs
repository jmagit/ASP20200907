using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Tienda.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure.Data.UnitOfWork;
using Infrastructure.Data.Core;
using Domain.Entities;
using Infrastructure.Data.Repositories;
using Domain.Services.Core;
using Domain.Services;
using Infrastructure.Data.Contracts.Repositories;
using Domain.Services.Contracts;
using Newtonsoft.Json.Serialization;

namespace Tienda.Web {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<TiendaDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("TiendaConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductDomainService, ProductDomainService>();

            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerDomainService, CustomerDomainService>();

            services.AddControllers().AddNewtonsoftJson(options => {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                //endpoints.MapControllerRoute(
                //    name: "mejor",
                //    pattern: "empleado/{numpage}/{pagesize}",
                //    defaults: new { controller = "Clientes", action = "Index" }
                //    );
                //endpoints.MapControllerRoute(
                //    name: "mejor",
                //    pattern: "empleado/{id}/{action=Index}",
                //    defaults: new { controller = "Clientes" }
                //    );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
