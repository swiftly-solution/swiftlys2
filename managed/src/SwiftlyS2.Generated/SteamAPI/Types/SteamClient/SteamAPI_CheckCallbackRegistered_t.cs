using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.SteamAPI;

[UnmanagedFunctionPointer(CallingConvention.StdCall)] // TODO: This is probably wrong, will likely crash on some platform.
public delegate void SteamAPI_CheckCallbackRegistered_t( int iCallbackNum );


