namespace SwiftlyS2.Shared.SteamAPI;

[Serializable]
public struct HHTMLBrowser : IEquatable<HHTMLBrowser>, IComparable<HHTMLBrowser>
{
    public static readonly HHTMLBrowser Invalid = new(0);
    public uint m_HHTMLBrowser;

    public HHTMLBrowser( uint value )
    {
        m_HHTMLBrowser = value;
    }

    public override string ToString()
    {
        return m_HHTMLBrowser.ToString();
    }

    public override bool Equals( object other )
    {
        return other is HHTMLBrowser && this == (HHTMLBrowser)other;
    }

    public override int GetHashCode()
    {
        return m_HHTMLBrowser.GetHashCode();
    }

    public static bool operator ==( HHTMLBrowser x, HHTMLBrowser y )
    {
        return x.m_HHTMLBrowser == y.m_HHTMLBrowser;
    }

    public static bool operator !=( HHTMLBrowser x, HHTMLBrowser y )
    {
        return !(x == y);
    }

    public static explicit operator HHTMLBrowser( uint value )
    {
        return new HHTMLBrowser(value);
    }

    public static explicit operator uint( HHTMLBrowser that )
    {
        return that.m_HHTMLBrowser;
    }

    public bool Equals( HHTMLBrowser other )
    {
        return m_HHTMLBrowser == other.m_HHTMLBrowser;
    }

    public int CompareTo( HHTMLBrowser other )
    {
        return m_HHTMLBrowser.CompareTo(other.m_HHTMLBrowser);
    }
}


