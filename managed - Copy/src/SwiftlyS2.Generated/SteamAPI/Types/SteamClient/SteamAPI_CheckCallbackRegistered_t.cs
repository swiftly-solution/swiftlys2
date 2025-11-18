using System.Runtime.InteropServices;
using IntPtr = System.IntPtr;

namespace SwiftlyS2.Shared.SteamAPI
{
	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)] // TODO: This is probably wrong, will likely crash on some platform.
	public delegate void SteamAPI_CheckCallbackRegistered_t( int iCallbackNum );
}


