using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Shared.GameEvents;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class GameEventHandler : Attribute {

  public HookMode HookMode { get; set; }

  public GameEventHandler(HookMode hookMode) {
    HookMode = hookMode;
  }

}



