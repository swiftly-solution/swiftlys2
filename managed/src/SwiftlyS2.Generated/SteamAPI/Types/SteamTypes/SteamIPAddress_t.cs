using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.SteamAPI;

[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SteamIPAddress_t
{
    private readonly long m_ip0;
    private readonly long m_ip1;

    private readonly ESteamIPType m_eType;

    public SteamIPAddress_t( System.Net.IPAddress iPAddress )
    {
        var bytes = iPAddress.GetAddressBytes();
        switch (iPAddress.AddressFamily)
        {
            case System.Net.Sockets.AddressFamily.InterNetwork:
                {
                    if (bytes.Length != 4)
                    {
                        throw new TypeInitializationException("SteamIPAddress_t: Unexpected byte length for Ipv4." + bytes.Length, null);
                    }

                    m_ip0 = (bytes[0] << 24) | (bytes[1] << 16) | (bytes[2] << 8) | bytes[3];
                    m_ip1 = 0;
                    m_eType = ESteamIPType.k_ESteamIPTypeIPv4;
                    break;
                }
            case System.Net.Sockets.AddressFamily.InterNetworkV6:
                {
                    if (bytes.Length != 16)
                    {
                        throw new TypeInitializationException("SteamIPAddress_t: Unexpected byte length for Ipv6: " + bytes.Length, null);
                    }

                    m_ip0 = (bytes[1] << 56) | (bytes[0] << 48) | (bytes[3] << 40) | (bytes[2] << 32) | (bytes[5] << 24) | (bytes[4] << 16) | (bytes[7] << 8) | bytes[6];
                    m_ip1 = (bytes[9] << 56) | (bytes[8] << 48) | (bytes[11] << 40) | (bytes[10] << 32) | (bytes[13] << 24) | (bytes[12] << 16) | (bytes[15] << 8) | bytes[14];
                    m_eType = ESteamIPType.k_ESteamIPTypeIPv6;
                    break;
                }

            case System.Net.Sockets.AddressFamily.Unknown:
                break;
            case System.Net.Sockets.AddressFamily.Unspecified:
                break;
            case System.Net.Sockets.AddressFamily.Unix:
                break;
            case System.Net.Sockets.AddressFamily.ImpLink:
                break;
            case System.Net.Sockets.AddressFamily.Pup:
                break;
            case System.Net.Sockets.AddressFamily.Chaos:
                break;
            case System.Net.Sockets.AddressFamily.Ipx:
                break;
            case System.Net.Sockets.AddressFamily.Iso:
                break;
            case System.Net.Sockets.AddressFamily.Ecma:
                break;
            case System.Net.Sockets.AddressFamily.DataKit:
                break;
            case System.Net.Sockets.AddressFamily.Ccitt:
                break;
            case System.Net.Sockets.AddressFamily.Sna:
                break;
            case System.Net.Sockets.AddressFamily.DecNet:
                break;
            case System.Net.Sockets.AddressFamily.DataLink:
                break;
            case System.Net.Sockets.AddressFamily.Lat:
                break;
            case System.Net.Sockets.AddressFamily.HyperChannel:
                break;
            case System.Net.Sockets.AddressFamily.AppleTalk:
                break;
            case System.Net.Sockets.AddressFamily.NetBios:
                break;
            case System.Net.Sockets.AddressFamily.VoiceView:
                break;
            case System.Net.Sockets.AddressFamily.FireFox:
                break;
            case System.Net.Sockets.AddressFamily.Banyan:
                break;
            case System.Net.Sockets.AddressFamily.Atm:
                break;
            case System.Net.Sockets.AddressFamily.Cluster:
                break;
            case System.Net.Sockets.AddressFamily.Ieee12844:
                break;
            case System.Net.Sockets.AddressFamily.Irda:
                break;
            case System.Net.Sockets.AddressFamily.NetworkDesigners:
                break;
            case System.Net.Sockets.AddressFamily.Max:
                break;
            case System.Net.Sockets.AddressFamily.Packet:
                break;
            case System.Net.Sockets.AddressFamily.ControllerAreaNetwork:
                break;
            default:
                {
                    throw new TypeInitializationException("SteamIPAddress_t: Unexpected address family " + iPAddress.AddressFamily, null);
                }
        }
    }

    public System.Net.IPAddress ToIPAddress()
    {
        if (m_eType == ESteamIPType.k_ESteamIPTypeIPv4)
        {
            var bytes = BitConverter.GetBytes(m_ip0);
            return new System.Net.IPAddress(new byte[] { bytes[3], bytes[2], bytes[1], bytes[0] });
        }
        else
        {
            var bytes = new byte[16];
            BitConverter.GetBytes(m_ip0).CopyTo(bytes, 0);
            BitConverter.GetBytes(m_ip1).CopyTo(bytes, 8);
            return new System.Net.IPAddress(bytes);
        }
    }

    public override string ToString()
    {
        return ToIPAddress().ToString();
    }

    public ESteamIPType GetIPType()
    {
        return m_eType;
    }

    public bool IsSet()
    {
        return m_ip0 != 0 || m_ip1 != 0;
    }
}


