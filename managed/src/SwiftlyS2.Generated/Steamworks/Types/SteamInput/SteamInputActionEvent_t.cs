using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Shared.SteamAPI;

/// <summary>
/// <para>when callbacks are enabled this fires each time a controller action</para>
/// <para>state changes</para>
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public struct SteamInputActionEvent_t
{
	public InputHandle_t controllerHandle;

	public ESteamInputActionEventType eEventType;

	/// <summary>
	/// <para>Option value</para>
	/// </summary>
	public OptionValue m_val;

	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct AnalogAction_t
	{
		public InputAnalogActionHandle_t actionHandle;

		public InputAnalogActionData_t analogActionData;
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct DigitalAction_t
	{
		public InputDigitalActionHandle_t actionHandle;

		public InputDigitalActionData_t digitalActionData;
	}

	[Serializable]
	[StructLayout(LayoutKind.Explicit)]
	public struct OptionValue
	{
		[FieldOffset(0)]
		public AnalogAction_t analogAction;

		[FieldOffset(0)]
		public DigitalAction_t digitalAction;
	}
}
