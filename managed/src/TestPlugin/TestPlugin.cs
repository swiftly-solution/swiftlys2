using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tomlyn.Extensions.Configuration;
using System.Text.RegularExpressions;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Plugins;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.ProtobufDefinitions;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Memory;
using YamlDotNet.Core.Tokens;
using Dapper;
using SwiftlyS2.Shared.Sounds;
using SwiftlyS2.Shared.EntitySystem;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using SwiftlyS2.Shared.Players;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using SwiftlyS2.Shared.Menus;
using SwiftlyS2.Shared.SteamAPI;
using SwiftlyS2.Core.Menus.OptionsBase;
using System.Collections.Concurrent;
using Dia2Lib;
using System.Reflection.Metadata;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;

namespace TestPlugin;

public class TestConfig
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class InProcessConfig : ManualConfig
{
    public InProcessConfig()
    {
        AddLogger(ConsoleLogger.Default);
        AddJob(Job.Default
            .WithToolchain(new InProcessNoEmitToolchain(true))
            .WithId("InProcess"));
    }
}

[PluginMetadata(Id = "sw2.testplugin", Version = "1.0.0")]
public class TestPlugin : BasePlugin
{
    public TestPlugin( ISwiftlyCore core ) : base(core)
    {
        Console.WriteLine("[TestPlugin] TestPlugin constructed successfully!");
        // Console.WriteLine($"sizeof(bool): {sizeof(bool)}");
        // Console.WriteLine($"Marshal.SizeOf<bool>: {Marshal.SizeOf<bool>()}");
        Core.Event.OnWeaponServicesCanUseHook += ( @event ) =>
        {
            // Console.WriteLine($"WeaponServicesCanUse: {@event.Weapon.WeaponBaseVData.AttackMovespeedFactor} {@event.OriginalResult}");

        };
    }

    [Command("be")]
    [CommandAlias("beh", false)]
    public void Test2Command( ICommandContext context )
    {
        Console.WriteLine("hello world");
        Console.WriteLine("\n");
    }

    [GameEventHandler(HookMode.Pre)]
    public HookResult OnPlayerSpawn( EventPlayerSpawn @event )
    {
        if (!@event.UserIdPlayer.IsValid)
        {
            return HookResult.Continue;
        }
        var player = @event.UserIdPlayer.RequiredController;
        if (player.InGameMoneyServices?.IsValid == true)
        {
            player.InGameMoneyServices.Account = Core.ConVar.Find<int>("mp_maxmoney")?.Value ?? 16000;
            player.InGameMoneyServices.AccountUpdated();
        }
        return HookResult.Continue;
    }

    public override void OnAllPluginsLoaded()
    {
        base.OnAllPluginsLoaded();

        for (var x = 0; x < 30; x++)
        {
            var builder = Core.MenusAPI
                .CreateBuilder()
                .Design.SetMenuTitle($"Test Menu {x + 1}");
            for (var j = 0; j < 5; j++)
            {
                var optionText = $"Menu # {x + 1} - Option # {j + 1}";
                var button = new ButtonMenuOption(optionText) { TextStyle = MenuOptionTextStyle.ScrollLeftLoop, MaxWidth = 16f };
                button.Click += ( sender, args ) =>
                {
                    args.Player.SendChat($"Clicked: {optionText}");
                    return ValueTask.CompletedTask;
                };
                _ = builder.AddOption(button);
            }

            var menu = builder.Build();
        }
    }

    public override void Load( bool hotReload )
    {
        // Core.Command.HookClientCommand((playerId, commandLine) =>
        // {
        //   Console.WriteLine("TestPlugin HookClientCommand " + playerId + " " + commandLine);
        //   return HookResult.Continue;
        // });

        // Core.Event.OnConsoleOutput += (@event) =>
        // {
        //   Console.WriteLine($"[TestPlugin] ConsoleOutput: {@event.Message}");
        // };

        // Core.Event.OnCommandExecuteHook += (@event) =>
        // {
        //   if (@event.HookMode == HookMode.Pre) return;
        //   Core.Logger.LogInformation("CommandExecute: {name} with {args}", @event.Command[0], @event.Command.ArgS);
        // };

        // Core.Event.OnEntityStartTouch += (@event) =>
        // {
        //   Console.WriteLine($"[New] EntityStartTouch: {@event.Entity.Entity?.DesignerName} -> {@event.OtherEntity.Entity?.DesignerName}");
        // };

        // Core.Event.OnEntityTouchHook += (@event) =>
        // {
        //   switch (@event.TouchType)
        //   {
        //     case EntityTouchType.StartTouch:
        //       Console.WriteLine($"EntityStartTouch: {@event.Entity.Entity?.DesignerName} -> {@event.OtherEntity.Entity?.DesignerName}");
        //       break;
        //     case EntityTouchType.Touch:
        //       break;
        //     case EntityTouchType.EndTouch:
        //       if (@event.Entity.Entity?.DesignerName != "player" || @event.OtherEntity.Entity?.DesignerName != "player")
        //       {
        //         return;
        //       }
        //       var player = @event.Entity.As<CCSPlayerPawn>();
        //       var otherPlayer = @event.OtherEntity.As<CCSPlayerPawn>();
        //       Console.WriteLine($"EntityEndTouch: {(player.Controller.Value?.PlayerName ?? string.Empty)} -> {(otherPlayer.Controller.Value?.PlayerName ?? string.Empty)}");
        //       break;
        //   }
        // };

        // Core.Event.OnPlayerPawnPostThink += ( @event ) =>
        // {
        //     Console.WriteLine($"PostThink -> {@event.PlayerPawn.OriginalController.Value?.PlayerName}");
        // };

        Core.Engine.ExecuteCommandWithBuffer("@ping", ( buffer ) =>
        {
            Console.WriteLine($"pong: {buffer}");
        });

        Core.GameEvent.HookPre<EventShowSurvivalRespawnStatus>(@event =>
        {
            @event.LocToken = "test";
            return HookResult.Continue;
        });

        Core.Configuration
            .InitializeJsonWithModel<TestConfig>("test.jsonc", "Main")
            .Configure(( builder ) =>
            {
                builder.AddJsonFile("test.jsonc", optional: false, reloadOnChange: true);
                builder.AddTomlFile("test.toml", optional: true, reloadOnChange: true);
            });

        ServiceCollection services = new();

        services
            .AddSwiftly(Core);

        Core.Event.OnPrecacheResource += ( @event ) =>
        {
            @event.AddItem("soundevents/mvp_anthem.vsndevts");
        };

        Core.Event.OnConVarValueChanged += ( @event ) =>
        {
            Console.WriteLine($"ConVar {@event.ConVarName} changed from {@event.OldValue} to {@event.NewValue} by player {@event.PlayerId}");
        };


        // var provider = services.BuildServiceProvider();

        // provider.GetRequiredService<TestService>();


        // Host.CreateDefaultBuilder()
        //   .ConfigureLogging((context, logging) => {
        //     logging.AddConsole();
        //   })
        //   .ConfigureAppConfiguration((context, config) => {
        //     config.SetBasePath(Core.Configuration.GetBasePath());
        //     config.AddJsonFile("test.jsonc", optional: false, reloadOnChange: true);
        //   })
        //   .ConfigureServices((context, services) => {
        //     services.AddOptionsWithValidateOnStart<IOptionsMonitor<TestConfig>>()
        //       .Bind(context.Configuration.GetSection("Main"));
        //   })
        //   .Build();

        // This can be used everywhere and the value will be updated when the config is changed
        // Console.WriteLine(config.CurrentValue.Age);


        // var config = new TestConfig();

        // throw new Exception("TestPlugin loaded");

        // Core.

        int i = 0;

        // var token2 = Core.Scheduler.Repeat(10, () => {
        //   Console.WriteLine(Core.Engine.TickCount);
        //   Console.WriteLine("TestPlugin Timer");
        // });
        Core.Logger.LogInformation("TestPlugin loaded");

        using var se = new SoundEvent();

        // var func = Core.Memory.GetUnmanagedFunctionByAddress<Test>(Core.Memory.GetAddressBySignature(Library.Server, "AAAAA")!.Value);

        // func.CallOriginal(1, 2);

        // func.Call(1, 2);

        // func.AddHook((next) => {
        //   return (a, b) => {
        //     Console.WriteLine("TestPlugin Hook " + a + " " + b);
        //     next()(a, b);
        //   };
        // });


        // Entrypoint

        // Core.Event.OnTick += () => {
        //   Console.WriteLine("TestPlugin OnTick ");
        // };

        // Core.Event.OnClientConnected += (@event) => {
        //   Console.WriteLine("TestPlugin OnClientConnected " + @event.PlayerId);
        // };

        // Core.Event.OnClientPutInServer += (@event) => {
        //   Console.WriteLine("TestPlugin OnClientPutInServer " + @event.PlayerId);
        // };

        Core.Event.OnClientDisconnected += ( @event ) =>
        {
            Console.WriteLine("TestPlugin OnClientDisconnected " + @event.PlayerId);
        };

        var convar = Core.ConVar.Find<float>("sv_cs_player_speed_has_hostage");
        // Core.Event.OnTick += () =>
        // {
        //     var players = Core.PlayerManager.GetAllPlayers();
        //     foreach (var player in players)
        //     {
        //         Core.Profiler.StartRecording("OnTick Send 1024 sv_cs_player_speed_has_hostage convar at player");
        //         for (int i = 0; i < 1024; i++)
        //         {
        //             convar!.ReplicateToClient(player.PlayerID, (float)Random.Shared.NextDouble());
        //         }
        //         Core.Profiler.StopRecording("OnTick Send 1024 sv_cs_player_speed_has_hostage convar at player");
        //     }
        // };

        // Core.Event.OnClientProcessUsercmds += (@event) => {
        //   foreach(var usercmd in @event.Usercmds) {
        //     usercmd.Base.ButtonsPb.Buttonstate1 &= 1UL << (int)GameButtons.Ctrl;
        //     usercmd.Base.ButtonsPb.Buttonstate2 &= 1UL << (int)GameButtons.Ctrl;
        //     usercmd.Base.ButtonsPb.Buttonstate3 &= 1UL << (int)GameButtons.Ctrl;
        //   }
        // };

        // Core.NetMessage.HookClientMessage<CCLCMsg_Move>((msg, id) => {
        //   Console.WriteLine("TestPlugin OnClientMove ");
        //   Console.WriteLine(BitConverter.ToString(msg.Data));
        //   return HookResult.Continue;
        // });

        Core.Event.OnEntityTakeDamage += ( @event ) =>
        {
            Console.WriteLine(@event.Entity.DesignerName);
            @event.Info.DamageFlags = TakeDamageFlags_t.DFLAG_SUPPRESS_BREAKABLES;
            @event.Result = HookResult.Stop;
        };

        // Core.Event.OnTick += () => {

        //   Console.WriteLine("TestPlugin OnTick");
        // };

        // Core.Event.OnEntityCreated += (ev) => {
        //   var entity = ev.Entity;
        //   entity.Entity.DesignerName = "a";
        //   Console.WriteLine("TestPlugin OnEntityCreated " + ev.Entity.Entity?.DesignerName);
        // };

        using CEntityKeyValues kv = new();

        kv.SetBool("test", true);

        Console.WriteLine(kv.Get<bool>("test2"));

        CUtlStringToken token = new("hello");
        Console.WriteLine($"2");
    }

    CEntityKeyValues kv { get; set; }
    CEntityInstance entity { get; set; }

    [Command("gd")]
    public void TestCommandGD( ICommandContext ctx )
    {
        var player = ctx.Sender;
        ctx.Reply($"Ground distance: {player!.RequiredPawn.GroundDistance}");
    }

    [Command("hh")]
    public void TestCommandHH( ICommandContext _ )
    {
        var filter = new CTraceFilter {
            IterateEntities = true,
            QueryShapeAttributes = new() {
                InteractsWith = MaskTrace.Player,
                InteractsExclude = MaskTrace.Sky,
                InteractsAs = MaskTrace.Player,
                CollisionGroup = CollisionGroup.PlayerMovement,
                ObjectSetMask = RnQueryObjectSet.All,
                HitSolid = true
            }
        };

        var start = new Vector(0, 0, 0);
        var end = new Vector(0, 0, 100);

        Console.WriteLine("AAA");
        var ray = new Ray_t();
        var trace = new CGameTrace();
        Console.WriteLine("AAA");
        Core.Trace.TraceShape(start, end, ray, filter, ref trace);

        Console.WriteLine(trace.Entity.IsValid);


    }

    [Command("tt")]
    public void TestCommand( ICommandContext context )
    {
        // token2?.Cancel();
        // kv = new();
        // kv.SetString("test", "SAFE");

        // _Core.Logger.LogInformation("!@#");

        // _Core.Logger.LogInformation(_Core.GameData.GetSignature("CEntityInstance::AcceptInput").ToString());

        // entity = _Core.EntitySystem.CreateEntityByDesignerName<CPointWorldText>("point_worldtext");
        // entity.DispatchSpawn(kv);
        // Console.WriteLine("Spawned entity with keyvalues");

        int j = 0;

        var cvar = Core.ConVar.Find<bool>("sv_cheats")!;
        Console.WriteLine(cvar);
        Console.WriteLine(cvar.Value);
        var cvar2 = Core.ConVar.Find<bool>("sv_autobunnyhopping")!;
        Console.WriteLine(cvar2);
        Console.WriteLine(cvar2.Value);

        var cvar3 = Core.ConVar.Create<string>("sw_test_cvar", "Test cvar", "ABCDEFG");
        Console.WriteLine(cvar3);
        Console.WriteLine(cvar3.Value);

        var cvar4 = Core.ConVar.Find<bool>("r_drawworld")!;

        cvar2.ReplicateToClient(0, true);

        cvar4.QueryClient(0, ( value ) =>
        {
            Console.WriteLine("QueryCallback " + value);
        });
    }

    [Command("w")]
    public void TestCommand1( ICommandContext context )
    {
        var ret = SteamGameServerUGC.DownloadItem(new PublishedFileId_t(3596198331), true);
        Console.WriteLine(SteamGameServer.GetPublicIP().ToIPAddress());
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate nint DispatchSpawnDelegate( nint pEntity, nint pKV );
    int order = 0;

    IUnmanagedFunction<DispatchSpawnDelegate>? _dispatchspawn;

    [Command("h0")]
    public void TestCommand0( ICommandContext _ )
    {
        var targetAddress = Core.Memory.GetAddressBySignature(Library.Server, "E8 ? ? ? ? 48 8B 46 ? 48 85 DB");
        if (targetAddress.HasValue)
        {
            var unmanagedMemory = Core.Memory.GetUnmanagedMemoryByAddress(targetAddress.Value);
            var hookId = unmanagedMemory.AddHook(( ref MidHookContext context ) =>
            {
                Console.WriteLine($"Mid-hook triggered at 0x{targetAddress.Value:X}");
                Console.WriteLine($"RAX: 0x{context.RAX:X}, RCX: 0x{context.RCX:X}, RDX: 0x{context.RDX:X}");
            });
        }
    }

    [Command("h1")]
    public void TestCommand2( ICommandContext _ )
    {
        // var token = Core.Scheduler.DelayAndRepeat(500, 1000, () =>
        // {

        // });

        var addres = Core.GameData.GetSignature("CBaseEntity::DispatchSpawn");

        var func = Core.Memory.GetUnmanagedFunctionByAddress<DispatchSpawnDelegate>(addres);
        var guid = func.AddHook(( next ) =>
        {
            return ( pEntity, pKV ) =>
            {
                Console.WriteLine("TestPlugin DispatchSpawn " + order++);
                return next()(pEntity, pKV);
            };
        });

        var memory = Core.Memory.GetUnmanagedMemoryByAddress(addres);
        var guid1 = memory.AddHook(( ref MidHookContext context ) =>
        {
            Core.Logger.LogInformation("MidHookContext:\n{Context}", context);
        });

        // _dispatchspawn.AddHook(( next ) =>
        // {
        //     return ( pEntity, pKV ) =>
        //     {
        //         Console.WriteLine("TestPlugin DispatchSpawn2 " + order++);
        //         return next()(pEntity, pKV);
        //     };
        // });
    }

    [EventListener<EventDelegates.OnEntityCreated>]
    public void OnEntityCreated( IOnEntityCreatedEvent @event )
    {
        // @event.Entity.Entity.DesignerName = "abc";
        Console.WriteLine("TestPlugin OnEntityCreated222 " + @event.Entity.Entity?.DesignerName);
    }

    Guid _hookId = Guid.Empty;

    [Command("bad")]
    public void TestCommandBad( ICommandContext context )
    {
        try
        {
            var isValveDS = Core.EntitySystem.GetGameRules()!.IsValveDS;
        }
        catch (Exception e)
        {
            Core.Logger.LogWarning("{Exception}", e.Message);
        }

        try
        {
            Core.EntitySystem.GetGameRules()!.IsValveDS = true;
        }
        catch (Exception e)
        {
            Core.Logger.LogWarning("{Exception}", e.Message);
        }

        try
        {
            Core.EntitySystem.GetGameRules()!.IsValveDSUpdated();
        }
        catch (Exception e)
        {
            Core.Logger.LogWarning("{Exception}", e.Message);
        }
    }

    [Command("h2")]
    public void TestCommand3( ICommandContext context )
    {
        var ent = Core.EntitySystem.CreateEntity<CPointWorldText>();
        ent.DispatchSpawn();
        ent.Collision.MaxsUpdated();
        ent.Collision.CollisionAttribute.OwnerIdUpdated();
    }

    [Command("tt3")]
    public void TestCommand33( ICommandContext context )
    {
        var ent = Core.EntitySystem.CreateEntity<CPhysicsPropOverride>();
        using CEntityKeyValues kv = new();
        kv.Set<uint>("m_spawnflags", 256);
        ent.DispatchSpawn(kv);
        ent.SetModel("weapons/models/grenade/incendiary/weapon_incendiarygrenade.vmdl");
        ent.Teleport(new Vector(context.Sender!.PlayerPawn!.AbsOrigin!.Value.X + 50, context.Sender!.PlayerPawn!.AbsOrigin!.Value.Y + 50, context.Sender!.PlayerPawn!.AbsOrigin!.Value.Z + 30), QAngle.Zero, Vector.Zero);
    }

    [Command("tt4")]
    public void TestCommand4( ICommandContext context )
    {
        Console.WriteLine(Core.Permission.PlayerHasPermission(7656, context.Args[0]));
    }

    [Command("tt5")]
    public void TestCommand5( ICommandContext context )
    {
        Console.WriteLine("TestPlugin TestCommand5");
    }

    [Command("tt6", permission: "tt6")]
    public void TestCommand6( ICommandContext context )
    {
        Console.WriteLine("TestPlugin TestCommand6");
    }

    [Command("tt99")]
    public void TestCommand99( ICommandContext context )
    {
        Console.WriteLine(context.Sender!.SteamID);
        Console.WriteLine(context.Sender!.UnauthorizedSteamID);
    }

    [Command("tt7")]
    public void TestCommand7( ICommandContext context )
    {
        Core.Engine.ExecuteCommandWithBuffer("@ping", ( buffer ) =>
        {
            Console.WriteLine($"pong: {buffer}");
        });
    }

    [Command("tt8")]
    public unsafe void TestCommand8( ICommandContext context )
    {
        Core.EntitySystem.GetAllEntitiesByDesignerName<CBuyZone>("func_buyzone").ToList().ForEach(zone =>
        {
            if ((zone?.IsValid ?? false))
            {
                zone.Despawn();
            }
        });

        var sender = context.Sender!;
        var target = Core.PlayerManager.GetAllPlayers().FirstOrDefault(p => p.PlayerID != sender.PlayerID)!;

        var origin = sender.RequiredPlayerPawn.AbsOrigin ?? Vector.Zero;
        var targetOrigin = target.RequiredPlayerPawn.AbsOrigin ?? Vector.Zero;

        Console.WriteLine("\n");
        Console.WriteLine($"Origin: {origin}");
        Console.WriteLine($"Target Origin: {targetOrigin}");

        // Ray_t* ray = stackalloc Ray_t[1];
        // ray->Init(Vector.Zero, Vector.Zero);
        Ray_t ray = new();
        ray.Init(Vector.Zero, Vector.Zero);

        var filter = new CTraceFilter {
            // unk01 = 1,
            IterateEntities = true,
            QueryShapeAttributes = new RnQueryShapeAttr_t {
                InteractsWith = MaskTrace.Player | MaskTrace.Solid | MaskTrace.Hitbox | MaskTrace.Npc,
                InteractsExclude = MaskTrace.Empty,
                InteractsAs = MaskTrace.Player,
                CollisionGroup = CollisionGroup.PlayerMovement,
                ObjectSetMask = RnQueryObjectSet.AllGameEntities,
                HitSolid = true,
                // HitTrigger = false,
                // HitSolidRequiresGenerateContacts = false,
                // ShouldIgnoreDisabledPairs = true,
                // IgnoreIfBothInteractWithHitboxes = true,
                // ForceHitEverything = true
            }
        };

        // filter.QueryShapeAttributes.EntityIdsToIgnore[0] = unchecked((uint)-1);
        // filter.QueryShapeAttributes.EntityIdsToIgnore[1] = unchecked((uint)-1);
        // filter.QueryShapeAttributes.OwnerIdsToIgnore[0] = unchecked((uint)-1);
        // filter.QueryShapeAttributes.OwnerIdsToIgnore[1] = unchecked((uint)-1);
        // filter.QueryShapeAttributes.HierarchyIds[0] = 0;
        // filter.QueryShapeAttributes.HierarchyIds[1] = 0;

        var trace = new CGameTrace();
        Core.Trace.TraceShape(origin, targetOrigin, ray, filter, ref trace);

        Console.WriteLine(trace.pEntity != null ? $"! Hit Entity: {trace.Entity.DesignerName}" : "! No entity hit");
        Console.WriteLine($"! SurfaceProperties: {(nint)trace.SurfaceProperties}, pEntity: {(nint)trace.pEntity}, HitBox: {(nint)trace.HitBox}({trace.HitBox->m_name.Value}), Body: {(nint)trace.Body}, Shape: {(nint)trace.Shape}, Contents: {trace.Contents}");
        Console.WriteLine($"! StartPos: {trace.StartPos}, EndPos: {trace.EndPos}, HitNormal: {trace.HitNormal}, HitPoint: {trace.HitPoint}");
        Console.WriteLine($"! HitOffset: {trace.HitOffset}, Fraction: {trace.Fraction}, Triangle: {trace.Triangle}, HitboxBoneIndex: {trace.HitboxBoneIndex}");
        Console.WriteLine($"! RayType: {trace.RayType}, StartInSolid: {trace.StartInSolid}, ExactHitPoint: {trace.ExactHitPoint}");
        Console.WriteLine("\n");
    }

    [GameEventHandler(HookMode.Pre)]
    public HookResult TestGameEventHandler( EventPlayerJump @e )
    {
        Console.WriteLine(@e.UserIdController.PlayerName);
        return HookResult.Continue;
    }

    [ServerNetMessageHandler]
    public HookResult TestServerNetMessageHandler( CCSUsrMsg_SendPlayerItemDrops msg )
    {
        Console.WriteLine("FIRED");
        return HookResult.Continue;
    }

    private Callback<GCMessageAvailable_t> _authTicketResponse;

    [EventListener<EventDelegates.OnSteamAPIActivated>]
    public void OnSteamAPIActivated()
    {
        Console.WriteLine("TestPlugin OnSteamAPIActivated");
        _authTicketResponse = Callback<GCMessageAvailable_t>.Create(AuthResponse);
    }

    public void AuthResponse( GCMessageAvailable_t param )
    {
        Console.WriteLine($"AuthResponse {param.m_nMessageSize}");
    }

    [Command("getip")]
    public void GetIpCommand( ICommandContext context )
    {
        context.Reply(SteamGameServer.GetPublicIP().ToString());
    }

    // [Command("i76")]
    // public void TestIssue76Command( ICommandContext context )
    // {
    //     var player = context.Sender!;
    //     IMenu settingsMenu = Core.Menus.CreateMenu("Settings");
    //     // Add the following code to render text properly
    //     //settingsMenu.Builder.AddText("123", overflowStyle: MenuHorizontalStyle.ScrollLeftLoop(25f));
    //     settingsMenu.Builder.AddText("123");
    //     settingsMenu.Builder.AddText("1234");
    //     settingsMenu.Builder.AddText("12345");

    //     Core.Menus.OpenMenu(player, settingsMenu);
    // }

    // [Command("i77")]
    // public void TestIssue77Command( ICommandContext context )
    // {
    //     var player = context.Sender!;
    //     IMenu settingsMenu = Core.Menus.CreateMenu("Settings");

    //     settingsMenu.Builder.AddText("123");
    //     settingsMenu.Builder.AddSubmenu("Submenu", () =>
    //     {
    //         var menu = Core.Menus.CreateMenu("Submenu");
    //         menu.Builder.AddText("1234");
    //         return menu;
    //     });

    //     settingsMenu.Builder.AddSubmenu("Async Submenu", async () =>
    //     {
    //         await Task.Delay(5000);
    //         var menu = Core.Menus.CreateMenu("Async Submenu");
    //         menu.Builder.AddText("12345");
    //         return menu;
    //     });

    //     Core.Menus.OpenMenu(player, settingsMenu);
    // }

    // [Command("i78")]
    // public void TestIssue78Command( ICommandContext context )
    // {
    //     var player = context.Sender!;
    //     IMenu settingsMenu = Core.Menus.CreateMenu("Settings");
    //     settingsMenu.Builder.AddButton("123", ( p ) =>
    //     {
    //         player.SendMessage(MessageType.Chat, "Button");
    //     });

    //     settingsMenu.Builder.Design.OverrideExitButton("shift");
    //     settingsMenu.Builder.Design.OverrideSelectButton("e");

    //     Core.Menus.OpenMenu(player, settingsMenu);
    // }

    [Command("ed")]
    public void EmitGrenadeCommand( ICommandContext context )
    {
        var smoke = CSmokeGrenadeProjectile.EmitGrenade(new(0, 0, 0), new(0, 0, 0), new(0, 0, 0), Team.CT, null);
        smoke.Despawn();
        smoke.SetTransmitState(true, context.Sender!.PlayerID);
    }

    [Command("hbw")]
    public void HideBotWeapon( ICommandContext context )
    {
        Core.PlayerManager.GetAlive()
            .Where(player => player.PlayerID != context.Sender!.PlayerID && player.IsValid && player.IsFakeClient)
            .ToList()
            .ForEach(player => player.PlayerPawn!.WeaponServices!.ActiveWeapon.Value!.SetTransmitState(false, context.Sender!.PlayerID));
    }

    [Command("sihb")]
    public void ShowIfHideBot( ICommandContext context )
    {
        Core.PlayerManager.GetAlive()
            .Where(player => player.PlayerID != context.Sender!.PlayerID && player.IsValid && player.IsFakeClient)
            .ToList()
            .ForEach(player => Console.WriteLine($"{player.Controller!.PlayerName} -> {(!player.PlayerPawn!.IsTransmitting(context.Sender!.PlayerID) ? "Hide" : "V")}"));
    }

    [Command("hb")]
    public void HideBot( ICommandContext context )
    {
        Core.PlayerManager.GetAlive()
            .Where(player => player.PlayerID != context.Sender!.PlayerID && player.IsValid && player.IsFakeClient)
            .ToList()
            .ForEach(player =>
            {
                // Console.WriteLine($"{player.Controller!.PlayerName}(B) -> {player.PlayerPawn!.IsTransmitting(context.Sender!.PlayerID)}({player.PlayerPawn!.IsTransmitting(player.PlayerID)})");
                player.PlayerPawn!.SetTransmitState(!player.PlayerPawn!.IsTransmitting(context.Sender!.PlayerID), context.Sender!.PlayerID);
                // Console.WriteLine($"{player.Controller!.PlayerName} -> {player.PlayerPawn!.IsTransmitting(context.Sender!.PlayerID)}({player.PlayerPawn!.IsTransmitting(player.PlayerID)})");
            });
    }

    [Command("ss")]
    public void SwapScoresCommand( ICommandContext _ )
    {
        Core.PlayerManager.SendChat($"Before: {Core.Game.MatchData}");
        Core.Game.SwapTeamScores();
        Core.PlayerManager.SendChat($"After: {Core.Game.MatchData}");
    }

    [Command("sizecheck")]
    public void SizeCheckCommand( ICommandContext _ )
    {
        unsafe
        {
            var moveDataSize = sizeof(CMoveData);
            var moveDataBaseSize = sizeof(CMoveDataBase);
            var subtickMoveSize = sizeof(SubtickMove);
            var touchListSize = sizeof(TouchListT);

            Console.WriteLine($"CMoveData size: {moveDataSize} bytes");
            Console.WriteLine($"CMoveDataBase size: {moveDataBaseSize} bytes");
            Console.WriteLine($"SubtickMove size: {subtickMoveSize} bytes");
            Console.WriteLine($"TouchListT size: {touchListSize} bytes");
        }
    }

    [Command("tm")]
    public void TestMenuCommand( ICommandContext _ )
    {
        var buyButton = new ButtonMenuOption("Purchase") { CloseAfterClick = true };
        buyButton.Click += async ( sender, args ) =>
        {
            await Task.Delay(1000);

            if (sender is MenuOptionBase option)
            {
                var triggerOption = option!.Menu!.Parent.TriggerOption;
                triggerOption!.Enabled = false;
                args.Player.SendChat($"Purchase completed -> {triggerOption!.Text}");
                // option!.Menu!.Tag = ("Purchase", System.Diagnostics.Stopwatch.GetTimestamp());
            }
        };

        var confirmMenu = Core.MenusAPI
            .CreateBuilder()
            .Design.SetMenuTitle("Confirmation Menu")
            .AddOption(buyButton)
            .AddOption(new ButtonMenuOption("Cancel") { CloseAfterClick = true })
            .Build();

        var shopMenu = Core.MenusAPI
            .CreateBuilder()
            .Design.SetMenuTitle("Shop Menu")
            .AddOption(new SubmenuMenuOption("Item 1", async () =>
            {
                await Task.Delay(100);
                return confirmMenu;
            }))
            .AddOption(new SubmenuMenuOption("Item 2", async () =>
            {
                await Task.Delay(100);
                return confirmMenu;
            }))
            .AddOption(new SubmenuMenuOption("Item 3", async () =>
            {
                await Task.Delay(100);
                return confirmMenu;
            }))
            .AddOption(new SubmenuMenuOption("Item 4", async () =>
            {
                await Task.Delay(100);
                return confirmMenu;
            }))
            .Build();

        var mainMenu = Core.MenusAPI
            .CreateBuilder()
            .Design.SetMenuTitle("Menu")
            .AddOption(new SubmenuMenuOption("Shop", async () =>
            {
                await Task.Delay(100);
                return shopMenu;
            }))
            .Build();

        Core.MenusAPI.OpenMenu(mainMenu, ( player, menu ) =>
        {
            Console.WriteLine($"{menu.Configuration.Title} closed for player: {player.Controller.PlayerName}");
        });

        // Core.MenusAPI.OpenMenuForPlayer(context.Sender!, menu, ( player, menu ) =>
        // {
        //     Console.WriteLine($"{menu.Configuration.Title} closed for player: {player.Controller.PlayerName}");
        // });

        // Core.MenusAPI.MenuClosed += ( sender, args ) =>
        // {
        //     if (args.Menu?.Tag is (string, long))
        //     {
        //         Console.WriteLine($"Purchase completed -> {args.Menu.Tag}");
        //     }
        // };
    }

    [Command("rmt")]
    public void RefactoredMenuTestCommand( ICommandContext context )
    {
        var button = new ButtonMenuOption(HtmlGradient.GenerateGradientText("Swiftlys2 向这广袤世界致以温柔问候", "#FFE4E1", "#FFC0CB", "#FF69B4")) { TextStyle = MenuOptionTextStyle.ScrollLeftLoop/*, CloseAfterClick = true*/ };
        button.Click += ( sender, args ) =>
        {
            args.Player.SendMessage(MessageType.Chat, "Swiftlys2 向这广袤世界致以温柔问候");
            button.Enabled = false;
            _ = Task.Run(async () =>
            {
                await Task.Delay(1000);
                button.Enabled = true;
            });
            return ValueTask.CompletedTask;
        };

        var toggle = new ToggleMenuOption("12");
        toggle.ValueChanged += ( sender, args ) =>
        {
            args.Player.SendChat($"OldValue: {args.OldValue}({args.OldValue.GetType().Name}), NewValue: {args.NewValue}({args.NewValue.GetType().Name})");
        };

        var player = context.Sender!;
        var menu = Core.MenusAPI
            .CreateBuilder()
            .SetPlayerFrozen(false)
            .Design.SetMaxVisibleItems(5)
            .Design.SetMenuTitle($"{HtmlGradient.GenerateGradientText("Redesigned Menu", "#00FA9A", "#F5FFFA")}")
            .Design.SetMenuTitleVisible(true)
            .Design.SetMenuFooterVisible(true)
            .Design.SetMenuFooterColor("#0F0")
            .Design.SetNavigationMarkerColor("#F0F8FFFF")
            .Design.SetVisualGuideLineColor("#FFFFFF")
            .Design.SetDisabledColor("#808080")
            .Design.EnableAutoAdjustVisibleItems()
            .Design.SetGlobalScrollStyle(MenuOptionScrollStyle.WaitingCenter)
            .AddOption(new TextMenuOption("1") { Visible = false })
            .AddOption(toggle)
            .AddOption(new ChoiceMenuOption("123", ["Option 1", "Option 2", "Option 3"]))
            .AddOption(new SliderMenuOption("1234"))
            .AddOption(new ProgressBarMenuOption("12345", () => (float)new Random().NextDouble(), multiLine: false))
            .AddOption(new SubmenuMenuOption("123456", async () =>
            {
                await Task.Delay(1000);
                var menu = Core.MenusAPI.CreateBuilder()
                    .SetPlayerFrozen(true)
                    .Design.SetMenuTitle("Async Submenu")
                    .AddOption(new TextMenuOption("123456"))
                    .Build();
                return menu;
            }))
            .AddOption(new InputMenuOption("1234567"))
            .AddOption(new TextMenuOption() { Text = "12345678", TextStyle = MenuOptionTextStyle.ScrollLeftLoop })
            .AddOption(new TextMenuOption("123456789"))
            .AddOption(new TextMenuOption("1234567890") { Visible = false })
            .AddOption(button)
            .AddOption(new TextMenuOption(HtmlGradient.GenerateGradientText("Swiftlys2 からこの広大なる世界へ温かい挨拶を", "#FFE5CC", "#FFAB91", "#FF7043")) { TextStyle = MenuOptionTextStyle.ScrollRightLoop })
            .AddOption(new TextMenuOption(HtmlGradient.GenerateGradientText("Swiftlys2 가 이 넓은 세상에 따뜻한 인사를 전합니다", "#E6E6FA", "#00FFFF", "#FF1493")) { TextStyle = MenuOptionTextStyle.ScrollLeftFade })
            .AddOption(new TextMenuOption(HtmlGradient.GenerateGradientText("Swiftlys2 приветствует этот прекрасный мир", "#AFEEEE", "#7FFFD4", "#40E0D0")) { TextStyle = MenuOptionTextStyle.ScrollRightFade })
            .AddOption(new TextMenuOption("<font color='#F5FFFA'><b><invalid><font color='#00FA9A'>Swiftlys2</font> salută această lume minunată</invalid></b></font>") { TextStyle = MenuOptionTextStyle.TruncateEnd })
            .AddOption(new TextMenuOption("<font color='#00FA9A'><b><invalid><font color='#F5FFFA'>Swiftlys2</font> extends warmest greetings to this wondrous world</invalid></b></font>") { TextStyle = MenuOptionTextStyle.TruncateBothEnds })
            // .AddOption(new TextMenuOption("Swiftlys2 sendas korajn salutojn al ĉi tiu mirinda mondo"))
            .AddOption(new TextMenuOption("1234567890") { Visible = false })
            .AddOption(new TextMenuOption("123456789"))
            .AddOption(new TextMenuOption("12345678") { TextStyle = MenuOptionTextStyle.ScrollRightLoop })
            .AddOption(new InputMenuOption("1234567"))
            .AddOption(new SubmenuMenuOption("123456", () =>
            {
                var menu = Core.MenusAPI.CreateBuilder()
                    .Design.SetMenuTitle("Async Submenu")
                    .AddOption(new TextMenuOption("123456"))
                    .Build();
                return menu;
            }))
            .AddOption(new ProgressBarMenuOption("12345", () => (float)new Random().NextDouble(), multiLine: false))
            .AddOption(new SliderMenuOption("1234"))
            .AddOption(new ChoiceMenuOption("123", ["Option 1", "Option 2", "Option 3"]))
            .AddOption(new ToggleMenuOption("12", false, "O", "X"))
            .AddOption(new TextMenuOption("1") { Visible = false })
            .Build();

        Core.MenusAPI.OpenMenu(menu);
        // Core.MenusAPI.OpenMenuForPlayer(player, menu);
    }

    // [Command("mt")]
    // public void MenuTestCommand( ICommandContext context )
    // {
    //     var player = context.Sender!;

    //     IMenu settingsMenu = Core.Menus.CreateMenu("MenuTest");

    //     settingsMenu.Builder.Design.MaxVisibleItems(5);

    //     // settingsMenu.Builder.Design.MaxVisibleItems(Random.Shared.Next(-2, 8));
    //     if (context.Args.Length < 1 || !int.TryParse(context.Args[0], out int vtype)) vtype = 0;
    //     settingsMenu.Builder.Design.SetVerticalScrollStyle(vtype switch {
    //         1 => MenuVerticalScrollStyle.LinearScroll,
    //         2 => MenuVerticalScrollStyle.WaitingCenter,
    //         _ => MenuVerticalScrollStyle.CenterFixed
    //     });

    //     if (context.Args.Length < 2 || !int.TryParse(context.Args[1], out int htype)) htype = 0;
    //     settingsMenu.Builder.Design.SetGlobalHorizontalStyle(htype switch {
    //         0 => MenuHorizontalStyle.Default,
    //         1 => MenuHorizontalStyle.TruncateBothEnds(26f),
    //         2 => MenuHorizontalStyle.ScrollLeftFade(26f, 8, 128),
    //         3 => MenuHorizontalStyle.ScrollLeftLoop(26f, 8, 128),
    //         1337 => MenuHorizontalStyle.TruncateEnd(0f),
    //         _ => MenuHorizontalStyle.TruncateEnd(26f)
    //     });

    //     settingsMenu.Builder.AddButton("1. AButton", ( p ) =>
    //     {
    //         player.SendMessage(MessageType.Chat, "Button");
    //     });

    //     settingsMenu.Builder.AddToggle("2. Toggle", defaultValue: true, ( p, value ) =>
    //     {
    //         player.SendMessage(MessageType.Chat, $"AddToggle {value}");
    //     });

    //     settingsMenu.Builder.AddSlider("3. Slider", min: 0, max: 100, defaultValue: 10, step: 10, ( p, value ) =>
    //     {
    //         player.SendMessage(MessageType.Chat, $"AddSlider {value}");
    //     });

    //     settingsMenu.Builder.AddAsyncButton("4. AsyncButton", async ( p ) =>
    //     {
    //         await Task.Delay(2000);
    //     });

    //     settingsMenu.Builder.AddText("5. Text");
    //     settingsMenu.Builder.AddText("6. Text");
    //     settingsMenu.Builder.AddText("7. Text");
    //     settingsMenu.Builder.AddText("8. Text");
    //     settingsMenu.Builder.AddText("9. Text");
    //     settingsMenu.Builder.AddSeparator();
    //     settingsMenu.Builder.AddText($"<b>{HtmlGradient.GenerateGradientText("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", "#FFE4E1", "#FFC0CB", "#FF69B4")}</b>", overflowStyle: MenuHorizontalStyle.TruncateEnd(26f));
    //     settingsMenu.Builder.AddText($"<b>{HtmlGradient.GenerateGradientText("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", "#FFE5CC", "#FFAB91", "#FF7043")}</b>", overflowStyle: MenuHorizontalStyle.TruncateBothEnds(26f));
    //     settingsMenu.Builder.AddText($"<b>{HtmlGradient.GenerateGradientText("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", "#E6E6FA", "#00FFFF", "#FF1493")}</b>", overflowStyle: MenuHorizontalStyle.ScrollRightFade(26f, 8));
    //     settingsMenu.Builder.AddText($"<b>{HtmlGradient.GenerateGradientText("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", "#AFEEEE", "#7FFFD4", "#40E0D0")}</b>", overflowStyle: MenuHorizontalStyle.ScrollLeftLoop(26f, 8));
    //     settingsMenu.Builder.AddText("<font color='#F5FFFA'><b><garbage>12345678901234567890<font color='#00FA9A'>split</font>12345678901234567890</garbage></b></font>", overflowStyle: MenuHorizontalStyle.ScrollLeftFade(26f, 8, 128));
    //     settingsMenu.Builder.AddText("<font color='#00FA9A'><b><garbage>一二三四五六七八九十<font color='#F5FFFA'>分割</font>一二三四五六七八九十</garbage></b></font>", overflowStyle: MenuHorizontalStyle.ScrollRightLoop(26f, 8, 64));
    //     settingsMenu.Builder.AddSeparator();
    //     settingsMenu.Builder.AddText("Swiftlys2 向这广袤世界致以温柔问候", overflowStyle: MenuHorizontalStyle.ScrollRightLoop(26f, 8));
    //     settingsMenu.Builder.AddText("Swiftlys2 からこの広大なる世界へ温かい挨拶を");
    //     settingsMenu.Builder.AddText("Swiftlys2 가 이 넓은 세상에 따뜻한 인사를 전합니다");
    //     settingsMenu.Builder.AddText("Swiftlys2 приветствует этот прекрасный мир");
    //     settingsMenu.Builder.AddText("Swiftlys2 salută această lume minunată");
    //     settingsMenu.Builder.AddText("Swiftlys2 extends warmest greetings to this wondrous world");
    //     settingsMenu.Builder.AddText("Swiftlys2 sendas korajn salutojn al ĉi tiu mirinda mondo");
    //     settingsMenu.Builder.AddSeparator();
    //     settingsMenu.Builder.AddAsyncButton("AsyncButton|AsyncButton|AsyncButton", async ( p ) => await Task.Delay(2000));
    //     settingsMenu.Builder.AddButton("Button|Button|Button|Button", ( p ) => { });
    //     settingsMenu.Builder.AddChoice("Choice|Choice|Choice|Choice", ["Option 1", "Option 2", "Option 3"], "Option 1", ( p, value ) => { }, overflowStyle: MenuHorizontalStyle.TruncateEnd(8f));
    //     settingsMenu.Builder.AddProgressBar("ProgressBar|ProgressBar|ProgressBar", () => (float)Random.Shared.NextDouble(), overflowStyle: MenuHorizontalStyle.ScrollLeftLoop(26f, 12));
    //     settingsMenu.Builder.AddSlider("Slider|Slider|Slider|Slider", 0f, 100f, 0f, 1f, ( p, value ) => { }, overflowStyle: MenuHorizontalStyle.ScrollRightLoop(8f, 12));
    //     // settingsMenu.Builder.AddSubmenu("Submenu");
    //     settingsMenu.Builder.AddToggle("Toggle|Toggle|Toggle|Toggle", true, ( p, value ) => { });
    //     settingsMenu.Builder.AddSeparator();

    //     Core.Menus.OpenMenu(player, settingsMenu);
    // }

    // [Command("menu")]
    // public void MenuCommand( ICommandContext context )
    // {
    //     var player = context.Sender!;
    //     var menu = Core.Menus.CreateMenu("Test Menu");

    //     menu.Builder
    //         .AddButton("Button 1", ( ctx ) =>
    //         {
    //             player.SendMessage(MessageType.Chat, "You clicked Button 1");
    //         })
    //         .AddButton("Button 2", ( ctx ) =>
    //         {
    //             player.SendMessage(MessageType.Chat, "You clicked Button 2");
    //         })
    //         .AddButton("Button 3", ( ctx ) =>
    //         {
    //             player.SendMessage(MessageType.Chat, "You clicked Button 3");
    //         })
    //         .AddButton("Button 4", ( ctx ) =>
    //         {
    //             player.SendMessage(MessageType.Chat, "You clicked Button 4");
    //         })
    //         .AddButton("Button 5", ( ctx ) =>
    //         {
    //             player.SendMessage(MessageType.Chat, "You clicked Button 5");
    //         })
    //         .AddButton("Button 6", ( ctx ) =>
    //         {
    //             player.SendMessage(MessageType.Chat, "You clicked Button 6");
    //         })
    //         .AddButton("Button 7", ( ctx ) =>
    //         {
    //             player.SendMessage(MessageType.Chat, "You clicked Button 7");
    //         })
    //         .AddButton("Button 8", ( ctx ) =>
    //         {
    //             player.SendMessage(MessageType.Chat, "You clicked Button 8");
    //         })
    //         .AddSeparator()
    //         .AddText("hello!", size: IMenuTextSize.ExtraLarge)
    //         .AutoClose(15f)
    //         .HasSound(true)
    //         .ForceFreeze();

    //     menu.Builder.Design.SetColor(new(0, 186, 105, 255));

    //     Core.Menus.OpenMenu(player, menu);
    // }

    [Command("mru")]
    public void MenuResourceUsageCommand( ICommandContext context )
    {
        var menus = new List<IMenuAPI>();

        for (var i = 0; i < 30; i++)
        {
            var builder = Core.MenusAPI
                .CreateBuilder()
                .Design.SetMenuTitle($"Test Menu {i + 1}");

            for (var j = 0; j < 5; j++)
            {
                var optionText = $"Menu # {i + 1} - Option # {j + 1}";
                var button = new ButtonMenuOption(optionText) { TextStyle = MenuOptionTextStyle.ScrollLeftLoop, MaxWidth = 16f };
                button.Click += ( sender, args ) =>
                {
                    args.Player.SendChat($"Clicked: {optionText}");
                    return ValueTask.CompletedTask;
                };
                _ = builder.AddOption(button);
            }

            menus.Add(builder.Build());
        }

        var mainMenu = Core.MenusAPI
            .CreateBuilder()
            .Design.SetMenuTitle("Menu");

        for (var i = 0; i < menus.Count; i++)
        {
            var menuIndex = i;
            _ = mainMenu.AddOption(new SubmenuMenuOption($"Menu #{i + 1}", menus[menuIndex]));
        }

        Core.MenusAPI.OpenMenuForPlayer(context.Sender!, mainMenu.Build());
    }

    [Command("tb2m")]
    public void TeleportBotToMeCommand( ICommandContext context )
    {
        var player = context.Sender!;
        Core.PlayerManager.GetAllPlayers()
            .Where(p => p.IsValid && p.IsFakeClient)
            .ToList()
            .FirstOrDefault()
            ?.Teleport(player.PlayerPawn!.AbsOrigin!.Value, player.PlayerPawn!.EyeAngles, Vector.Zero);
    }

    [Command("los")]
    public void LineOfSightCommand( ICommandContext context )
    {
        var player = context.Sender!;
        Core.PlayerManager.GetAlive()
            .Where(p => p.PlayerID != player.PlayerID)
            .ToList()
            .ForEach(targetPlayer => context.Reply($"Line of sight to {targetPlayer.Controller!.PlayerName}: {player.PlayerPawn!.HasLineOfSight(targetPlayer.PlayerPawn!)}"));
    }

    public override void Unload()
    {
        Console.WriteLine("TestPlugin unloaded");
    }
}