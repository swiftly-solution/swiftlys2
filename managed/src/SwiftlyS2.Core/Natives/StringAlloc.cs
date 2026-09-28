using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;

namespace SwiftlyS2.Core.Natives;

internal static class StringAlloc
{
    public static string CreateCSharpString( nint cstrPtr, int length )
    {
        if (cstrPtr == 0 || length == 0) return "";

        return Marshal.PtrToStringUTF8(cstrPtr, length);
    }

    public static string CreateCSharpString( nint cstrPtr )
    {
        if (cstrPtr == 0) return "";
        return Marshal.PtrToStringUTF8(cstrPtr) ?? "(null)";
    }
}