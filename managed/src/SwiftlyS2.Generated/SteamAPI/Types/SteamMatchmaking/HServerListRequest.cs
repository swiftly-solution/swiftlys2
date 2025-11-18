using IntPtr = nint;

namespace SwiftlyS2.Shared.SteamAPI;

[Serializable]
public struct HServerListRequest : IEquatable<HServerListRequest>
{
    public static readonly HServerListRequest Invalid = new(IntPtr.Zero);
    public nint m_HServerListRequest;

    public HServerListRequest( nint value )
    {
        m_HServerListRequest = value;
    }

    public override string ToString()
    {
        return m_HServerListRequest.ToString();
    }

    public override bool Equals( object other )
    {
        return other is HServerListRequest && this == (HServerListRequest)other;
    }

    public override int GetHashCode()
    {
        return m_HServerListRequest.GetHashCode();
    }

    public static bool operator ==( HServerListRequest x, HServerListRequest y )
    {
        return x.m_HServerListRequest == y.m_HServerListRequest;
    }

    public static bool operator !=( HServerListRequest x, HServerListRequest y )
    {
        return !(x == y);
    }

    public static explicit operator HServerListRequest( nint value )
    {
        return new HServerListRequest(value);
    }

    public static explicit operator nint( HServerListRequest that )
    {
        return that.m_HServerListRequest;
    }

    public bool Equals( HServerListRequest other )
    {
        return m_HServerListRequest == other.m_HServerListRequest;
    }
}


