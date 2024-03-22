using ATS.MVP.Domain.Candidates.Repositories;
using ATS.MVP.Infrastructure.Candidates.Repositories;
using ATS.MVP.Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ATS.MVP.Infrastructure;

public static class Bootstrap
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        //var configKey = nameof(MongoDBConfiguration);

        //IConfigurationSection section = configuration.GetSection(configKey);

        //services.Configure<MongoDBConfiguration>(_ => configuration.GetSection(configKey));

        services.AddScoped<IMongoDBContext, MongoDBContext>();

        services.AddScoped<ICandidateRepository, CandidateRepository>();

        MongoDBContext.ConfigureMappings();

        return services;
    }
}
