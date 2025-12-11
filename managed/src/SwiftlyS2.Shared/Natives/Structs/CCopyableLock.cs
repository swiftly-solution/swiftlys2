using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct CCopyableLock<T> where T : unmanaged
{
    public T Mutex;
}