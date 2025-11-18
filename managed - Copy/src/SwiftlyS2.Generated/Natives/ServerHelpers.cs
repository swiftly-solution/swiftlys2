#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeServerHelpers {
  private static int _MainThreadID;

  private unsafe static delegate* unmanaged<byte*, int> _GetServerLanguage;

  public unsafe static string GetServerLanguage() {
    var ret = _GetServerLanguage(null);
    var pool = ArrayPool<byte>.Shared;
    var retBuffer = pool.Rent(ret + 1);
    fixed (byte* retBufferPtr = retBuffer) {
      ret = _GetServerLanguage(retBufferPtr);
      var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
      pool.Return(retBuffer);
      return retString;
    }
  }

  private unsafe static delegate* unmanaged<byte> _UsePlayerLanguage;

  public unsafe static bool UsePlayerLanguage() {
    var ret = _UsePlayerLanguage();
    return ret == 1;
  }

  private unsafe static delegate* unmanaged<byte> _IsFollowingServerGuidelines;

  public unsafe static bool IsFollowingServerGuidelines() {
    var ret = _IsFollowingServerGuidelines();
    return ret == 1;
  }

  private unsafe static delegate* unmanaged<byte> _UseAutoHotReload;

  public unsafe static bool UseAutoHotReload() {
    var ret = _UseAutoHotReload();
    return ret == 1;
  }
}