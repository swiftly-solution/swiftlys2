using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Profiler;
using SwiftlyS2.Shared.EntitySystem;
using SwiftlyS2.Core.SchemaDefinitions;

namespace SwiftlyS2.Core.NetMessages;

[Obsolete("Use HookEntityOutput with EntityOutputEventHandler instead.")]
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate int EntityOutputHookCallbackDelegate( nint entityio, nint outputName, nint activator, nint caller, float delay );

[Obsolete("Use HookEntityOutput with EntityOutputEventHandler instead.")]
internal class EntityOutputHookCallback : IDisposable
{
    public Guid Guid { get; init; }

    private readonly ILogger<EntityOutputHookCallback> logger;
    private readonly EntityOutputHookCallbackDelegate unmanagedCallback;
    private readonly nint unmanagedCallbackPtr;
    private readonly ulong nativeHookId;

    private volatile bool disposed;

    public EntityOutputHookCallback( string className, string outputName, IEntitySystemService.EntityOutputHandler callback, ILoggerFactory loggerFactory, IContextedProfilerService profiler )
    {
        this.Guid = Guid.NewGuid();
        this.logger = loggerFactory.CreateLogger<EntityOutputHookCallback>();
        this.disposed = false;

        unmanagedCallback = ( entityio, outputName, activator, caller, delay ) =>
        {
            try
            {
                var category = "EntityOutputHookCallback::" + outputName;
                profiler.StartRecording(category);
                var outputStr = Marshal.PtrToStringAnsi(outputName) ?? string.Empty;
                HookResult result;
                unsafe
                {
                    result = callback(
                        Unsafe.AsRef<CEntityIOOutput>((void*)entityio),
                        Marshal.PtrToStringAnsi(outputName) ?? string.Empty,
                        new CEntityInstanceImpl(activator),
                        new CEntityInstanceImpl(caller),
                        delay
                    );
                }
                profiler.StopRecording(category);
                return (int)result;
            }
            catch (Exception e)
            {
                if (!GlobalExceptionHandler.Handle(e))
                {
                    return 0;
                }
                logger.LogError(e, "Failed to execute entity output callback {0}.", Guid);
            }
            return 0;
        };

        unmanagedCallbackPtr = Marshal.GetFunctionPointerForDelegate(unmanagedCallback);
        nativeHookId = NativeEntitySystem.HookEntityOutput(className, outputName, unmanagedCallbackPtr);
    }

    ~EntityOutputHookCallback()
    {
        Dispose();
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }
        disposed = true;

        NativeEntitySystem.UnhookEntityOutput(nativeHookId);

        GC.SuppressFinalize(this);
    }
}