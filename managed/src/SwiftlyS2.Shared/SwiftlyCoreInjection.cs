using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SwiftlyS2.Shared;

public static class SwiftlyCoreInjection
{
    /// <summary>
    /// Binds configuration to options WITHOUT merging collections/dictionaries.
    /// This shadows Microsoft's BindConfiguration to fix the default merge behavior.
    /// </summary>
    public static OptionsBuilder<T> BindConfiguration<T>(
        this OptionsBuilder<T> builder,
        string configSectionPath) where T : class
    {
        builder.Services.AddSingleton<IConfigureOptions<T>>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            return new ConfigureNamedOptions<T>(builder.Name, options =>
            {
                var section = config.GetSection(configSectionPath);
                var loaded = section.Get<T>();

                if (loaded == null)
                    return;

                foreach (var prop in typeof(T).GetProperties())
                {
                    if (!prop.CanRead || !prop.CanWrite)
                        continue;

                    var value = prop.GetValue(loaded);
                    if (value != null)
                        prop.SetValue(options, value);
                }
            });
        });

        return builder;
    }

    public static IServiceCollection AddSwiftly( this IServiceCollection self, ISwiftlyCore core, bool addLogger = true, bool addConfiguration = true )
    {
        _ = self
            .AddSingleton(core)
            .AddSingleton(core.ConVar)
            .AddSingleton(core.Command)
            .AddSingleton(core.Database)
            .AddSingleton(core.Engine)
            .AddSingleton(core.EntitySystem)
            .AddSingleton(core.Event)
            .AddSingleton(core.GameData)
            .AddSingleton(core.GameEvent)
            .AddSingleton(core.Localizer)
            .AddSingleton(core.Memory)
            .AddSingleton(core.NetMessage)
            .AddSingleton(core.Permission)
            .AddSingleton(core.PlayerManager)
            .AddSingleton(core.Profiler)
            .AddSingleton(core.Scheduler)
            .AddSingleton(core.Trace)
            .AddSingleton(core.MenusAPI)
            .AddSingleton(core.CommandLine)
            .AddSingleton(core.GameFileSystem)
            .AddSingleton(core.Translation)
            .AddSingleton(core.PluginManager);

        if (addLogger)
        {
            _ = self
                .AddSingleton(core.LoggerFactory)
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>));
        }

        if (addConfiguration && core.Configuration.BasePathExists)
        {
            _ = self
                .AddSingleton(core.Configuration)
                .AddSingleton(core.Configuration.Manager)
                .AddSingleton<IConfiguration>(provider => provider.GetRequiredService<IConfigurationManager>());
        }

        return self;
    }
}