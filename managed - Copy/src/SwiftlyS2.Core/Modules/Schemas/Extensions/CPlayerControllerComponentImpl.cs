using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CPlayerControllerComponentImpl : CPlayerControllerComponent
{
    public CBasePlayerController Controller => __m_pChainEntity.Entity.As<CBasePlayerController>();
}