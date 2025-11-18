using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CEntityIdentityImpl
{

    public CEntityInstance EntityInstance => new CEntityInstanceImpl(Address.Read<nint>());

    public CHandle<CEntityInstance> EntityHandle => new(Address.Read<uint>(0x10));

}