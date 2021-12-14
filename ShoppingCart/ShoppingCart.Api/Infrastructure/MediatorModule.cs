using Autofac;
using MediatR;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
namespace ShoppingCart.Api.Infrastructure
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            /*builder.RegisterAssemblyTypes(typeof(EiirSubmittedEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));*/
            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events


            // Register the Requests' Validators (Validators based on FluentValidation library)



            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => componentContext.TryResolve(t, out var o) ? o : null;
            });




        }
    }
}
