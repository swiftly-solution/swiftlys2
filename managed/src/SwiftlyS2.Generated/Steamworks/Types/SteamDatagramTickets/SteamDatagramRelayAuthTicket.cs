using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Shared.SteamAPI;

/// <summary>
/// <para>Network-routable identifier for a service.  This is an intentionally</para>
/// <para>opaque byte blob.  The relays know how to use this to forward it on</para>
/// <para>to the intended destination, but otherwise clients really should not</para>
/// <para>need to know what's inside.  (Indeed, we don't really want them to</para>
/// <para>know, as it could reveal information useful to an attacker.)</para>
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
public unsafe struct SteamDatagramRelayAuthTicket
{
	/// <summary>
	/// <para>Identity of the gameserver we want to talk to.  This is required.</para>
	/// </summary>
	SteamNetworkingIdentity m_identityGameserver;

	/// <summary>
	/// <para>Identity of the person who was authorized.  This is required.</para>
	/// </summary>
	SteamNetworkingIdentity m_identityAuthorizedClient;

	/// <summary>
	/// <para>SteamID is authorized to send from a particular public IP.  If this</para>
	/// <para>is 0, then the sender is not restricted to a particular IP.</para>
	/// <para>Recommend to leave this set to zero.</para>
	/// </summary>
	uint m_unPublicIP;

	/// <summary>
	/// <para>Time when the ticket expires.  Recommended: take the current</para>
	/// <para>time and add 6 hours, or maybe a bit longer if your gameplay</para>
	/// <para>sessions are longer.</para>
	/// <para>NOTE: relays may reject tickets with expiry times excessively</para>
	/// <para>far in the future, so contact us if you wish to use an expiry</para>
	/// <para>longer than, say, 24 hours.</para>
	/// </summary>
	RTime32 m_rtimeTicketExpiry;

	/// <summary>
	/// <para>Routing information where the gameserver is listening for</para>
	/// <para>relayed traffic.  You should fill this in when generating</para>
	/// <para>a ticket.</para>
	/// <para>When generating tickets on your backend:</para>
	/// <para>- In production: The gameserver knows the proper routing</para>
	/// <para>information, so you need to call</para>
	/// <para>ISteamNetworkingSockets::GetHostedDedicatedServerAddress</para>
	/// <para>and send the info to your backend.</para>
	/// <para>- In development, you will need to provide public IP</para>
	/// <para>of the server using SteamDatagramServiceNetID::SetDevAddress.</para>
	/// <para>Relays need to be able to send UDP</para>
	/// <para>packets to this server.  Since it's very likely that</para>
	/// <para>your server is behind a firewall/NAT, make sure that</para>
	/// <para>the address is the one that the outside world can use.</para>
	/// <para>The traffic from the relays will be "unsolicited", so</para>
	/// <para>stateful firewalls won't work -- you will probably have</para>
	/// <para>to set up an explicit port forward.</para>
	/// <para>On the client:</para>
	/// <para>- this field will always be blank.</para>
	/// </summary>
	SteamDatagramHostedAddress m_routing;

	/// <summary>
	/// <para>App ID this is for.  This is required, and should be the</para>
	/// <para>App ID the client is running.  (Even if your gameserver</para>
	/// <para>uses a different App ID.)</para>
	/// </summary>
	uint m_nAppID;

	/// <summary>
	/// <para>Restrict this ticket to be used for a particular virtual port?</para>
	/// <para>Set to -1 to allow any virtual port.</para>
	/// <para>This is useful as a security measure, and also so the client will</para>
	/// <para>use the right ticket (which might have extra fields that are useful</para>
	/// <para>for proper analytics), if the client happens to have more than one</para>
	/// <para>appropriate ticket.</para>
	/// <para>Note: if a client has more that one acceptable ticket, they will</para>
	/// <para>always use the one expiring the latest.</para>
	/// </summary>
	int m_nRestrictToVirtualPort;

	/// <summary>
	/// <para>Extra fields.</para>
	/// <para>These are collected for backend analytics.  For example, you might</para>
	/// <para>send a MatchID so that all of the records for a particular match can</para>
	/// <para>be located.  Or send a game mode field so that you can compare</para>
	/// <para>the network characteristics of different game modes.</para>
	/// <para>(At the time of this writing we don't have a way to expose the data</para>
	/// <para>we collect to partners, but we hope to in the future so that you can</para>
	/// <para>get visibility into network conditions.)</para>
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
	struct ExtraField
	{
		enum EType
		{
			k_EType_String,
			/// <summary>
			/// <para>For most small integral values.  Uses google protobuf sint64, so it's small on the wire.  WARNING: In some places this value may be transmitted in JSON, in which case precision may be lost in backend analytics.  Don't use this for an "identifier", use it for a scalar quantity.</para>
			/// </summary>
			k_EType_Int,
			/// <summary>
			/// <para>64 arbitrary bits.  This value is treated as an "identifier".  In places where JSON format is used, it will be serialized as a string.  No aggregation / analytics can be performed on this value.</para>
			/// </summary>
			k_EType_Fixed64,
		};
		EType m_eType;

		fixed byte m_szName[28];

		[StructLayout(LayoutKind.Explicit)]
		struct OptionValue
		{
			[FieldOffset(0)]
			fixed byte m_szStringValue[128];

			[FieldOffset(0)]
			long m_nIntValue;

			[FieldOffset(0)]
			ulong m_nFixed64Value;
		}
		OptionValue m_val;
	};

	const int k_nMaxExtraFields = 16;

	int m_nExtraFields;

	[InlineArray(k_nMaxExtraFields)]
	struct ExtraFieldArray
	{
		private ExtraField _element0;
	}

	ExtraFieldArray m_vecExtraFields;

	/// <summary>
	/// <para>Reset all fields</para>
	/// </summary>
	public void Clear()
	{
	}
}

