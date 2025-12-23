using System.Reflection;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared.EntitySystem;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Services;

internal class TestService
{
    // [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    // internal delegate nint DispatchSpawnHook( nint entity, nint kv );

    private readonly ILogger<TestService> logger;
    private readonly ProfileService profile;
    private readonly ISwiftlyCore core;

    public unsafe TestService( ILogger<TestService> logger, ProfileService profileService, ISwiftlyCore core )
    {
        this.profile = profileService;
        this.core = core;
        this.logger = logger;

        logger.LogWarning("TestService created");
        logger.LogWarning("TestService created");
        logger.LogWarning("TestService created");
        logger.LogWarning("TestService created");
        logger.LogWarning("TestService created");
        logger.LogWarning("TestService created");
        logger.LogWarning("TestService created");
        logger.LogWarning("TestService created");
        logger.LogWarning("TestService created");

        core.Registrator.Register(this);
        Test2();
    }

    private static void PrintStructFields<T>( T obj ) where T : struct
    {
        var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (var field in fields)
        {
            var value = field.GetValue(obj);
            Console.WriteLine($"{field.Name}: {value}");
        }
    }

    public void Test()
    {
        _ = core.Scheduler.RepeatBySeconds(1.0f, () =>
        {
            var gameServer = NativeEngineHelpers.GetNetworkGameServer();
            unsafe
            {
                ref var array = ref gameServer.AsRef<CUtlVector<nint>>(624);
                foreach (var client in array)
                {
                    if (client == 0)
                    {
                        continue;
                    }
                    ref var serversideClient = ref client.AsRef<CServerSideClient>();
                    PrintStructFields(serversideClient.Base);
                }
            }
        });
    }

    public void Test2()
    {
        var variant = new CVariant<CVariantDefaultAllocator>(100);
        Console.WriteLine(variant.ToString());
        variant.SetString("LOL");
        Console.WriteLine(variant.ToString());
        variant.SetVector(new(1, 1, 1));
        Console.WriteLine(variant.ToString());
    }

    // [EntityOutputHandler("*", "*")]
    // public void Test3( IOnEntityFireOutputHookEvent @event )
    // {
    //     Console.WriteLine("MFMFMFMFMFMFMFMFMF");
    //     Console.WriteLine($"HookEntityOutput -> designerName: {@event.DesignerName} output: {@event.OutputName}, activator: {@event.Activator?.As<CBaseEntity>()?.DesignerName}, caller: {@event.Caller?.As<CBaseEntity>()?.DesignerName}, value: {@event.VariantValue}, delay: {@event.Delay}");
    // }

    // [EntityInputHandler("*", "*")]
    // public void Test4( IOnEntityIdentityAcceptInputHookEvent @event )
    // {
    //     Console.WriteLine("FMFMFMFMFMFMFMFMFM");
    //     Console.WriteLine($"HookEntityInput -> designerName: {@event.DesignerName} output: {@event.InputName}, activator: {@event.Activator?.As<CBaseEntity>()?.DesignerName}, caller: {@event.Caller?.As<CBaseEntity>()?.DesignerName}, value: {@event.VariantValue}");
    // }

    [EntityOutputHandler<CPropDoorRotating>("*")]
    public void Test3( IOnEntityFireOutputHookEvent @event )
    {
        Console.WriteLine($"HookEntityOutput -> designerName: {@event.DesignerName} output: {@event.OutputName}, activator: {@event.Activator?.As<CBaseEntity>()?.DesignerName}, caller: {@event.Caller?.As<CBaseEntity>()?.DesignerName}, value: {@event.VariantValue}, delay: {@event.Delay}");
    }

    [EntityInputHandler<CCSPlayerPawn>("*")]
    public void Test4( IOnEntityIdentityAcceptInputHookEvent @event )
    {
        Console.WriteLine($"HookEntityInput -> designerName: {@event.DesignerName} output: {@event.InputName}, activator: {@event.Activator?.As<CBaseEntity>()?.DesignerName}, caller: {@event.Caller?.As<CBaseEntity>()?.DesignerName}, value: {@event.VariantValue}");
    }
}