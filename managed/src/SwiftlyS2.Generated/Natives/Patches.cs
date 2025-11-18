#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;

namespace SwiftlyS2.Core.Natives;

internal static class NativePatches
{
    private static readonly int _MainThreadID;

    private static readonly unsafe delegate* unmanaged< byte*, void > _Apply;

    public unsafe static void Apply( string patchName )
    {
        var pool = ArrayPool<byte>.Shared;
        var patchNameLength = Encoding.UTF8.GetByteCount(patchName);
        var patchNameBuffer = pool.Rent(patchNameLength + 1);
        _ = Encoding.UTF8.GetBytes(patchName, patchNameBuffer);
        patchNameBuffer[patchNameLength] = 0;
        fixed (byte* patchNameBufferPtr = patchNameBuffer)
        {
            _Apply(patchNameBufferPtr);
            pool.Return(patchNameBuffer);
        }
    }

    private static readonly unsafe delegate* unmanaged< byte*, void > _Revert;

    public unsafe static void Revert( string patchName )
    {
        var pool = ArrayPool<byte>.Shared;
        var patchNameLength = Encoding.UTF8.GetByteCount(patchName);
        var patchNameBuffer = pool.Rent(patchNameLength + 1);
        _ = Encoding.UTF8.GetBytes(patchName, patchNameBuffer);
        patchNameBuffer[patchNameLength] = 0;
        fixed (byte* patchNameBufferPtr = patchNameBuffer)
        {
            _Revert(patchNameBufferPtr);
            pool.Return(patchNameBuffer);
        }
    }

    private static readonly unsafe delegate* unmanaged< byte*, byte > _Exists;

    public unsafe static bool Exists( string patchName )
    {
        var pool = ArrayPool<byte>.Shared;
        var patchNameLength = Encoding.UTF8.GetByteCount(patchName);
        var patchNameBuffer = pool.Rent(patchNameLength + 1);
        _ = Encoding.UTF8.GetBytes(patchName, patchNameBuffer);
        patchNameBuffer[patchNameLength] = 0;
        fixed (byte* patchNameBufferPtr = patchNameBuffer)
        {
            var ret = _Exists(patchNameBufferPtr);
            pool.Return(patchNameBuffer);
            return ret == 1;
        }
    }
}