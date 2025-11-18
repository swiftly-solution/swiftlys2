using SwiftlyS2.Shared.Events;

namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CInButtonState
{
  public GameButtonFlags ButtonPressed { get; set; }

  public GameButtonFlags ButtonChanged { get; set; }

  public GameButtonFlags ButtonScroll { get; set; }
  
}