using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Services;

namespace SwiftlyS2.Core.Services;

internal class TraceManager : ITraceManager
{
    public void TracePlayerBBox(Vector start, Vector end, BBox_t bounds, CTraceFilter filter, ref CGameTrace trace)
    {
        unsafe
        {
            fixed(CGameTrace* tracePtr = &trace)
                GameFunctions.TracePlayerBBox(start, end, bounds, &filter, tracePtr);
        }
    }

    public void TraceShape(Vector start, Vector end, Ray_t ray, CTraceFilter filter, ref CGameTrace trace)
    {
        unsafe
        {
            fixed (CGameTrace* tracePtr = &trace)
                GameFunctions.TraceShape(NativeEngineHelpers.GetTraceManager(), &ray, start, end, &filter, tracePtr);
        }
    }
}