namespace SwiftlyS2.Shared.Plugins;

public enum PluginReloadMethod
{
    /// <summary>
    /// Default. When dll file update it will automatically reload. Or you can use the reload command.
    /// </summary>
    Auto = 0,

    /// <summary>
    /// When dll file updated, it won't reload at once until a new map start. Or you can use the reload command whenever you want.
    /// </summary>
    OnMapChange = 1,

    /// <summary>
    /// You can only use the reload command. It won't reload on its own.
    /// </summary>
    OnlyByCommand = 2


}
