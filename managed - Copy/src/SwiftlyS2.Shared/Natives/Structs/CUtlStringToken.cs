using System;
using System.Runtime.InteropServices;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public partial struct CUtlStringToken
{
    [LibraryImport("tier0", SetLastError = true, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(System.Runtime.InteropServices.Marshalling.AnsiStringMarshaller))]
    static partial void RegisterStringToken(uint nHashCode, string pStart, string? pEnd = null, [MarshalAs(UnmanagedType.Bool)] bool bExtraAddToDatabase = true);

    public uint HashCode;

    public CUtlStringToken(string str)
    {
        if (str != null)
        {
            HashCode = MurmurHash2.HashString(str, 0x31415926);
            if (ExternDLL.GetExportedVariable<bool>("tier0", "g_bUpdateStringTokenDatabase"))
            {
                RegisterStringToken(HashCode, str);
            }
        }
    }
}