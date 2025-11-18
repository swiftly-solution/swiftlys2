using System.Runtime.InteropServices;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential, Size = 8)]
public struct CGlobalSymbol
{

    private nint _pString;

    public string Value {
        get {
            return !_pString.IsValidPtr() ? string.Empty : Marshal.PtrToStringUTF8(_pString)!;
        }
        set {
            _pString = StringPool.Allocate(value);
        }
    }
}
