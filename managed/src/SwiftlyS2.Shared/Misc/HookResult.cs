namespace SwiftlyS2.Shared.Misc;

/// <summary>
/// Result of a hook.
/// </summary>
public enum HookResult : uint
{

    /// <summary>
    /// The executions of following hooks and original function will continue.
    /// 
    /// </summary>
    Continue = 0,

    /// <summary>
    /// The executions of following hooks and original function will all be cancelled.
    /// 
    /// Won't work for post hooks.
    /// </summary>
    Stop = 1,

    /// <summary>
    /// The executions of following hooks will be cancelled, but the original function will continue.
    ///
    /// Won't work for post hooks.
    /// </summary>
    Handled = 2,

    /// <summary>
    /// The executions of following hooks will continue and original function will be cancelled.
    /// </summary>
    CancelOriginal = 3,
}