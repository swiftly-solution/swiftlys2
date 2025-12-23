using BenchmarkDotNet.Attributes;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace TestPlugin;

public class PlayerBenchmarks
{
    private CCSPlayerController? controller;

    [GlobalSetup]
    public void Setup()
    {
        controller = BenchContext.Controller;

        if (controller == null)
        {
            throw new InvalidOperationException("Controller is not set");
        }
    }

    [Benchmark]
    public void Test()
    {
        for (var i = 0; i < 10000; i++)
        {
            _ = controller?.Pawn.Value?.WeaponServices?.ActiveWeapon;
        }
    }
}