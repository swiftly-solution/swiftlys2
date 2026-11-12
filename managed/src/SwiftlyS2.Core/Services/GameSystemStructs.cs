using System.Runtime.InteropServices;

namespace SwiftlyS2.Core.Services;

[StructLayout(LayoutKind.Sequential)]
internal struct EventBuildGameSessionManifest_t
{
    public nint m_pResourceManifest;
}
