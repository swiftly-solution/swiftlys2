using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CBaseEntityImpl : CBaseEntity
{
    public CEntitySubclassVDataBase VData {
        get {
            return new CEntitySubclassVDataBaseImpl(NativeSchema.GetVData(_Handle));
        }
    }

    public Vector? AbsOrigin => CBodyComponent?.SceneNode?.AbsOrigin;
    public QAngle? AbsRotation => CBodyComponent?.SceneNode?.AbsRotation;

    public void Teleport( Vector? position, QAngle? angle, Vector? velocity )
    {
        unsafe
        {
            Vector* pos = null, vel = null;
            QAngle* ang = null;

            if (position.HasValue)
            {
                var v = position.Value;
                pos = &v;
            }

            if (angle.HasValue)
            {
                var a = angle.Value;
                ang = &a;
            }

            if (velocity.HasValue)
            {
                var ve = velocity.Value;
                vel = &ve;
            }

            GameFunctions.Teleport(Address, pos, ang, vel);
        }
    }

    public void CollisionRulesChanged()
    {
        GameFunctions.CBaseEntity_CollisionRulesChanged(Address);
    }
}