using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Extensions;

namespace SwiftlyS2.Shared.Natives;

/// <summary>
/// Wrapper class for native char*.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 0x8)]
public struct CString
{
    [FieldOffset(0x0)]
    private nint pString; // const char*

    public string Value {
        readonly get {
            return !pString.IsValidPtr() ? string.Empty : Marshal.PtrToStringUTF8(pString)!;
        }
        set {
            pString = StringPool.Allocate(value);
        }
    }

    public static implicit operator string( CString str ) => str.Value;
    public static implicit operator CString( string str ) => new() { pString = StringPool.Allocate(str) };
    public override readonly string ToString() => Value;
}
