using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Shared.SteamAPI;

/// <summary>
/// <para>Interface used to send signaling messages for a particular connection.</para>
/// <para>- For connections initiated locally, you will construct it and pass</para>
/// <para>it to ISteamNetworkingSockets::ConnectP2PCustomSignaling.</para>
/// <para>- For connections initiated remotely and "accepted" locally, you</para>
/// <para>will return it from ISteamNetworkingSignalingRecvContext::OnConnectRequest</para>
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public struct ISteamNetworkingConnectionSignaling
{
	/// <summary>
	/// <para>Called to send a rendezvous message to the remote peer.  This may be called</para>
	/// <para>from any thread, at any time, so you need to be thread-safe!  Don't take</para>
	/// <para>any locks that might hold while calling into SteamNetworkingSockets functions,</para>
	/// <para>because this could lead to deadlocks.</para>
	/// <para>Note that when initiating a connection, we may not know the identity</para>
	/// <para>of the peer, if you did not specify it in ConnectP2PCustomSignaling.</para>
	/// <para>Return true if a best-effort attempt was made to deliver the message.</para>
	/// <para>If you return false, it is assumed that the situation is fatal;</para>
	/// <para>the connection will be closed, and Release() will be called</para>
	/// <para>eventually.</para>
	/// <para>Signaling objects will not be shared between connections.</para>
	/// <para>You can assume that the same value of hConn will be used</para>
	/// <para>every time.</para>
	/// </summary>
	public bool SendSignal(HSteamNetConnection hConn, ref SteamNetConnectionInfo_t info, IntPtr pMsg, int cbMsg)
	{
		return NativeMethods.SteamAPI_ISteamNetworkingConnectionSignaling_SendSignal(ref this, hConn, ref info, pMsg, cbMsg);
	}

	/// <summary>
	/// <para>Called when the connection no longer needs to send signals.</para>
	/// <para>Note that this happens eventually (but not immediately) after</para>
	/// <para>the connection is closed.  Signals may need to be sent for a brief</para>
	/// <para>time after the connection is closed, to clean up the connection.</para>
	/// <para>If you do not need to save any additional per-connection information</para>
	/// <para>and can handle SendSignal() using only the arguments supplied, you do</para>
	/// <para>not need to actually create different objects per connection.  In that</para>
	/// <para>case, it is valid for all connections to use the same global object, and</para>
	/// <para>for this function to do nothing.</para>
	/// </summary>
	public void Release()
	{
		NativeMethods.SteamAPI_ISteamNetworkingConnectionSignaling_Release(ref this);
	}
}

