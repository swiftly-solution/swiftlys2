using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CBasePlayerWeaponImpl : CBasePlayerWeapon
{
    public CBasePlayerWeaponVData PlayerWeaponVData
    {
        get
        {
            return VData.As<CBasePlayerWeaponVData>();
        }
    }
}