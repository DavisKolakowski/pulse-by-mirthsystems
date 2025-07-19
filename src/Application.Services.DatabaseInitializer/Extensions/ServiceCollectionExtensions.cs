using Application.Services.DatabaseInitializer.Options;
using Microsoft.Extensions.Options;

namespace Application.Services.DatabaseInitializer.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureDataSetup(
            this IServiceCollection services,
            Action<OptionsBuilder<DataOptions>> configureOptions)
    {
        var optionsBuilder = services.AddOptions<DataOptions>();
        configureOptions(optionsBuilder);
        return services;
    }
}
