using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Shared.SteamAPI;

[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.Cdecl)]
public delegate void SteamInputActionEventCallbackPointer(IntPtr /* SteamInputActionEvent_t* */ SteamInputActionEvent);

