using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities;

public interface ICoreModule
{
    void Load(IServiceCollection collection);
}