using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Shared.Plugins;

public abstract class BasePlugin : IPlugin
{
    protected ISwiftlyCore Core { get; private init; }

    public BasePlugin( ISwiftlyCore core )
    {
        Core = core;
        Console.SetOut(new ConsoleRedirector());
        Console.SetError(new ConsoleRedirector());
    }

    public virtual void ConfigureSharedInterface( IInterfaceManager interfaceManager ) { }

    public virtual void UseSharedInterface( IInterfaceManager interfaceManager ) { }

    public virtual void OnSharedInterfaceInjected( IInterfaceManager interfaceManager ) { }

    public virtual void OnAllPluginsLoaded() { }

    public abstract void Load( bool hotReload );

    public abstract void Unload();

    /// <summary>
    /// You can choose when the plugin is allowed to reload.
    /// </summary>
    public virtual PluginReloadMethod ReloadMethod { get; set; } = PluginReloadMethod.Auto;
}