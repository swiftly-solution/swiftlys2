using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Shared.GameHooks;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class GameHookHandler : Attribute
{
    public HookMode HookMode { get; set; }

    public GameHookHandler( HookMode hookMode )
    {
        HookMode = hookMode;
    }

}
