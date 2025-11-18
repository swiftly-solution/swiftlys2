using System.Runtime.InteropServices;
using IntPtr = System.IntPtr;

namespace SwiftlyS2.Shared.SteamAPI {
	[System.Serializable]
	public struct SteamNetworkingPOPID : System.IEquatable<SteamNetworkingPOPID>, System.IComparable<SteamNetworkingPOPID> {
		public uint m_SteamNetworkingPOPID;

		public SteamNetworkingPOPID(uint value) {
			m_SteamNetworkingPOPID = value;
		}

		public override string ToString() {
			return m_SteamNetworkingPOPID.ToString();
		}

		public override bool Equals(object other) {
			return other is SteamNetworkingPOPID && this == (SteamNetworkingPOPID)other;
		}

		public override int GetHashCode() {
			return m_SteamNetworkingPOPID.GetHashCode();
		}

		public static bool operator ==(SteamNetworkingPOPID x, SteamNetworkingPOPID y) {
			return x.m_SteamNetworkingPOPID == y.m_SteamNetworkingPOPID;
		}

		public static bool operator !=(SteamNetworkingPOPID x, SteamNetworkingPOPID y) {
			return !(x == y);
		}

		public static explicit operator SteamNetworkingPOPID(uint value) {
			return new SteamNetworkingPOPID(value);
		}

		public static explicit operator uint(SteamNetworkingPOPID that) {
			return that.m_SteamNetworkingPOPID;
		}

		public bool Equals(SteamNetworkingPOPID other) {
			return m_SteamNetworkingPOPID == other.m_SteamNetworkingPOPID;
		}

		public int CompareTo(SteamNetworkingPOPID other) {
			return m_SteamNetworkingPOPID.CompareTo(other.m_SteamNetworkingPOPID);
		}
	}
}


