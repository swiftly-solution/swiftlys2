using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

public struct CUtlRBTreeLinks<TKey> where TKey : unmanaged
{
    public TKey Left;
    public TKey Right;
    public TKey Parent;
    public TKey Tag;
}

[StructLayout(LayoutKind.Sequential)]
public struct CUtlRBTreeNode<TKey, TValue>
    where TKey : unmanaged
{
    public CUtlRBTreeLinks<TKey> Links;
    public TValue Data;
}