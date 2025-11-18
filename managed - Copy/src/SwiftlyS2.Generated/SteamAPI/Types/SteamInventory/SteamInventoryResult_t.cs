using System.Runtime.InteropServices;
using IntPtr = System.IntPtr;

namespace SwiftlyS2.Shared.SteamAPI {
	[System.Serializable]
	public struct SteamInventoryResult_t : System.IEquatable<SteamInventoryResult_t>, System.IComparable<SteamInventoryResult_t> {
		public static readonly SteamInventoryResult_t Invalid = new SteamInventoryResult_t(-1);
		public int m_SteamInventoryResult;

		public SteamInventoryResult_t(int value) {
			m_SteamInventoryResult = value;
		}

		public override string ToString() {
			return m_SteamInventoryResult.ToString();
		}

		public override bool Equals(object other) {
			return other is SteamInventoryResult_t && this == (SteamInventoryResult_t)other;
		}

		public override int GetHashCode() {
			return m_SteamInventoryResult.GetHashCode();
		}

		public static bool operator ==(SteamInventoryResult_t x, SteamInventoryResult_t y) {
			return x.m_SteamInventoryResult == y.m_SteamInventoryResult;
		}

		public static bool operator !=(SteamInventoryResult_t x, SteamInventoryResult_t y) {
			return !(x == y);
		}

		public static explicit operator SteamInventoryResult_t(int value) {
			return new SteamInventoryResult_t(value);
		}

		public static explicit operator int(SteamInventoryResult_t that) {
			return that.m_SteamInventoryResult;
		}

		public bool Equals(SteamInventoryResult_t other) {
			return m_SteamInventoryResult == other.m_SteamInventoryResult;
		}

		public int CompareTo(SteamInventoryResult_t other) {
			return m_SteamInventoryResult.CompareTo(other.m_SteamInventoryResult);
		}
	}
}


