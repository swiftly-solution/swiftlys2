using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.EntitySystem;
using SwiftlyS2.Shared.Profiler;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Core.NetMessages;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate int EntityOutputHookCallbackDelegate( nint entityio, nint outputName, nint activator, nint caller, float delay );

internal class EntityOutputHookCallback : IDisposable
{
    public Guid Guid { get; init; }
    public IContextedProfilerService Profiler { get; }

    private IEntitySystemService.EntityOutputHandler _callback;
    private ILogger<EntityOutputHookCallback> _logger;
    private EntityOutputHookCallbackDelegate _unmanagedCallback;
    private nint _unmanagedCallbackPtr;
    private ulong _nativeHookId;

    public EntityOutputHookCallback( string className, string outputName, IEntitySystemService.EntityOutputHandler callback, ILoggerFactory loggerFactory, IContextedProfilerService profiler )
    {
        Guid = Guid.NewGuid();
        Profiler = profiler;
        _logger = loggerFactory.CreateLogger<EntityOutputHookCallback>();
        _callback = callback;
        _unmanagedCallback = ( entityio, outputName, activator, caller, delay ) =>
        {
            try
            {
                var category = "EntityOutputHookCallback::" + outputName;
                Profiler.StartRecording(category);
                var outputStr = Marshal.PtrToStringAnsi(outputName) ?? string.Empty;
                var result = _callback(
                    new CEntityIOOutputImpl(entityio),
                    Marshal.PtrToStringAnsi(outputName) ?? string.Empty,
                    new CEntityInstanceImpl(activator),
                    new CEntityInstanceImpl(caller),
                    delay
                );
                Profiler.StopRecording(category);
                return (int)result;
            }
            catch (Exception e)
            {
                if (!GlobalExceptionHandler.Handle(e)) return 0;
                _logger.LogError(e, "Failed to execute entity output callback {0}.", Guid);
            }
            return 0;
        };

        _unmanagedCallbackPtr = Marshal.GetFunctionPointerForDelegate(_unmanagedCallback);
        _nativeHookId = NativeEntitySystem.HookEntityOutput(className, outputName, _unmanagedCallbackPtr);
    }

    public void Dispose()
    {
        NativeEntitySystem.UnhookEntityOutput(_nativeHookId);
    }
}