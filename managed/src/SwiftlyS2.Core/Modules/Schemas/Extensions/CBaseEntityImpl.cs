using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CBaseEntityImpl : CBaseEntity
{
    public CEntitySubclassVDataBase VData {
        get { return new CEntitySubclassVDataBaseImpl(NativeSchema.GetVData(_Handle)); }
    }

    public Vector? AbsOrigin {
        get {
            if (CBodyComponent == null) return null;
            if (!CBodyComponent.IsValid) return null;
            if (CBodyComponent.SceneNode == null) return null;
            if (!CBodyComponent.SceneNode.IsValid) return null;

            return CBodyComponent?.SceneNode?.AbsOrigin;
        }
    }

    public QAngle? AbsRotation {
        get {
            if (CBodyComponent == null) return null;
            if (!CBodyComponent.IsValid) return null;
            if (CBodyComponent.SceneNode == null) return null;
            if (!CBodyComponent.SceneNode.IsValid) return null;

            return CBodyComponent?.SceneNode?.AbsRotation;
        }
    }

    public Team Team {
        get => (Team)TeamNum;
        set => TeamNum = (byte)value;
    }

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

    public void TakeDamage( CTakeDamageInfo dmgInfo )
    {
        unsafe
        {
            GameFunctions.TakeDamage(Address, &dmgInfo);
        }
    }

    public void TakeDamage( CBaseEntity inflictor, CBaseEntity attacker, CBaseEntity ability, float flDamage, DamageTypes_t bitsDamageType )
    {
        var info = new CTakeDamageInfo(inflictor, attacker, ability, flDamage, bitsDamageType);
        TakeDamage(info);
    }

    public void CollisionRulesChanged()
    {
        GameFunctions.CBaseEntity_CollisionRulesChanged(Address);
    }
}