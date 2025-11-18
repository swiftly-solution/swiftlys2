using System.Runtime.InteropServices;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Shared.Natives;

/// <summary>
/// Wrapper class for native char*.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct CString
{

    [FieldOffset(0)]
    private nint _pString; // char*

    public string Value {
        get {
            return !_pString.IsValidPtr() ? string.Empty : Marshal.PtrToStringUTF8(_pString)!;
        }

        set {
            _pString = StringPool.Allocate(value);
        }
    }

    public static implicit operator string( CString str ) => str.Value;
    public static implicit operator CString( string str ) => new() { _pString = StringPool.Allocate(str) };
}
