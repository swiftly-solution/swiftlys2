using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Plugins;

namespace PluginId;

[PluginMetadata(Id = "PluginId", Version = "PluginVersion", Name = "PluginName", Author = "PluginAuthor", Description = "PluginDescription")]
public partial class PluginId : BasePlugin
{
    public PluginId( ISwiftlyCore core ) : base(core)
    {
    }

    public override void ConfigureSharedInterface( IInterfaceManager interfaceManager )
    {
    }

    public override void UseSharedInterface( IInterfaceManager interfaceManager )
    {
    }

    public override void Load( bool hotReload )
    {

    }

    public override void Unload()
    {
    }
}