using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Shared.SteamAPI;

/// <summary>
/// <para>Store an IP and port.  IPv6 is always used; IPv4 is represented using</para>
/// <para>"IPv4-mapped" addresses: IPv4 aa.bb.cc.dd =&gt; IPv6 ::ffff:aabb:ccdd</para>
/// <para>(RFC 4291 section 2.5.5.2.)</para>
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct SteamNetworkingIPAddr : IEquatable<SteamNetworkingIPAddr>
{
	public fixed byte m_ipv6[16];
	/// <summary>
	/// <para>Host byte order</para>
	/// </summary>
	public ushort m_port;

	/// <summary>
	/// <para>Max length of the buffer needed to hold IP formatted using ToString, including '\0'</para>
	/// <para>([0123:4567:89ab:cdef:0123:4567:89ab:cdef]:12345)</para>
	/// </summary>
	public const int k_cchMaxString = 48;

	/// <summary>
	/// <para>Set everything to zero.  E.g. [::]:0</para>
	/// </summary>
	public void Clear()
	{
		NativeMethods.SteamAPI_SteamNetworkingIPAddr_Clear(ref this);
	}

	/// <summary>
	/// <para>Return true if the IP is ::0.  (Doesn't check port.)</para>
	/// </summary>
	public bool IsIPv6AllZeros()
	{
		return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsIPv6AllZeros(ref this);
	}

	/// <summary>
	/// <para>Set IPv6 address.  IP is interpreted as bytes, so there are no endian issues.  (Same as inaddr_in6.)  The IP can be a mapped IPv4 address</para>
	/// </summary>
	public void SetIPv6(byte[] ipv6, ushort nPort)
	{
		NativeMethods.SteamAPI_SteamNetworkingIPAddr_SetIPv6(ref this, ipv6, nPort);
	}

	/// <summary>
	/// <para>Sets to IPv4 mapped address.  IP and port are in host byte order.</para>
	/// </summary>
	public void SetIPv4(uint nIP, ushort nPort)
	{
		NativeMethods.SteamAPI_SteamNetworkingIPAddr_SetIPv4(ref this, nIP, nPort);
	}

	/// <summary>
	/// <para>Return true if IP is mapped IPv4</para>
	/// </summary>
	public bool IsIPv4()
	{
		return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsIPv4(ref this);
	}

	/// <summary>
	/// <para>Returns IP in host byte order (e.g. aa.bb.cc.dd as 0xaabbccdd).  Returns 0 if IP is not mapped IPv4.</para>
	/// </summary>
	public uint GetIPv4()
	{
		return NativeMethods.SteamAPI_SteamNetworkingIPAddr_GetIPv4(ref this);
	}

	/// <summary>
	/// <para>Set to the IPv6 localhost address ::1, and the specified port.</para>
	/// </summary>
	public void SetIPv6LocalHost(ushort nPort = 0)
	{
		NativeMethods.SteamAPI_SteamNetworkingIPAddr_SetIPv6LocalHost(ref this, nPort);
	}

	/// <summary>
	/// <para>Return true if this identity is localhost.  (Either IPv6 ::1, or IPv4 127.0.0.1)</para>
	/// </summary>
	public bool IsLocalHost()
	{
		return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsLocalHost(ref this);
	}

	/// <summary>
	/// <para>Print to a string, with or without the port.  Mapped IPv4 addresses are printed</para>
	/// <para>as dotted decimal (12.34.56.78), otherwise this will print the canonical</para>
	/// <para>form according to RFC5952.  If you include the port, IPv6 will be surrounded by</para>
	/// <para>brackets, e.g. [::1:2]:80.  Your buffer should be at least k_cchMaxString bytes</para>
	/// <para>to avoid truncation</para>
	/// <para>See also SteamNetworkingIdentityRender</para>
	/// </summary>
	public void ToString(out string buf, bool bWithPort)
	{
		IntPtr buf2 = Marshal.AllocHGlobal(k_cchMaxString);
		NativeMethods.SteamAPI_SteamNetworkingIPAddr_ToString(ref this, buf2, k_cchMaxString, bWithPort);
		buf = StringAlloc.CreateCSharpString(buf2);
		Marshal.FreeHGlobal(buf2);
	}

	/// <summary>
	/// <para>Parse an IP address and optional port.  If a port is not present, it is set to 0.</para>
	/// <para>(This means that you cannot tell if a zero port was explicitly specified.)</para>
	/// </summary>
	public bool ParseString(string pszStr)
	{
		return NativeMethods.SteamAPI_SteamNetworkingIPAddr_ParseString(ref this, pszStr);
	}

	/// <summary>
	/// <para>See if two addresses are identical</para>
	/// </summary>
	public bool Equals(SteamNetworkingIPAddr x)
	{
		return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsEqualTo(ref this, ref x);
	}

	/// <summary>
	/// <para>Classify address as FakeIP.  This function never returns</para>
	/// <para>k_ESteamNetworkingFakeIPType_Invalid.</para>
	/// </summary>
	public ESteamNetworkingFakeIPType GetFakeIPType()
	{
		return NativeMethods.SteamAPI_SteamNetworkingIPAddr_GetFakeIPType(ref this);
	}

	/// <summary>
	/// <para>Return true if we are a FakeIP</para>
	/// </summary>
	public bool IsFakeIP()
	{
		return GetFakeIPType() > ESteamNetworkingFakeIPType.k_ESteamNetworkingFakeIPType_NotFake;
	}
}
