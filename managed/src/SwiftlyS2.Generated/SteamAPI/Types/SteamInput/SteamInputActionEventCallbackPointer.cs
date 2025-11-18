using System.Runtime.InteropServices;
using IntPtr = nint;

namespace SwiftlyS2.Shared.SteamAPI;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SteamInputActionEventCallbackPointer( IntPtr /* SteamInputActionEvent_t* */ SteamInputActionEvent );


