using System.Runtime.InteropServices;
using IntPtr = System.IntPtr;

namespace SwiftlyS2.Shared.SteamAPI {
	[System.Serializable]
	public struct SteamNetworkingMicroseconds : System.IEquatable<SteamNetworkingMicroseconds>, System.IComparable<SteamNetworkingMicroseconds> {
		public long m_SteamNetworkingMicroseconds;

		public SteamNetworkingMicroseconds(long value) {
			m_SteamNetworkingMicroseconds = value;
		}

		public override string ToString() {
			return m_SteamNetworkingMicroseconds.ToString();
		}

		public override bool Equals(object other) {
			return other is SteamNetworkingMicroseconds && this == (SteamNetworkingMicroseconds)other;
		}

		public override int GetHashCode() {
			return m_SteamNetworkingMicroseconds.GetHashCode();
		}

		public static bool operator ==(SteamNetworkingMicroseconds x, SteamNetworkingMicroseconds y) {
			return x.m_SteamNetworkingMicroseconds == y.m_SteamNetworkingMicroseconds;
		}

		public static bool operator !=(SteamNetworkingMicroseconds x, SteamNetworkingMicroseconds y) {
			return !(x == y);
		}

		public static explicit operator SteamNetworkingMicroseconds(long value) {
			return new SteamNetworkingMicroseconds(value);
		}

		public static explicit operator long(SteamNetworkingMicroseconds that) {
			return that.m_SteamNetworkingMicroseconds;
		}

		public bool Equals(SteamNetworkingMicroseconds other) {
			return m_SteamNetworkingMicroseconds == other.m_SteamNetworkingMicroseconds;
		}

		public int CompareTo(SteamNetworkingMicroseconds other) {
			return m_SteamNetworkingMicroseconds.CompareTo(other.m_SteamNetworkingMicroseconds);
		}
	}
}


