using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CEntityIdentity {
  public CEntityInstance EntityInstance { get; }

  public CHandle<CEntityInstance> EntityHandle { get; }
}