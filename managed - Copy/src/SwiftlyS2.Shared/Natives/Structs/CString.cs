using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Shared.Natives;

/// <summary>
/// Wrapper class for native char*.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct CString {

  [FieldOffset(0)]
  private nint _pString; // char*
  
  public string Value {
    get {
      if (!_pString.IsValidPtr()) {
        return string.Empty;
      }
      return Marshal.PtrToStringUTF8(_pString)!;
    }

    set
    {
      _pString = StringPool.Allocate(value);
    }
  }

  public static implicit operator string(CString str) => str.Value;
  public static implicit operator CString(string str) => new CString { _pString = StringPool.Allocate(str) };
}
