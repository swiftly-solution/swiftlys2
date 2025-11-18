#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeTest {
  private static int _MainThreadID;

  private unsafe static delegate* unmanaged<nint> _Test;

  public unsafe static nint Test() {
    var ret = _Test();
    return ret;
  }
}