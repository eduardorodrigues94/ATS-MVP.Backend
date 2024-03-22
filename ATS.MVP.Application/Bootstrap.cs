using ATS.MVP.Application.Commom.EntryPoint;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ATS.MVP.Application
{

    public static class Bootstrap
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            var assembly = typeof(IApplicationAssemblyReference).Assembly;

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
