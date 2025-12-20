using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace TestPlugin;

public class TestService
{
    private ISwiftlyCore Core { get; init; }

    public TestService( ISwiftlyCore core, ILogger<TestService> logger, IOptionsMonitor<PluginConfig> config )
    {
        Core = core;
        logger.LogInformation("TestService created");
        logger.LogInformation("Config: {Config}", config.CurrentValue.DatabasePurgeDays);
        core.Registrator.Register(this);
    }

    [Command("test")]
    public void TestCommand( ICommandContext context )
    {
        Core.NetMessage.Send<CUserMessageShake>(um =>
        {
            um.Frequency = 1f;
            um.Recipients.AddAllPlayers();
        });
        context.Reply("Test command");
    }
}