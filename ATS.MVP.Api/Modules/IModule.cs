namespace ATS.MVP.Api.Modules;

/// <summary>
/// Interface para agrupar módulos (grupos de endpoints) do sistema
/// </summary>
internal interface IModule
{
    /// <summary>
    /// Adiciona grupo de endpoints ao app
    /// </summary>
    /// <param name="app"></param>
    void AddEndpoints(IEndpointRouteBuilder app);
}
