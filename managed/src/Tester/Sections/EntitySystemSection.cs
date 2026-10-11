using SwiftlyS2.Shared;
using SwiftlyS2.Shared.EntitySystem;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class EntitySystemSection( ISwiftlyCore core ) : Section(core)
{
    private const string DesignerName = "info_target";

    public override string Name => "entitysystem";

    private IEntitySystemService Es => Core.EntitySystem;

    private CInfoTarget Spawn()
    {
        var e = Es.CreateEntity<CInfoTarget>();
        e.DispatchSpawn();
        return e;
    }

    public override Task Test( TestContext t )
    {
        var es = Es;

        t.Test("CreateEntity<T> creates a valid, spawned entity with the right designer name", () =>
        {
            var e = Spawn();
            try
            {
                Expect(e.IsValid, "not valid after creation");
                Equal(DesignerName, e.DesignerName, "DesignerName");
                Expect(e.Index > 0, $"Index={e.Index}");
            }
            finally { e.Despawn(); }
        });

        t.Test("CreateEntity<T>(forcedIndex) honors the requested index", () =>
        {
            var e = Es.CreateEntity<CInfoTarget>();
            e.Despawn();
            var free = (int)e.Index;
            CInfoTarget forced;
            try { forced = Es.CreateEntity<CInfoTarget>(free); }
            catch (ArgumentException) { Skip("freed index was not reclaimed in time (deferred removal)"); return; }
            try
            {
                Equal((uint)free, forced.Index, "Index");
            }
            finally { forced.Despawn(); }
        });

        t.Test("CreateEntityByDesignerName<T> matches CreateEntity<T>", () =>
        {
            var e = Es.CreateEntityByDesignerName<CInfoTarget>(DesignerName);
            try
            {
                Expect(e.IsValid, "not valid");
                Equal(DesignerName, e.DesignerName, "DesignerName");
            }
            finally { e.Despawn(); }
        });

        t.Test("CreateEntityByDesignerName with an invalid name throws", () =>
            Throws<Exception>(() => Es.CreateEntityByDesignerName<CInfoTarget>("tester_no_such_designer_name")));

        t.Test("GetEntityByIndex<T> and GetEntityByIndex resolve the same entity created", () =>
        {
            var e = Spawn();
            try
            {
                Equal(e, Es.GetEntityByIndex<CInfoTarget>(e.Index), "typed");
                Equal(e, Es.GetEntityByIndex(e.Index)!, "untyped");
                Expect(Es.GetEntityByIndex<CInfoTarget>(999999) is null, "bogus index typed");
                Expect(Es.GetEntityByIndex(999999) is null, "bogus index untyped");
            }
            finally { e.Despawn(); }
        });

        t.Test("GetEntityByAddress<T> and GetEntityByAddress resolve the same entity created", () =>
        {
            var e = Spawn();
            try
            {
                Equal(e, Es.GetEntityByAddress<CInfoTarget>(e.Address), "typed");
                Equal(e, Es.GetEntityByAddress(e.Address)!, "untyped");
                Expect(Es.GetEntityByAddress<CInfoTarget>(0) is null, "null address typed");
                Expect(Es.GetEntityByAddress(0) is null, "null address untyped");
            }
            finally { e.Despawn(); }
        });

        t.Test("GetRefEHandle tracks the entity and invalidates after Despawn", () =>
        {
            var e = Spawn();
            var handle = Es.GetRefEHandle(e);
            Expect(handle.IsValid, "handle invalid right after creation");
            Equal(e, handle.Value, "handle.Value");
            e.Despawn();
            Expect(!handle.IsValid, "handle still valid after Despawn");
        });

        t.Test("GetAllEntities / GetAllEntitiesByClass / GetAllEntitiesByDesignerName all see a created entity", () =>
        {
            var e = Spawn();
            try
            {
                Expect(es.GetAllEntities().Contains(e), "GetAllEntities");
                Expect(es.GetAllEntitiesByClass<CInfoTarget>().Contains(e), "GetAllEntitiesByClass");
                Expect(es.GetAllEntitiesByDesignerName<CInfoTarget>(DesignerName).Contains(e), "GetAllEntitiesByDesignerName");
            }
            finally { e.Despawn(); }
        });

        t.Test("GetGameRules returns a valid entity when a map is loaded", () =>
        {
            var rules = es.GetGameRules();
            if (rules is not { IsValid: true }) Skip("no game rules entity (map not loaded?)");
        });

        t.Test("HookEntityOutput / HookEntityInput register and unhook cleanly", () =>
        {
            var outGuid = es.HookEntityOutput<CInfoTarget>("*", _ => { });
            var inGuid = es.HookEntityInput<CInfoTarget>("*", _ => { });
            Expect(es.UnhookEntityOutput(outGuid), "output unhook");
            Expect(es.UnhookEntityInput(inGuid), "input unhook");
            Expect(!es.UnhookEntityOutput(outGuid), "double output unhook");
            Expect(!es.UnhookEntityInput(inGuid), "double input unhook");
            Expect(!es.UnhookEntityOutput(Guid.NewGuid()), "unknown output guid");
        });

        t.Test("Ref-field schema properties (Health/MaxHealth) round-trip", () =>
        {
            var e = Spawn();
            try
            {
                e.Health = 55;
                e.MaxHealth = 100;
                Equal(55, e.Health, "Health");
                Equal(100, e.MaxHealth, "MaxHealth");
            }
            finally { e.Despawn(); }
        });

        t.Test("String schema properties (Target) round-trip", () =>
        {
            var e = Spawn();
            try
            {
                e.Target = "tester_target_name";
                Equal("tester_target_name", e.Target, "Target");
            }
            finally { e.Despawn(); }
        });

        t.Test("Team property reads/writes through the underlying TeamNum byte", () =>
        {
            var e = Spawn();
            try
            {
                e.Team = Team.CT;
                Equal(Team.CT, e.Team, "Team");
                Equal((byte)Team.CT, e.TeamNum, "TeamNum");
            }
            finally { e.Despawn(); }
        });

        t.Test("Teleport moves the entity and AbsOrigin reflects it", () =>
        {
            var e = Spawn();
            try
            {
                if (e.AbsOrigin is null) Skip("entity has no scene node / AbsOrigin on this build");
                var target = new Vector(123, 456, 789);
                e.Teleport(target, QAngle.Zero, Vector.Zero);
                var pos = e.AbsOrigin!.Value;
                Expect(MathF.Abs(pos.X - target.X) < 0.5f && MathF.Abs(pos.Y - target.Y) < 0.5f && MathF.Abs(pos.Z - target.Z) < 0.5f, $"AbsOrigin {pos}");
            }
            finally { e.Despawn(); }
        });

        return Task.CompletedTask;
    }
}
