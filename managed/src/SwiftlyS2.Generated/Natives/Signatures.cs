#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeSignatures
{

    private unsafe static delegate* unmanaged<byte*, byte> _Exists;

    public unsafe static bool Exists(string signatureName)
    {
        using var signatureNameStr = new ScopedCString(signatureName);
        fixed (byte* signatureNameBufferPtr = signatureNameStr)
        {
            var ret = _Exists(signatureNameBufferPtr);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<byte*, nint> _Fetch;

    public unsafe static nint Fetch(string signatureName)
    {
        using var signatureNameStr = new ScopedCString(signatureName);
        fixed (byte* signatureNameBufferPtr = signatureNameStr)
        {
            var ret = _Fetch(signatureNameBufferPtr);
            return ret;
        }
    }
}