using System.Text;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Core.Natives;

internal class StringPool
{

  private static Dictionary<string, nint> stringToAddr = new();
  private static Lock _lock = new();

  public static nint Allocate( string str )
  {
    lock (_lock)
    {
      if (stringToAddr.TryGetValue(str, out var addr))
      {
        return addr;
      }

      var length = Encoding.UTF8.GetByteCount(str);
      addr = NativeAllocator.Alloc((ulong)(length + 1));
      stringToAddr[str] = addr;
      unsafe
      {
        Span<byte> bytes = new(addr.ToPointer(), length + 1);
        Encoding.UTF8.GetBytes(str, bytes);
        bytes[length] = 0;
      }
      return addr;
    }
  }
}