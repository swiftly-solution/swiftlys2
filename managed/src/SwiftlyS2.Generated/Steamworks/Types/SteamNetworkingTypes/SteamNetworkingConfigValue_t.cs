using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Shared.SteamAPI;

/// <summary>
/// <para>In a few places we need to set configuration options on listen sockets and connections, and</para>
/// <para>have them take effect *before* the listen socket or connection really starts doing anything.</para>
/// <para>Creating the object and then setting the options "immediately" after creation doesn't work</para>
/// <para>completely, because network packets could be received between the time the object is created and</para>
/// <para>when the options are applied.  To set options at creation time in a reliable way, they must be</para>
/// <para>passed to the creation function.  This structure is used to pass those options.</para>
/// <para>For the meaning of these fields, see ISteamNetworkingUtils::SetConfigValue.  Basically</para>
/// <para>when the object is created, we just iterate over the list of options and call</para>
/// <para>ISteamNetworkingUtils::SetConfigValueStruct, where the scope arguments are supplied by the</para>
/// <para>object being created.</para>
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public struct SteamNetworkingConfigValue_t
{
	/// <summary>
	/// <para>Which option is being set</para>
	/// </summary>
	public ESteamNetworkingConfigValue m_eValue;

	/// <summary>
	/// <para>Which field below did you fill in?</para>
	/// </summary>
	public ESteamNetworkingConfigDataType m_eDataType;

	/// <summary>
	/// <para>Option value</para>
	/// </summary>
	public OptionValue m_val;

	[StructLayout(LayoutKind.Explicit)]
	public struct OptionValue
	{
		[FieldOffset(0)]
		public int m_int32;

		[FieldOffset(0)]
		public long m_int64;

		[FieldOffset(0)]
		public float m_float;

		/// <summary>
		/// <para>Points to your '\0'-terminated buffer</para>
		/// </summary>
		[FieldOffset(0)]
		public IntPtr m_string;

		[FieldOffset(0)]
		public IntPtr m_functionPtr;
	}
}

