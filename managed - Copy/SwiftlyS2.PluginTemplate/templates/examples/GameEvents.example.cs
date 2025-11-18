using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.GameEvents;

namespace PluginId;


/// This is an example that shows various gameevent API.
public partial class PluginId
{

  public void InitializeGameEvents()
  {
    /// Hook a game event.
    /// The method must take a single parameter that is the game event type, and return a HookResult.

    Core.GameEvent.HookPre<EventPlayerJump>((@event) => {
      Console.WriteLine($"Player {@event.UserIdController.PlayerName} jumped");
      return HookResult.Continue;
    });

    /// Fire a game event to all players.
    /// You can configure the event inside the action.
    /// The event will be destroyed immediately after being fired.
    /// 
    /// To fire to a specific client, also check Core.GameEvent.FireToPlayer
    Core.GameEvent.Fire<EventShowSurvivalRespawnStatus>(@event => {
      @event.LocToken = "Hello World";
    });
  }

  [GameEventHandler(HookMode.Pre)]
  public HookResult TestServerNetMessageHandler(EventPlayerJump @event)
  {
    /// You can also hook the event by using the attribute.
    /// The attribute only works on main class that inherits BasePlugin.

    @event.DontBroadcast = true;

    return HookResult.Continue;
  }

}
