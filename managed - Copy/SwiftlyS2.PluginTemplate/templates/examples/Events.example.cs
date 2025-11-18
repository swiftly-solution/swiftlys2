using SwiftlyS2.Shared.Misc;

namespace PluginId;

/// <summary>
/// This is an example that shows how to use events.
/// </summary>
public partial class PluginId
{
  public void InitializeEvents()
  {
    // Register an event on tick.
    Core.Event.OnTick += () => {
      Console.WriteLine("Tick");
    };

    Core.Event.OnEntityCreated += (@event) => {
      Console.WriteLine("Entity created");
      Console.WriteLine(@event.Entity.DesignerName);
    };

    Core.Event.OnClientConnected += (@event) => {
      Console.WriteLine("Client connected");
      // prevent a join.
      @event.Result = HookResult.Stop;
    };

    Core.Event.OnPrecacheResource += (@event) => {
      // Add your resource here.
      @event.AddItem("characters/test.vmdl");
    };

  }
}