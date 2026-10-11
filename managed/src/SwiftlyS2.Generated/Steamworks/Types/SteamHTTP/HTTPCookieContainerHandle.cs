using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Shared.SteamAPI;

[Serializable]
public struct HTTPCookieContainerHandle : IEquatable<HTTPCookieContainerHandle>, IComparable<HTTPCookieContainerHandle>
{
	public static readonly HTTPCookieContainerHandle Invalid = new HTTPCookieContainerHandle(0);
	public uint m_HTTPCookieContainerHandle;

	public HTTPCookieContainerHandle(uint value)
	{
		m_HTTPCookieContainerHandle = value;
	}

	public override string ToString()
	{
		return m_HTTPCookieContainerHandle.ToString();
	}

	public override bool Equals(object? other)
	{
		return other is HTTPCookieContainerHandle && this == (HTTPCookieContainerHandle)other;
	}

	public override int GetHashCode()
	{
		return m_HTTPCookieContainerHandle.GetHashCode();
	}

	public static bool operator ==(HTTPCookieContainerHandle x, HTTPCookieContainerHandle y)
	{
		return x.m_HTTPCookieContainerHandle == y.m_HTTPCookieContainerHandle;
	}

	public static bool operator !=(HTTPCookieContainerHandle x, HTTPCookieContainerHandle y)
	{
		return !(x == y);
	}

	public static explicit operator HTTPCookieContainerHandle(uint value)
	{
		return new HTTPCookieContainerHandle(value);
	}

	public static explicit operator uint(HTTPCookieContainerHandle that)
	{
		return that.m_HTTPCookieContainerHandle;
	}

	public bool Equals(HTTPCookieContainerHandle other)
	{
		return m_HTTPCookieContainerHandle == other.m_HTTPCookieContainerHandle;
	}

	public int CompareTo(HTTPCookieContainerHandle other)
	{
		return m_HTTPCookieContainerHandle.CompareTo(other.m_HTTPCookieContainerHandle);
	}
}
