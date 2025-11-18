using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CEntityIdentityImpl {

  public CEntityInstance EntityInstance => new CEntityInstanceImpl(Address.Read<nint>());

  public CHandle<CEntityInstance> EntityHandle => new CHandle<CEntityInstance>(Address.Read<uint>(0x10));

}