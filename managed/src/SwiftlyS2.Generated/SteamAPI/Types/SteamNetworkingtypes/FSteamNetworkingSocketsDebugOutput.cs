using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.SteamAPI;

/// Setup callback for debug output, and the desired verbosity you want.
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void FSteamNetworkingSocketsDebugOutput( ESteamNetworkingSocketsDebugOutputType nType, System.Text.StringBuilder pszMsg );


