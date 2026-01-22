using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct CUtlSlot
{
    public CCopyableLock<CThreadSpinMutex> Mutex;
    public CUtlVector<nint> ConnectedSignalers;
}