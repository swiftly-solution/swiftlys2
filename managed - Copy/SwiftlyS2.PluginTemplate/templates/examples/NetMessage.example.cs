using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace PluginId;


/// This is an example that shows various net message API.
public partial class PluginId {

  public void InitializeNetMessage() {

    /*
      This is an example that shows how to send a net message.
      The msg will be destroyed immediately after being sent.
    */
    Core.NetMessage.Send<CUserMessageShake>(msg => {
      // Setting fields of the net message.
      msg.Amplitude = 10;
      msg.Duration = 10;

      // Control the recipients of the net message.
      msg.Recipients.AddAllPlayers();
    });


    /*
      You can also create a persistent net message and send it.
      As long as the message is not recycled by GC, you can send it as many times as you want.
    */
    using var message = Core.NetMessage.Create<CUserMessageShake>();
    message.Amplitude = 10;
    message.Duration = 10;
    message.Recipients.AddAllPlayers();
    message.SendToAllPlayers();
  }

  /// The ServerNetMessageHandler attribute is used to mark a method as a server-side net message handler.
  /// the attribute only works on main class that inherits BasePlugin.
  /// 
  /// The method must take a single parameter that is the net message type, and return a HookResult.
  /// 
  [ServerNetMessageHandler]
  public HookResult TestServerNetMessageHandler(CMsgSosStartSoundEvent msg) {

    // You can also set fields of the net message.
    msg.SoundeventHash = 0;

    // You can also control the recipients of the net message.
    msg.Recipients.RemoveAllPlayers();

    return HookResult.Continue;
  }

}
