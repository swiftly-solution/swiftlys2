using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Plugins;
using Tester.Framework;

namespace Tester;

[PluginMetadata(Id = "sw2.tester", Version = "1.0.0", Name = "Tester", Description = "In-game testing and profiling framework for SwiftlyS2 features.")]
public class TesterPlugin( ISwiftlyCore core ) : BasePlugin(core)
{
    private const string Permission = "tester.run";
    private Runner _runner = null!;

    public override void Load( bool hotReload )
    {
        _runner = new Runner(Core);
    }

    public override void Unload() { }

    [Command("tester", permission: Permission, helpText: "tester list | test [section] | profile [section]")]
    public void TesterCommand( ICommandContext context )
    {
        if (context.Sender is not { IsValid: true } player)
        {
            context.Reply("tester must be run by a connected player");
            return;
        }

        if (!Core.Permission.PlayerHasPermission(player.SteamID, Permission))
        {
            context.Reply($"no permission ({Permission})");
            return;
        }

        var action = context.Args.Length > 0 ? context.Args[0].ToLowerInvariant() : "list";
        var section = context.Args.Length > 1 ? context.Args[1] : null;
        if (section is "all") section = null;

        switch (action)
        {
            case "list":
                context.Reply("sections: " + string.Join(", ", _runner.Discover().Select(s => s.Name)));
                context.Reply("usage: tester list | test [section|all] | profile [section|all]");
                break;
            case "test":
                _ = _runner.Run(player, Mode.Test, section);
                break;
            case "profile":
                _ = _runner.Run(player, Mode.Profile, section);
                break;
            default:
                context.Reply("usage: tester list | test [section|all] | profile [section|all]");
                break;
        }
    }
}
