using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.SteamAPI;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SteamAPIWarningMessageHook_t( int nSeverity, System.Text.StringBuilder pchDebugText );


