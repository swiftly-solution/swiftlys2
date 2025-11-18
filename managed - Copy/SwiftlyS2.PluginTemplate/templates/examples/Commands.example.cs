using SwiftlyS2.Shared.Commands;

namespace PluginId;

/// <summary>
/// This is an example that shows how to use commands.
/// </summary>
public partial class PluginId
{
  public void InitializeCommands()
  {
    // Register a command.
    Core.Command.RegisterCommand("test2", (context) => {
      Console.WriteLine("Test command");
    });
  }

  [Command("test")] // this will be `sw_test` in the console.
  // [Command("test", registerRaw: true)] // this will be `test` in the console, without the sw_ prefix.
  public void TestCommand(ICommandContext context)
  {
    context.Reply("Hello World");
  }
}