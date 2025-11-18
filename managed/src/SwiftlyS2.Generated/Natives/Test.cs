#pragma warning disable CS0649
#pragma warning disable CS0169

namespace SwiftlyS2.Core.Natives;

internal static class NativeTest
{
    private static readonly unsafe delegate* unmanaged< nint > _Test;

    public unsafe static nint Test()
    {
        var ret = _Test();
        return ret;
    }
}