using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Shared.SteamAPI;

/// <summary>
/// <para>A message that has been received.</para>
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public struct SteamNetworkingMessage_t
{
	/// <summary>
	/// <para>Message payload</para>
	/// </summary>
	public IntPtr m_pData;

	/// <summary>
	/// <para>Size of the payload.</para>
	/// </summary>
	public int m_cbSize;

	/// <summary>
	/// <para>For messages received on connections: what connection did this come from?</para>
	/// <para>For outgoing messages: what connection to send it to?</para>
	/// <para>Not used when using the ISteamNetworkingMessages interface</para>
	/// </summary>
	public HSteamNetConnection m_conn;

	/// <summary>
	/// <para>For inbound messages: Who sent this to us?</para>
	/// <para>For outbound messages on connections: not used.</para>
	/// <para>For outbound messages on the ad-hoc ISteamNetworkingMessages interface: who should we send this to?</para>
	/// </summary>
	public SteamNetworkingIdentity m_identityPeer;

	/// <summary>
	/// <para>For messages received on connections, this is the user data</para>
	/// <para>associated with the connection.</para>
	/// <para>This is *usually* the same as calling GetConnection() and then</para>
	/// <para>fetching the user data associated with that connection, but for</para>
	/// <para>the following subtle differences:</para>
	/// <para>- This user data will match the connection's user data at the time</para>
	/// <para>is captured at the time the message is returned by the API.</para>
	/// <para>If you subsequently change the userdata on the connection,</para>
	/// <para>this won't be updated.</para>
	/// <para>- This is an inline call, so it's *much* faster.</para>
	/// <para>- You might have closed the connection, so fetching the user data</para>
	/// <para>would not be possible.</para>
	/// <para>Not used when sending messages.</para>
	/// </summary>
	public long m_nConnUserData;

	/// <summary>
	/// <para>Local timestamp when the message was received</para>
	/// <para>Not used for outbound messages.</para>
	/// </summary>
	public SteamNetworkingMicroseconds m_usecTimeReceived;

	/// <summary>
	/// <para>Message number assigned by the sender.  This is not used for outbound</para>
	/// <para>messages.  Note that if multiple lanes are used, each lane has its own</para>
	/// <para>message numbers, which are assigned sequentially, so messages from</para>
	/// <para>different lanes will share the same numbers.</para>
	/// </summary>
	public long m_nMessageNumber;

	/// <summary>
	/// <para>Function used to free up m_pData.  This mechanism exists so that</para>
	/// <para>apps can create messages with buffers allocated from their own</para>
	/// <para>heap, and pass them into the library.  This function will</para>
	/// <para>usually be something like:</para>
	/// <para>free( pMsg-&gt;m_pData );</para>
	/// </summary>
	public IntPtr m_pfnFreeData;

	/// <summary>
	/// <para>Function to used to decrement the internal reference count and, if</para>
	/// <para>it's zero, release the message.  You should not set this function pointer,</para>
	/// <para>or need to access this directly!  Use the Release() function instead!</para>
	/// </summary>
	internal IntPtr m_pfnRelease;

	/// <summary>
	/// <para>When using ISteamNetworkingMessages, the channel number the message was received on</para>
	/// <para>(Not used for messages sent or received on "connections")</para>
	/// </summary>
	public int m_nChannel;

	/// <summary>
	/// <para>Bitmask of k_nSteamNetworkingSend_xxx flags.</para>
	/// <para>For received messages, only the k_nSteamNetworkingSend_Reliable bit is valid.</para>
	/// <para>For outbound messages, all bits are relevant</para>
	/// </summary>
	public int m_nFlags;

	/// <summary>
	/// <para>Arbitrary user data that you can use when sending messages using</para>
	/// <para>ISteamNetworkingUtils::AllocateMessage and ISteamNetworkingSockets::SendMessage.</para>
	/// <para>(The callback you set in m_pfnFreeData might use this field.)</para>
	/// <para>Not used for received messages.</para>
	/// </summary>
	public long m_nUserData;

	/// <summary>
	/// <para>For outbound messages, which lane to use?  See ISteamNetworkingSockets::ConfigureConnectionLanes.</para>
	/// <para>For inbound messages, what lane was the message received on?</para>
	/// </summary>
	public ushort m_idxLane;

	public ushort _pad1__;

	/// <summary>
	/// <para>You MUST call this when you're done with the object,</para>
	/// <para>to free up memory, etc.</para>
	/// </summary>
	public void Release()
	{
		throw new NotImplementedException("Please use the static Release function instead which takes an IntPtr.");
	}

	/// <summary>
	/// <para>You MUST call this when you're done with the object,</para>
	/// <para>to free up memory, etc.</para>
	/// <para>This is a Steamworks.NET extension.</para>
	/// </summary>
	public static void Release(IntPtr pointer)
	{
		NativeMethods.SteamAPI_SteamNetworkingMessage_t_Release(pointer);
	}

	/// <summary>
	/// <para>Convert an IntPtr received from ISteamNetworkingSockets.ReceiveMessagesOnPollGroup into our structure.</para>
	/// <para>This is a Steamworks.NET extension.</para>
	/// </summary>
	public static SteamNetworkingMessage_t FromIntPtr(IntPtr pointer)
	{
		return Marshal.PtrToStructure<SteamNetworkingMessage_t>(pointer);
	}
}
