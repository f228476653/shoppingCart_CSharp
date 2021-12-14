using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ShoppingCart.Api.Infrastructure;
using ShoppingCart.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services) {
            services.AddAutoMapper(new[] { typeof(ShoppingCartProfile) });
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShoppingCart.Api", Version = "v1" });
            });
            // Autofac. Use AddTransient if not using Autofac
            var builder = new ContainerBuilder();
            builder.Populate(services);
            ConfigureContainer(builder);

            var serviceProvider = new AutofacServiceProvider(builder.Build());
            // serviceProvider.EnsureDbCreated(); // Note: This should only ever be used locally
            return serviceProvider;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        /*public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShoppingCart.Api", Version = "v1" });
            });
        }*/

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingCart.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public virtual DbContextOptions<T> GetDbContextOptions<T>(string connectionString) where T : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<T>();
            
            
            // optionsBuilder.EnableSensitiveDataLogging(true); // Only use locally 
            optionsBuilder.UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            

            return optionsBuilder.Options;
        }

        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
            //configure auto fac here
            builder.RegisterModule(new ApplicationModule("Server=localhost;Database=ShoppingCart;Trusted_Connection=True;"));
            ConfigureDbContextOptions(builder, "Server=localhost;Database=ShoppingCart;Trusted_Connection=True;");

            builder.RegisterModule(new MediatorModule());

        }
        public DbContextOptions<ShoppingCartContext> CreateShoppingCartDbContextOptions()
        {
            return GetDbContextOptions<ShoppingCartContext>("Server=localhost;Database=ShoppingCart;Trusted_Connection=True;");
        }

        public void ConfigureDbContextOptions(ContainerBuilder builder, string connectionString)
        {
            // Note: AutoFac automatically fulfills this Func<> mapping, which we don't want. So we don't use IfNotRegistered
            builder.RegisterInstance<Func<DbContextOptions<ShoppingCartContext>>>(CreateShoppingCartDbContextOptions);
        }
    }
}
