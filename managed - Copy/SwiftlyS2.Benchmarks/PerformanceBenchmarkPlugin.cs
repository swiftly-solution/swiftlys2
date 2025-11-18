using System.Runtime.InteropServices;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Exporters.Xml;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Plugins;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Benchmarks;

[PluginMetadata(Id = "benchmark", Version = "1.0.0")]
public class PerformanceBenchmarkPlugin : BasePlugin
{
    public PerformanceBenchmarkPlugin( ISwiftlyCore core ) : base(core)
    {
    }

    public override void Load( bool hotReload )
    {
        NativeInteropBenchmark.SetCore(Core);
        Core.Registrator.Register(this);
    }

    [Command("benchmark", true)]
    public void RunBenchmark( ICommandContext context )
    {
        Console.WriteLine("\n");
        Console.WriteLine("\n");

        var job = Job.Default
            .WithToolchain(InProcessEmitToolchain.Instance)
            .WithWarmupCount(5)
            .WithIterationCount(20)
            .WithMinIterationCount(15)
            .WithMaxIterationCount(30)
            .WithGcServer(true)
            .WithGcForce(false);

        var config = ManualConfig
            .Create(DefaultConfig.Instance)
            .AddJob(job)
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .AddColumn(StatisticColumn.P95)
            .AddColumn(StatisticColumn.StdDev)
            .AddColumn(StatisticColumn.Median)
            .AddExporter(HtmlExporter.Default)
            .AddExporter(MarkdownExporter.GitHub)
            .AddExporter(CsvExporter.Default)
            .AddExporter(JsonExporter.Full)
            .KeepBenchmarkFiles(true);

        BenchmarkRunner.Run<NativeInteropBenchmark>(config);

        Console.WriteLine("\n");
        Console.WriteLine("\n");
    }

    public override void Unload()
    {
    }
}

/// <summary>
/// BenchmarkDotNet tests for measuring C# to C++ interop overhead by pattern
/// Tests different interop scenarios using safe benchmark-specific native functions
/// </summary>
[MemoryDiagnoser]
public class NativeInteropBenchmark
{
    [DllImport("swiftlys2", EntryPoint = "SwiftlyS2_Benchmark_PInvoke", CallingConvention = CallingConvention.Cdecl)]
    private static extern int SwiftlyS2BenchmarkPInvoke();

    [DllImport("kernel32.dll")]
    private static extern int GetCurrentProcessId();

    public static void SetCore( ISwiftlyCore sw2Core ) { }

    // [Benchmark(Baseline = true, Description = "Baseline: Pure C# method")]
    // public int Baseline_ManagedCall()
    // {
    //     return 1337;
    // }

    // [Benchmark(Description = "Baseline: P/Invoke (usermode)")]
    // public int Baseline_PInvoke_Usermode()
    // {
    //     return SwiftlyS2BenchmarkPInvoke();
    // }

    // [Benchmark(Description = "Baseline: P/Invoke (kernel32)")]
    // public int Baseline_PInvoke_Kernel()
    // {
    //     return GetCurrentProcessId();
    // }

    // [Benchmark(Description = "Pattern 1: void -> void")]
    // public void Pattern1_VoidToVoid()
    // {
    //     NativeBenchmark.VoidToVoid();
    // }

    // [Benchmark(Description = "Pattern 2: void -> int32")]
    // public int Pattern2_VoidToInt32()
    // {
    //     return NativeBenchmark.GetInt32();
    // }

    // [Benchmark(Description = "Pattern 3: void -> ptr")]
    // public IntPtr Pattern3_VoidToPtr()
    // {
    //     return NativeBenchmark.GetPtr();
    // }

    // [Benchmark(Description = "Pattern 4: int32 -> int32")]
    // public int Pattern4_Int32ToInt32()
    // {
    //     return NativeBenchmark.Int32ToInt32(42);
    // }

    // [Benchmark(Description = "Pattern 5: float -> float")]
    // public float Pattern5_FloatToFloat()
    // {
    //     return NativeBenchmark.FloatToFloat(3.14f);
    // }

    // [Benchmark(Description = "Pattern 6: string -> string")]
    // public string Pattern6_StringToString()
    // {
    //     return NativeBenchmark.StringToString("test");
    // }

    [Benchmark(Description = "Pattern 7: string -> ptr")]
    public IntPtr Pattern7_StringToPtr()
    {
        return NativeBenchmark.StringToPtr("test");
    }

    // [Benchmark(Description = "Pattern 8: 5 params")]
    // public int Pattern8_MultiPrimitives()
    // {
    //     return NativeBenchmark.MultiPrimitives(IntPtr.Zero, 100, 3.14f, true, 999UL);
    // }

    [Benchmark(Description = "Pattern 9: 5 params + 1 string")]
    public int Pattern9_MultiWithOneString()
    {
        return NativeBenchmark.MultiWithOneString(IntPtr.Zero, "test", IntPtr.Zero, 42, 1.5f);
    }

    [Benchmark(Description = "Pattern 10: 5 params + 2 strings")]
    public void Pattern10_MultiWithTwoStrings()
    {
        NativeBenchmark.MultiWithTwoStrings(IntPtr.Zero, "test1", IntPtr.Zero, "test2", 100);
    }

    // [Benchmark(Description = "Pattern 11: void -> bool")]
    // public bool Pattern11_VoidToBool()
    // {
    //     return NativeBenchmark.GetBool();
    // }

    // [Benchmark(Description = "Pattern 12: void -> uint32")]
    // public uint Pattern12_VoidToUInt32()
    // {
    //     return NativeBenchmark.GetUInt32();
    // }

    // [Benchmark(Description = "Pattern 13: void -> int64")]
    // public long Pattern13_VoidToInt64()
    // {
    //     return NativeBenchmark.GetInt64();
    // }

    // [Benchmark(Description = "Pattern 14: void -> uint64")]
    // public ulong Pattern14_VoidToUInt64()
    // {
    //     return NativeBenchmark.GetUInt64();
    // }

    // [Benchmark(Description = "Pattern 15: void -> float")]
    // public float Pattern15_VoidToFloat()
    // {
    //     return NativeBenchmark.GetFloat();
    // }

    // [Benchmark(Description = "Pattern 16: void -> double")]
    // public double Pattern16_VoidToDouble()
    // {
    //     return NativeBenchmark.GetDouble();
    // }

    // [Benchmark(Description = "Pattern 17: bool -> bool")]
    // public bool Pattern17_BoolToBool()
    // {
    //     return NativeBenchmark.BoolToBool(true);
    // }

    // [Benchmark(Description = "Pattern 18: uint32 -> uint32")]
    // public uint Pattern18_UInt32ToUInt32()
    // {
    //     return NativeBenchmark.UInt32ToUInt32(1337u);
    // }

    // [Benchmark(Description = "Pattern 19: int64 -> int64")]
    // public long Pattern19_Int64ToInt64()
    // {
    //     return NativeBenchmark.Int64ToInt64(9999999L);
    // }

    // [Benchmark(Description = "Pattern 20: uint64 -> uint64")]
    // public ulong Pattern20_UInt64ToUInt64()
    // {
    //     return NativeBenchmark.UInt64ToUInt64(9999999UL);
    // }

    // [Benchmark(Description = "Pattern 21: double -> double")]
    // public double Pattern21_DoubleToDouble()
    // {
    //     return NativeBenchmark.DoubleToDouble(3.14159);
    // }

    // [Benchmark(Description = "Pattern 22: ptr -> ptr")]
    // public IntPtr Pattern22_PtrToPtr()
    // {
    //     return NativeBenchmark.PtrToPtr(new IntPtr(0x1234));
    // }

    // [Benchmark(Description = "Pattern 23: Vector -> Vector")]
    // public unsafe Vector Pattern23_VectorToVector()
    // {
    //     Vector result;
    //     NativeBenchmark.VectorToVector((nint)(&result), new Vector(1.0f, 2.0f, 3.0f));
    //     return result;
    // }

    // [Benchmark(Description = "Pattern 24: QAngle -> QAngle")]
    // public unsafe QAngle Pattern24_QAngleToQAngle()
    // {
    //     QAngle result;
    //     NativeBenchmark.QAngleToQAngle((nint)(&result), new QAngle(45.0f, 90.0f, 0.0f));
    //     return result;
    // }

    [Benchmark(Description = "Pattern 25: Vector + QAngle + String)")]
    public void Pattern26_ComplexWithString()
    {
        NativeBenchmark.ComplexWithString(
            IntPtr.Zero,
            new Vector(100.0f, 200.0f, 300.0f),
            "test_entity",
            new QAngle(0.0f, 180.0f, 0.0f)
        );
    }
}