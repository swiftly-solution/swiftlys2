using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Shared.SteamAPI;

/// <summary>
/// <para>An abstract way to represent the identity of a network host.  All identities can</para>
/// <para>be represented as simple string.  Furthermore, this string representation is actually</para>
/// <para>used on the wire in several places, even though it is less efficient, in order to</para>
/// <para>facilitate forward compatibility.  (Old client code can handle an identity type that</para>
/// <para>it doesn't understand.)</para>
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct SteamNetworkingIdentity : IEquatable<SteamNetworkingIdentity>
{
	/// <summary>
	/// <para>Type of identity.</para>
	/// </summary>
	public ESteamNetworkingIdentityType m_eType;

	/// <summary>
	/// <para>Internal representation.  Don't access this directly, use the accessors!</para>
	/// <para>Number of bytes that are relevant below.  This MUST ALWAYS be</para>
	/// <para>set.  (Use the accessors!)  This is important to enable old code to work</para>
	/// <para>with new identity types.</para>
	/// </summary>
	private int m_cbSize;

	/// <summary>
	/// <para>Pad structure to leave easy room for future expansion</para>
	/// </summary>
	private fixed uint m_reserved[32];

	/// <summary>
	/// <para>Max sizes</para>
	/// <para>Max length of the buffer needed to hold any identity, formatted in string format by ToString</para>
	/// </summary>
	public const int k_cchMaxString = 128;
	/// <summary>
	/// <para>Max length of the string for generic string identities.  Including terminating '\0'</para>
	/// </summary>
	public const int k_cchMaxGenericString = 32;
	/// <summary>
	/// <para>Including terminating '\0'</para>
	/// </summary>
	public const int k_cchMaxXboxPairwiseID = 33;
	public const int k_cbMaxGenericBytes = 32;

	//
	// Get/Set in various formats.
	//

	public void Clear()
	{
		NativeMethods.SteamAPI_SteamNetworkingIdentity_Clear(ref this);
	}

	/// <summary>
	/// <para>Return true if we are the invalid type.  Does not make any other validity checks (e.g. is SteamID actually valid)</para>
	/// </summary>
	public bool IsInvalid()
	{
		return NativeMethods.SteamAPI_SteamNetworkingIdentity_IsInvalid(ref this);
	}

	public void SetSteamID(CSteamID steamID)
	{
		NativeMethods.SteamAPI_SteamNetworkingIdentity_SetSteamID(ref this, (ulong)steamID);
	}

	/// <summary>
	/// <para>Return black CSteamID (!IsValid()) if identity is not a SteamID</para>
	/// </summary>
	public CSteamID GetSteamID()
	{
		return (CSteamID)NativeMethods.SteamAPI_SteamNetworkingIdentity_GetSteamID(ref this);
	}

	/// <summary>
	/// <para>Takes SteamID as raw 64-bit number</para>
	/// </summary>
	public void SetSteamID64(ulong steamID)
	{
		NativeMethods.SteamAPI_SteamNetworkingIdentity_SetSteamID64(ref this, steamID);
	}

	/// <summary>
	/// <para>Returns 0 if identity is not SteamID</para>
	/// </summary>
	public ulong GetSteamID64()
	{
		return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetSteamID64(ref this);
	}

	/// <summary>
	/// <para>Returns false if invalid length</para>
	/// </summary>
	public bool SetXboxPairwiseID(string pszString)
	{
		return NativeMethods.SteamAPI_SteamNetworkingIdentity_SetXboxPairwiseID(ref this, pszString);
	}

	/// <summary>
	/// <para>Returns nullptr if not Xbox ID</para>
	/// </summary>
	public string GetXboxPairwiseID()
	{
		return StringAlloc.CreateCSharpString(NativeMethods.SteamAPI_SteamNetworkingIdentity_GetXboxPairwiseID(ref this));
	}

	public void SetPSNID(ulong id)
	{
		NativeMethods.SteamAPI_SteamNetworkingIdentity_SetPSNID(ref this, id);
	}

	/// <summary>
	/// <para>Returns 0 if not PSN</para>
	/// </summary>
	public ulong GetPSNID()
	{
		return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetPSNID(ref this);
	}

	public void SetStadiaID(ulong id)
	{
		NativeMethods.SteamAPI_SteamNetworkingIdentity_SetStadiaID(ref this, id);
	}

	/// <summary>
	/// <para>Returns 0 if not Stadia</para>
	/// </summary>
	public ulong GetStadiaID()
	{
		return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetStadiaID(ref this);
	}

	/// <summary>
	/// <para>Set to specified IP:port</para>
	/// </summary>
	public void SetIPAddr(SteamNetworkingIPAddr addr)
	{
		NativeMethods.SteamAPI_SteamNetworkingIdentity_SetIPAddr(ref this, ref addr);
	}

	/// <summary>
	/// <para>returns null if we are not an IP address.</para>
	/// </summary>
	public SteamNetworkingIPAddr GetIPAddr()
	{
		throw new NotImplementedException();
		// TODO: Should SteamNetworkingIPAddr be a class?
		//       or should this return some kind of pointer instead?
		//return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetIPAddr(ref this);
	}

	public void SetIPv4Addr(uint nIPv4, ushort nPort)
	{
		NativeMethods.SteamAPI_SteamNetworkingIdentity_SetIPv4Addr(ref this, nIPv4, nPort);
	}

	/// <summary>
	/// <para>returns 0 if we are not an IPv4 address.</para>
	/// </summary>
	public uint GetIPv4()
	{
		return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetIPv4(ref this);
	}

	public ESteamNetworkingFakeIPType GetFakeIPType()
	{
		return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetFakeIPType(ref this);
	}

	public bool IsFakeIP()
	{
		return GetFakeIPType() > ESteamNetworkingFakeIPType.k_ESteamNetworkingFakeIPType_NotFake;
	}

	/// <summary>
	/// <para>"localhost" is equivalent for many purposes to "anonymous."  Our remote</para>
	/// <para>will identify us by the network address we use.</para>
	/// <para>Set to localhost.  (We always use IPv6 ::1 for this, not 127.0.0.1)</para>
	/// </summary>
	public void SetLocalHost()
	{
		NativeMethods.SteamAPI_SteamNetworkingIdentity_SetLocalHost(ref this);
	}

	/// <summary>
	/// <para>Return true if this identity is localhost.</para>
	/// </summary>
	public bool IsLocalHost()
	{
		return NativeMethods.SteamAPI_SteamNetworkingIdentity_IsLocalHost(ref this);
	}

	/// <summary>
	/// <para>Returns false if invalid length</para>
	/// </summary>
	public bool SetGenericString(string pszString)
	{
		return NativeMethods.SteamAPI_SteamNetworkingIdentity_SetGenericString(ref this, pszString);
	}

	/// <summary>
	/// <para>Returns nullptr if not generic string type</para>
	/// </summary>
	public string GetGenericString()
	{
		return StringAlloc.CreateCSharpString(NativeMethods.SteamAPI_SteamNetworkingIdentity_GetGenericString(ref this));
	}

	/// <summary>
	/// <para>Returns false if invalid size.</para>
	/// </summary>
	public bool SetGenericBytes(byte[] data, uint cbLen)
	{
		return NativeMethods.SteamAPI_SteamNetworkingIdentity_SetGenericBytes(ref this, data, cbLen);
	}

	/// <summary>
	/// <para>Returns null if not generic bytes type</para>
	/// </summary>
	public byte[] GetGenericBytes(out int cbLen)
	{
		throw new NotImplementedException();
		//return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetGenericBytes(ref this, out cbLen);
	}

	/// <summary>
	/// <para>See if two identities are identical</para>
	/// </summary>
	public bool Equals(SteamNetworkingIdentity x)
	{
		return NativeMethods.SteamAPI_SteamNetworkingIdentity_IsEqualTo(ref this, ref x);
	}

	/// <summary>
	/// <para>Print to a human-readable string.  This is suitable for debug messages</para>
	/// <para>or any other time you need to encode the identity as a string.  It has a</para>
	/// <para>URL-like format (type:&lt;type-data&gt;).  Your buffer should be at least</para>
	/// <para>k_cchMaxString bytes big to avoid truncation.</para>
	/// <para>See also SteamNetworkingIPAddrRender</para>
	/// </summary>
	public void ToString(out string buf)
	{
		IntPtr buf2 = Marshal.AllocHGlobal(k_cchMaxString);
		NativeMethods.SteamAPI_SteamNetworkingIdentity_ToString(ref this, buf2, k_cchMaxString);
		buf = StringAlloc.CreateCSharpString(buf2);
		Marshal.FreeHGlobal(buf2);
	}

	/// <summary>
	/// <para>Parse back a string that was generated using ToString.  If we don't understand the</para>
	/// <para>string, but it looks "reasonable" (it matches the pattern type:&lt;type-data&gt; and doesn't</para>
	/// <para>have any funky characters, etc), then we will return true, and the type is set to</para>
	/// <para>k_ESteamNetworkingIdentityType_UnknownType.  false will only be returned if the string</para>
	/// <para>looks invalid.</para>
	/// </summary>
	public bool ParseString(string pszStr)
	{
		return NativeMethods.SteamAPI_SteamNetworkingIdentity_ParseString(ref this, pszStr);
	}
}
