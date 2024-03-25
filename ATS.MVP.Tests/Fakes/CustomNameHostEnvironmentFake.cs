using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace ATS.MVP.Tests.Fakes;

public class CustomNameHostEnvironmentFake : IHostEnvironment
{
    private readonly bool _isProduction;

    public CustomNameHostEnvironmentFake(bool isProduction)
    {
        _isProduction = isProduction;
    }

    public string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IFileProvider ContentRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string ContentRootPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string EnvironmentName { get => _isProduction ? "Production" : "Development"; set => throw new NotImplementedException(); }
}
