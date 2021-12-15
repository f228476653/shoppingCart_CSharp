

using Autofac;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ShoppingCart.AppServices.Services;
using ShoppingCart.Domain.Infrastructure;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Reflection;
using Module = Autofac.Module;

namespace ShoppingCart.Api.Infrastructure
{
    /// <summary>
    /// When using Autofac you typically register the types via modules. This allow you to split the registration types
    /// between multiple files depending on where your types are, just as you could have the application types
    /// distributed across multiple class libraries.
    /// Implements the <see cref="Autofac.Module" />
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    [ExcludeFromCodeCoverage]
    public class ApplicationModule : Module
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationModule"/> class.
        /// </summary>
        /// <param name="queriesConnectionString">The database connection string for queries.</param>
        public ApplicationModule(string queriesConnectionString)
        {
            QueriesConnectionString = queriesConnectionString;
        }

        /// <summary>
        /// Gets the connection string for database queries.
        /// </summary>
        /// <value>The queries connection string.</value>
        public string QueriesConnectionString { get; }

        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>Note that the ContainerBuilder parameter is unique to this module.</remarks>
        protected override void Load(ContainerBuilder builder)
        {
            var externalServiceNames = new Dictionary<string, string>();


            builder.RegisterType<HttpClient>();
            builder.RegisterType<ShoppingCartContext>().As<ShoppingCartContext>().InstancePerDependency();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().InstancePerDependency();
            builder.RegisterType<CartRepository>().As<ICartRepository>().InstancePerDependency();
            builder.RegisterType<ShoppingCartContext>().As<ShoppingCartContext>().InstancePerDependency()
              .OnActivating(e =>
              {
                  var mediator = e.Context.Resolve<IMediator>();
                  e.Instance.SetMediator(mediator);
              });

        }

        public class ShoppingCartDesignTimeContextFactory : IDesignTimeDbContextFactory<ShoppingCartContext>
        {
            public ShoppingCartContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ShoppingCartContext>();
                optionsBuilder.UseSqlServer("Server=localhost;Database=EIIRBb;Trusted_Connection=True;");

                return new ShoppingCartContext(() => optionsBuilder.Options);
            }
        }

    }
}
