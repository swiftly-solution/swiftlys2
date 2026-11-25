using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Profiler;

namespace SwiftlyS2.Core.Profiler;

internal class ContextedProfilerService : IContextedProfilerService
{

    private string _Name { get; init; }

    public ContextedProfilerService( CoreContext context )
    {
        _Name = context.Name;
    }

    public void StartRecording( string name )
    {
        NativeTracer.CustomBegin(name, _Name);
    }

    public void StopRecording( string name )
    {
        NativeTracer.CustomEnd(name);
    }
}
