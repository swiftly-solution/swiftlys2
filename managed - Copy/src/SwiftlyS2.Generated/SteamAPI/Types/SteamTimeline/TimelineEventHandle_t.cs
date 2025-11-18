using System.Runtime.InteropServices;
using IntPtr = System.IntPtr;

namespace SwiftlyS2.Shared.SteamAPI {
	[System.Serializable]
	public struct TimelineEventHandle_t : System.IEquatable<TimelineEventHandle_t>, System.IComparable<TimelineEventHandle_t> {
		public ulong m_TimelineEventHandle;

		public TimelineEventHandle_t(ulong value) {
			m_TimelineEventHandle = value;
		}

		public override string ToString() {
			return m_TimelineEventHandle.ToString();
		}

		public override bool Equals(object other) {
			return other is TimelineEventHandle_t && this == (TimelineEventHandle_t)other;
		}

		public override int GetHashCode() {
			return m_TimelineEventHandle.GetHashCode();
		}

		public static bool operator ==(TimelineEventHandle_t x, TimelineEventHandle_t y) {
			return x.m_TimelineEventHandle == y.m_TimelineEventHandle;
		}

		public static bool operator !=(TimelineEventHandle_t x, TimelineEventHandle_t y) {
			return !(x == y);
		}

		public static explicit operator TimelineEventHandle_t(ulong value) {
			return new TimelineEventHandle_t(value);
		}

		public static explicit operator ulong(TimelineEventHandle_t that) {
			return that.m_TimelineEventHandle;
		}

		public bool Equals(TimelineEventHandle_t other) {
			return m_TimelineEventHandle == other.m_TimelineEventHandle;
		}

		public int CompareTo(TimelineEventHandle_t other) {
			return m_TimelineEventHandle.CompareTo(other.m_TimelineEventHandle);
		}
	}
}


