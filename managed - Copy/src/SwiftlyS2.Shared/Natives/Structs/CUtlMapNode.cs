using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct CUtlMapTreeNode<TKey, TValue>
{
    public TKey Key;
    public TValue Element;
}