using BenchmarkDotNet.Attributes;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace TestPlugin;

public class PlayerBenchmarks
{
    private CCSPlayerController _controller;

    [GlobalSetup]
    public void Setup()
    {
        _controller = BenchContext.Controller;

        if (_controller is null)
        {
            throw new InvalidOperationException("Controller is not set");
        }
    }

    [Benchmark]
    public void Test()
    {
        for (int i = 0; i < 10000; i++)
        {
            var a = _controller.Pawn.Value.WeaponServices.ActiveWeapon;
        }
    }
}