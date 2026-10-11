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
public unsafe struct SteamDatagramHostedAddress
{
	/// <summary>
	/// <para>Size of data blob.</para>
	/// </summary>
	public int m_cbSize;

	/// <summary>
	/// <para>Opaque</para>
	/// </summary>
	public fixed byte m_data[128];

	/// <summary>
	/// <para>Reset to empty state</para>
	/// </summary>
	public void Clear()
	{
		m_cbSize = 0;
		fixed (byte* data = m_data)
			new Span<byte>(data, 128).Clear();
	}
}