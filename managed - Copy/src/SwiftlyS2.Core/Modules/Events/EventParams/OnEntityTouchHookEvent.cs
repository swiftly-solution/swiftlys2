using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Events;

[Obsolete("OnEntityTouchHookEvent is deprecated. Use OnEntityStartTouchEvent, OnEntityTouchEvent, or OnEntityEndTouchEvent instead.")]
internal class OnEntityTouchHookEvent : IOnEntityTouchHookEvent
{
  public required CBaseEntity Entity { get; init; }

  public required CBaseEntity OtherEntity { get; init; }

  public required EntityTouchType TouchType { get; init; }
}

internal class OnEntityStartTouchEvent : IOnEntityStartTouchEvent
{
  public required CBaseEntity Entity { get; init; }

  public required CBaseEntity OtherEntity { get; init; }
}

internal class OnEntityTouchEvent : IOnEntityTouchEvent
{
  public required CBaseEntity Entity { get; init; }

  public required CBaseEntity OtherEntity { get; init; }
}

internal class OnEntityEndTouchEvent : IOnEntityEndTouchEvent
{
  public required CBaseEntity Entity { get; init; }

  public required CBaseEntity OtherEntity { get; init; }
}