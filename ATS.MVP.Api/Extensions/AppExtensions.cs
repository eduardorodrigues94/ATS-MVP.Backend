using ATS.MVP.Api.Modules;
using System.Reflection;

namespace ATS.MVP.Api.Extensions;

/// <summary>
/// Métodos de extensões da aplicação
/// </summary>
internal static class AppExtensions
{
    /// <summary>
    /// Método de extensão para adicionar endpoints via reflection
    /// </summary>
    /// <param name="app"></param> 
    /// Segui o padrão utilizando app para ser familiar a inicialização
    /// Numa aplicação real ao invés de usar reflections provavelmente usaríamos alguns pacotes
    /// Para adicionar autenticação, ratelimiting, telemetria, etc para maior flexibilidade, 
    /// controle e possibilidade de extensão como o carter <a>https://github.com/CarterCommunity/Carter</a>, 
    /// mas decidi não colocar aqui pois seria 'overkill' para a simplicidade da aplicação
    public static void AddATSMVPEndpoints(this IEndpointRouteBuilder app)
    {
        var moduleTypes = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsInterface);

        foreach (var moduleType in moduleTypes)
        {
            var moduleInstance = Activator.CreateInstance(moduleType) as IModule;

            moduleInstance?.AddEndpoints(app);
        }
    }
}
