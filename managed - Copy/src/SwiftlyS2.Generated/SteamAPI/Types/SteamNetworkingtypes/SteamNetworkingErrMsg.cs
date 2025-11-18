using System.Runtime.InteropServices;
using IntPtr = System.IntPtr;

namespace SwiftlyS2.Shared.SteamAPI
{
	/// Used to return English-language diagnostic error messages to caller.
	/// (For debugging or spewing to a console, etc.  Not intended for UI.)
	[System.Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct SteamNetworkingErrMsg
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.k_cchMaxSteamNetworkingErrMsg)]
		public byte[] m_SteamNetworkingErrMsg;
	}
}

