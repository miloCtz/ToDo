using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Serilog;
using ToDo.Application.Behaviors;
using ToDo.Data;

namespace ToDo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddToDoService(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .Scan(
                    selector => selector
                        .FromAssemblies(
                            ToDo.Data.AssemblyReference.Assembly)
                        .AddClasses(false)
                        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(ToDo.Application.AssemblyReference.Assembly));

            serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            serviceCollection.AddValidatorsFromAssembly(
                ToDo.Application.AssemblyReference.Assembly,
                includeInternalTypes: true);

            serviceCollection.AddDbContext<DatabaseContext>(
                (sp, optionsBuilder) =>
                {
                    optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ConnetionString"));
                });

            return serviceCollection;
        }

        public static ConfigureHostBuilder AddLogProvider(this ConfigureHostBuilder host)
        {
            host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            return host;
        }
    }
}
