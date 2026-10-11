using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Shared.SteamAPI;

/// <summary>
/// <para>Interface used when a custom signal is received.</para>
/// <para>See ISteamNetworkingSockets::ReceivedP2PCustomSignal</para>
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public struct ISteamNetworkingSignalingRecvContext
{
	/// <summary>
	/// <para>Called when the signal represents a request for a new connection.</para>
	/// <para>If you want to ignore the request, just return NULL.  In this case,</para>
	/// <para>the peer will NOT receive any reply.  You should consider ignoring</para>
	/// <para>requests rather than actively rejecting them, as a security measure.</para>
	/// <para>If you actively reject requests, then this makes it possible to detect</para>
	/// <para>if a user is online or not, just by sending them a request.</para>
	/// <para>If you wish to send back a rejection, then use</para>
	/// <para>ISteamNetworkingSockets::CloseConnection() and then return NULL.</para>
	/// <para>We will marshal a properly formatted rejection signal and</para>
	/// <para>call SendRejectionSignal() so you can send it to them.</para>
	/// <para>If you return a signaling object, the connection is NOT immediately</para>
	/// <para>accepted by default.  Instead, it stays in the "connecting" state,</para>
	/// <para>and the usual callback is posted, and your app can accept the</para>
	/// <para>connection using ISteamNetworkingSockets::AcceptConnection.  This</para>
	/// <para>may be useful so that these sorts of connections can be more similar</para>
	/// <para>to your application code as other types of connections accepted on</para>
	/// <para>a listen socket.  If this is not useful and you want to skip this</para>
	/// <para>callback process and immediately accept the connection, call</para>
	/// <para>ISteamNetworkingSockets::AcceptConnection before returning the</para>
	/// <para>signaling object.</para>
	/// <para>After accepting a connection (through either means), the connection</para>
	/// <para>will transition into the "finding route" state.</para>
	/// </summary>
	public IntPtr OnConnectRequest(HSteamNetConnection hConn, ref SteamNetworkingIdentity identityPeer, int nLocalVirtualPort)
	{
		return NativeMethods.SteamAPI_ISteamNetworkingSignalingRecvContext_OnConnectRequest(ref this, hConn, ref identityPeer, nLocalVirtualPort);
	}

	/// <summary>
	/// <para>This is called to actively communicate rejection or failure</para>
	/// <para>to the incoming message.  If you intend to ignore all incoming requests</para>
	/// <para>that you do not wish to accept, then it's not strictly necessary to</para>
	/// <para>implement this.</para>
	/// </summary>
	public void SendRejectionSignal(ref SteamNetworkingIdentity identityPeer, IntPtr pMsg, int cbMsg)
	{
		NativeMethods.SteamAPI_ISteamNetworkingSignalingRecvContext_SendRejectionSignal(ref this, ref identityPeer, pMsg, cbMsg);
	}
}

