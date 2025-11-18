using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Extensions;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential, Size = 8)]
public struct CUtlString {

  private nint _ptr;

  public string Value {

    get {
      if (!_ptr.IsValidPtr()) return string.Empty;
      return Marshal.PtrToStringUTF8(_ptr)!;
    }
    set => _ptr = StringPool.Allocate(value);
  }

  public static implicit operator string(CUtlString str) => str.Value;
  public static implicit operator CUtlString(string str) => new() { _ptr = StringPool.Allocate(str) };
}