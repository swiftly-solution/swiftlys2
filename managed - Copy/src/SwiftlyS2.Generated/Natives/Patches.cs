#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativePatches {
  private static int _MainThreadID;

  private unsafe static delegate* unmanaged<byte*, void> _Apply;

  public unsafe static void Apply(string patchName) {
    var pool = ArrayPool<byte>.Shared;
    var patchNameLength = Encoding.UTF8.GetByteCount(patchName);
    var patchNameBuffer = pool.Rent(patchNameLength + 1);
    Encoding.UTF8.GetBytes(patchName, patchNameBuffer);
    patchNameBuffer[patchNameLength] = 0;
    fixed (byte* patchNameBufferPtr = patchNameBuffer) {
      _Apply(patchNameBufferPtr);
      pool.Return(patchNameBuffer);
    }
  }

  private unsafe static delegate* unmanaged<byte*, void> _Revert;

  public unsafe static void Revert(string patchName) {
    var pool = ArrayPool<byte>.Shared;
    var patchNameLength = Encoding.UTF8.GetByteCount(patchName);
    var patchNameBuffer = pool.Rent(patchNameLength + 1);
    Encoding.UTF8.GetBytes(patchName, patchNameBuffer);
    patchNameBuffer[patchNameLength] = 0;
    fixed (byte* patchNameBufferPtr = patchNameBuffer) {
      _Revert(patchNameBufferPtr);
      pool.Return(patchNameBuffer);
    }
  }

  private unsafe static delegate* unmanaged<byte*, byte> _Exists;

  public unsafe static bool Exists(string patchName) {
    var pool = ArrayPool<byte>.Shared;
    var patchNameLength = Encoding.UTF8.GetByteCount(patchName);
    var patchNameBuffer = pool.Rent(patchNameLength + 1);
    Encoding.UTF8.GetBytes(patchName, patchNameBuffer);
    patchNameBuffer[patchNameLength] = 0;
    fixed (byte* patchNameBufferPtr = patchNameBuffer) {
      var ret = _Exists(patchNameBufferPtr);
      pool.Return(patchNameBuffer);
      return ret == 1;
    }
  }
}