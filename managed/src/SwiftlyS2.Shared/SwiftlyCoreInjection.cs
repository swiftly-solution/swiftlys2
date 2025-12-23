using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SwiftlyS2.Shared;

/// <summary>
/// Custom options factory that prevents Microsoft's default merge behavior.
/// Config file values completely replace code defaults instead of merging.
/// </summary>
public class SwiftlyOptionsFactory<T> : IOptionsFactory<T> where T : class, new()
{
    private readonly IEnumerable<IConfigureOptions<T>> _setups;
    private readonly IEnumerable<IPostConfigureOptions<T>> _postConfigures;
    private readonly IEnumerable<IValidateOptions<T>> _validations;

    public SwiftlyOptionsFactory(
        IEnumerable<IConfigureOptions<T>> setups,
        IEnumerable<IPostConfigureOptions<T>> postConfigures,
        IEnumerable<IValidateOptions<T>> validations)
    {
        _setups = setups;
        _postConfigures = postConfigures;
        _validations = validations;
    }

    public T Create(string name)
    {
        var options = new T();
        var boundConfig = TryGetBoundConfiguration();

        if (boundConfig != null)
        {
            // Clear default collections before binding - prevents merge behavior
            ClearCollectionDefaults(options, boundConfig);
            boundConfig.Bind(options);
        }
        else
        {
            // Fallback: run all configure actions (original behavior)
            foreach (var setup in _setups)
            {
                if (setup is IConfigureNamedOptions<T> namedSetup)
                    namedSetup.Configure(name, options);
                else
                    setup.Configure(options);
            }
        }

        foreach (var post in _postConfigures)
            post.PostConfigure(name, options);

        foreach (var validate in _validations)
        {
            var result = validate.Validate(name, options);
            if (result is { Failed: true, Failures: not null })
                throw new OptionsValidationException(name, typeof(T), result.Failures);
        }

        return options;
    }

    private IConfiguration? TryGetBoundConfiguration()
    {
        foreach (var setup in _setups)
        {
            var setupType = setup.GetType();
            if (!setupType.IsGenericType) continue;

            var genericArgs = setupType.GetGenericArguments();
            if (genericArgs.Length != 2 || !typeof(IConfiguration).IsAssignableFrom(genericArgs[1]))
                continue;

            var action = setupType.GetProperty("Action")?.GetValue(setup) as Delegate;

            if (setupType.GetProperty("Dependency")?.GetValue(setup) is not IConfiguration rootConfig || action?.Target == null) continue;

            var sectionPath = ExtractSectionPath(action.Target);
            if (!string.IsNullOrEmpty(sectionPath))
                return rootConfig.GetSection(sectionPath);
        }

        return null;
    }

    private static string? ExtractSectionPath(object closure)
    {
        foreach (var field in closure.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            if (field.FieldType != typeof(string)) continue;
            var value = field.GetValue(closure) as string;
            if (!string.IsNullOrEmpty(value) && !value.Contains('\\') && !value.Contains('/'))
                return value;
        }

        return null;
    }

    /// <summary>
    /// Clears all collections before Bind() to prevent merge behavior.
    /// </summary>
    private static void ClearCollectionDefaults(object instance, IConfiguration config)
    {
        ClearCollectionsRecursive(instance, config);
    }

    private static void ClearCollectionsRecursive(object current, IConfiguration config)
    {
        foreach (var prop in current.GetType().GetProperties())
        {
            if (!prop.CanRead || !prop.CanWrite) continue;

            var currentValue = prop.GetValue(current);
            if (currentValue == null) continue;

            var section = config.GetSection(prop.Name);
            var hasConfigValue = section.GetChildren().Any() || section.Value != null;

            if (currentValue is System.Collections.IDictionary dict)
            {
                // If config specifies this dictionary, clear it completely
                if (hasConfigValue)
                    dict.Clear();
            }
            else if (currentValue is System.Collections.IList list)
            {
                // If config specifies this list, clear it completely
                if (hasConfigValue)
                    list.Clear();
            }
            else if (!IsSimpleType(prop.PropertyType) && prop.PropertyType.IsClass)
            {
                // Recurse into nested objects
                ClearCollectionsRecursive(currentValue, section);
            }
        }
    }

    private static bool IsSimpleType(Type type) =>
        type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal) ||
        type == typeof(DateTime) || type == typeof(DateTimeOffset) || type == typeof(TimeSpan) ||
        type == typeof(Guid) || Nullable.GetUnderlyingType(type) != null;
}

public static class SwiftlyCoreInjection
{
    public static IServiceCollection AddSwiftly(this IServiceCollection self, ISwiftlyCore core, bool addLogger = true, bool addConfiguration = true)
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

        // Replace default options factory to prevent merge behavior
        _ = self.AddSingleton(typeof(IOptionsFactory<>), typeof(SwiftlyOptionsFactory<>));

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
