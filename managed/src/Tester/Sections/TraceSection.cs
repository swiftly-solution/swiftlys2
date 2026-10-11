using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.Services;
using SwiftlyS2.Shared.Trace;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class TraceSection( ISwiftlyCore core ) : Section(core)
{
    public override string Name => "trace";

    private ITraceManager Tr => Core.Trace;

    private static Vector CallerOrigin( TestContext t )
    {
        var pawn = t.Caller.Pawn;
        if (pawn is not { IsValid: true }) Skip("caller has no valid pawn");
        var origin = pawn!.AbsOrigin;
        if (origin is null) Skip("caller pawn has no AbsOrigin");
        return origin!.Value;
    }

    public override Task Test( TestContext t )
    {
        t.Test("TraceShapeLine from the caller's eyes never throws and reports a sane fraction", () =>
        {
            var origin = CallerOrigin(t);
            var end = new Vector(origin.X, origin.Y, origin.Z - 8192f);
            var result = Tr.TraceShapeLine(origin, end);
            Expect(result.Fraction >= 0f, $"Fraction={result.Fraction}");
        });

        t.Test("TraceShapeAngle with a zero direction stays at the start", () =>
        {
            var origin = CallerOrigin(t);
            var result = Tr.TraceShapeAngle(origin, QAngle.Zero, 0f);
            Expect(result.Distance <= 0.01f, $"Distance={result.Distance}");
        });

        t.Test("TracePlayerBBox from a point to itself does not hit anything new", () =>
        {
            var origin = CallerOrigin(t);
            var bounds = new BBox_t { Mins = new Vector(-16, -16, 0), Maxs = new Vector(16, 16, 72) };
            var result = Tr.TracePlayerBBox(origin, origin, bounds);
            Expect(result.Fraction is >= 0f and <= 1.0001f, $"Fraction={result.Fraction}");
        });

        t.Test("TraceShapeLine honors EntitiesToIgnore (ignored entity is never reported as the hit)", () =>
        {
            var e = Core.EntitySystem.CreateEntity<CInfoTarget>();
            e.DispatchSpawn();
            try
            {
                var origin = e.AbsOrigin;
                if (origin is null) Skip("entity has no AbsOrigin on this build");
                var start = origin!.Value;
                var end = new Vector(start.X + 64, start.Y, start.Z);
                var p = new TraceParams { EntitiesToIgnore = [e] };
                var result = Tr.TraceShapeLine(start, end, p);
                Expect(result.Entity is null || !ReferenceEquals(result.Entity, e), "ignored entity was reported as the hit");
            }
            finally { e.Despawn(); }
        });

        return Task.CompletedTask;
    }
}
