using System.Collections;
using System.Reflection;
using Microsoft.Extensions.Logging.Abstractions;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

var tests = new (string Name, Action Run)[] {
    ("Service order and handler order", ServiceOrder),
    ("Stop and Handled apply between services", GroupedResults),
    ("CancelOriginal continues dispatch", CancelOriginal),
    ("Exceptions are isolated per service", Exceptions),
    ("Duplicate and multicast removal", Removal),
    ("Pre and Post remain independent", Phases),
    ("Removing an absent listener preserves native count", EmptyRemoval),
    ("Null listeners preserve native count", NullListeners),
    ("Concurrent listener updates preserve native count", ConcurrentUpdates),
    ("Disposal clears captured delegates", DisposedDelegates),
    ("Disposal skips a later service", LaterDisposal),
    ("Listener discovery covers every published phase", Discovery),
};

var failures = 0;
foreach (var test in tests.Where(test => args.Length == 0 || args.Any(arg => test.Name.Contains(arg, StringComparison.OrdinalIgnoreCase))))
{
    Fixture.SeedNativeCounts();
    try
    {
        test.Run();
        Console.WriteLine($"PASS {test.Name}");
    }
    catch (Exception error)
    {
        failures++;
        Console.WriteLine($"FAIL {test.Name}: {error.Message}");
    }
}
return failures == 0 ? 0 : 1;

static void Equal<T>( T expected, T actual )
{
    if (!EqualityComparer<T>.Default.Equals(expected, actual))
        throw new Exception($"Expected {expected}; got {actual}.");
}

static void ServiceOrder()
{
    using var first = new Fixture();
    using var second = new Fixture();
    var calls = new List<string>();
    second.Hook.Pre += ( ref ProcessMovementMovementPreContext ctx ) => calls.Add("second");
    first.Hook.Pre += ( ref ProcessMovementMovementPreContext ctx ) => calls.Add("first-a");
    first.Hook.Pre += ( ref ProcessMovementMovementPreContext ctx ) => calls.Add("first-b");
    Fixture.DispatchPre();
    Equal("first-a,first-b,second", string.Join(',', calls));
}

static void GroupedResults()
{
    foreach (var result in new[] { HookResult.Stop, HookResult.Handled })
    {
        using var first = new Fixture();
        using var second = new Fixture();
        var calls = new List<int>();
        first.Hook.Pre += ( ref ProcessMovementMovementPreContext ctx ) => { calls.Add(1); ctx.SetHookResult(result); };
        first.Hook.Pre += ( ref ProcessMovementMovementPreContext ctx ) => calls.Add(2);
        second.Hook.Pre += ( ref ProcessMovementMovementPreContext ctx ) => calls.Add(3);
        Fixture.DispatchPre();
        Equal("1,2", string.Join(',', calls));
    }
}

static void CancelOriginal()
{
    using var first = new Fixture();
    using var second = new Fixture();
    var calls = 0;
    first.Hook.Pre += ( ref ProcessMovementMovementPreContext ctx ) => ctx.SetHookResult(HookResult.CancelOriginal);
    second.Hook.Pre += ( ref ProcessMovementMovementPreContext ctx ) => calls++;
    Fixture.DispatchPre();
    Equal(1, calls);
}

static void Exceptions()
{
    using var first = new Fixture();
    using var second = new Fixture();
    var calls = new List<int>();
    first.Hook.Pre += ( ref ProcessMovementMovementPreContext ctx ) => throw new InvalidOperationException("test callback");
    first.Hook.Pre += ( ref ProcessMovementMovementPreContext ctx ) => calls.Add(1);
    second.Hook.Pre += ( ref ProcessMovementMovementPreContext ctx ) => calls.Add(2);
    Fixture.DispatchPre();
    Equal("2", string.Join(',', calls));
}

static void Removal()
{
    using var service = new Fixture();
    var calls = new List<int>();
    OnProcessMovementMovementPreDelegate a = ( ref ProcessMovementMovementPreContext ctx ) => calls.Add(1);
    OnProcessMovementMovementPreDelegate b = ( ref ProcessMovementMovementPreContext ctx ) => calls.Add(2);
    service.Hook.Pre += a + b;
    service.Hook.Pre += a + b;
    service.Hook.Pre -= a + b;
    Fixture.DispatchPre();
    Equal("1,2", string.Join(',', calls));
    service.Hook.Pre -= a;
    calls.Clear();
    Fixture.DispatchPre();
    Equal("2", string.Join(',', calls));
}

static void Phases()
{
    using var service = new Fixture();
    var pre = 0;
    var post = 0;
    OnProcessMovementMovementPreDelegate onPre = ( ref ProcessMovementMovementPreContext ctx ) => pre++;
    service.Hook.Pre += onPre;
    service.Hook.Post += ( ref ProcessMovementMovementPostContext ctx ) => post++;
    Fixture.DispatchPre();
    Equal(1, pre);
    Equal(0, post);
    service.Hook.Pre -= onPre;
    Fixture.DispatchPre();
    Fixture.DispatchPost();
    Equal(1, pre);
    Equal(1, post);
}

static void EmptyRemoval()
{
    using var service = new Fixture();
    var before = Fixture.NativeCount;
    service.Hook.Pre -= ( ref ProcessMovementMovementPreContext ctx ) => { };
    Equal(before, Fixture.NativeCount);
}

static void NullListeners()
{
    using var service = new Fixture();
    var before = Fixture.NativeCount;
    service.Hook.Pre += null!;
    Equal(before, Fixture.NativeCount);
    service.Hook.Post += null!;
    Equal(before, Fixture.NativeCount);
}

static void ConcurrentUpdates()
{
    using var service = new Fixture();
    var before = Fixture.NativeCount;
    OnProcessMovementMovementPreDelegate handler = ( ref ProcessMovementMovementPreContext ctx ) => { };
    Parallel.For(0, 16, _ => {
        for (var i = 0; i < 100; i++)
        {
            service.Hook.Pre += handler;
            service.Hook.Pre -= handler;
        }
    });
    Equal(before, Fixture.NativeCount);
}

static void DisposedDelegates()
{
    using var service = new Fixture();
    var calls = 0;
    service.Hook.Pre += ( ref ProcessMovementMovementPreContext ctx ) => calls++;
    service.Hook.Post += ( ref ProcessMovementMovementPostContext ctx ) => calls++;
    var invokePre = service.Hook.GetType().GetMethod("InvokePre")!.CreateDelegate<OnProcessMovementMovementPreDelegate>(service.Hook);
    var invokePost = service.Hook.GetType().GetMethod("InvokePost")!.CreateDelegate<OnProcessMovementMovementPostDelegate>(service.Hook);
    service.Dispose();
    var pre = new ProcessMovementMovementPreContext();
    var post = new ProcessMovementMovementPostContext();
    invokePre(ref pre);
    invokePost(ref post);
    Equal(0, calls);
}

static void LaterDisposal()
{
    using var first = new Fixture();
    using var second = new Fixture();
    var calls = 0;
    first.Hook.Pre += ( ref ProcessMovementMovementPreContext ctx ) => second.Dispose();
    second.Hook.Pre += ( ref ProcessMovementMovementPreContext ctx ) => calls++;
    Fixture.DispatchPre();
    Equal(0, calls);
}

static void Discovery()
{
    var fields = (from category in Fixture.ServiceType.GetFields(Fixture.InstanceFields)
                  from hook in category.FieldType.GetFields(Fixture.InstanceFields)
                  from listener in hook.FieldType.GetFields(Fixture.InstanceFields)
                  where typeof(Delegate).IsAssignableFrom(listener.FieldType)
                  select listener).ToArray();
    var phases = Fixture.Publisher.GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
        .Where(method => method.Name.StartsWith("Invoke") && method.GetParameters() is [{ ParameterType.IsByRef: true }])
        .ToArray();
    Equal(fields.Length, phases.Length);
    Equal(true, phases.Length > 0);
    foreach (var phase in phases)
    {
        var context = phase.GetParameters()[0].ParameterType;
        var matches = fields.Where(field => field.FieldType.GetMethod("Invoke")!.GetParameters() is [var parameter] && parameter.ParameterType == context).ToArray();
        Equal(1, matches.Length);
    }
}

sealed class Fixture : IDisposable
{
    public const BindingFlags InstanceFields = BindingFlags.Instance | BindingFlags.NonPublic;
    public static readonly Type ServiceType = typeof(IGameHooks).Assembly.GetType("SwiftlyS2.Core.GameHooks.GameHooksService")!;
    public static readonly Type Publisher = ServiceType.Assembly.GetType("SwiftlyS2.Core.GameHooks.GameHooksPublisher")!;
    private static readonly Type HookNames = ServiceType.Assembly.GetType("SwiftlyS2.Core.GameHooks.HookListener")!;
    private static readonly IDictionary NativeCounts = (IDictionary)Publisher.GetField("hookListeners", BindingFlags.NonPublic | BindingFlags.Static)!.GetValue(null)!;
    private static readonly object MovementHookName = Enum.Parse(HookNames, "ProcessMovement");
    private static readonly OnProcessMovementMovementPreDelegate InvokePre = Publisher.GetMethod("InvokeProcessMovementPre", BindingFlags.NonPublic | BindingFlags.Static)!.CreateDelegate<OnProcessMovementMovementPreDelegate>();
    private static readonly OnProcessMovementMovementPostDelegate InvokePost = Publisher.GetMethod("InvokeProcessMovementPost", BindingFlags.NonPublic | BindingFlags.Static)!.CreateDelegate<OnProcessMovementMovementPostDelegate>();
    private readonly object service;

    public IProcessMovementMovementHook Hook => ((IGameHooks)service).Movement.ProcessMovement;
    public static int NativeCount => (int)NativeCounts[MovementHookName]!;

    public Fixture()
    {
        var logger = typeof(NullLogger<>).MakeGenericType(ServiceType).GetField("Instance")!.GetValue(null);
        service = Activator.CreateInstance(ServiceType, [null, logger])!;
    }

    public static void SeedNativeCounts()
    {
        // Keep native attachment outside these managed dispatch tests.
        foreach (var hook in Enum.GetValues(HookNames)) NativeCounts[hook] = 10000;
    }

    public static void DispatchPre()
    {
        var context = new ProcessMovementMovementPreContext();
        InvokePre(ref context);
    }

    public static void DispatchPost()
    {
        var context = new ProcessMovementMovementPostContext();
        InvokePost(ref context);
    }

    public void Dispose() => ((IDisposable)service).Dispose();
}
