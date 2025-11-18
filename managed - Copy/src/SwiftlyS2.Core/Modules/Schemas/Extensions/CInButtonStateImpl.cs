using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CInButtonStateImpl : CInButtonState
{
  public GameButtonFlags ButtonPressed
  {
    get => (GameButtonFlags)ButtonStates[0];
    set => ButtonStates[0] = (ulong)value;
  }

  public GameButtonFlags ButtonChanged
  {
    get => (GameButtonFlags)ButtonStates[1];
    set => ButtonStates[1] = (ulong)value;
  }

  public GameButtonFlags ButtonScroll
  {
    get => (GameButtonFlags)ButtonStates[2];
    set => ButtonStates[2] = (ulong)value;
  }

}