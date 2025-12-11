using System.Runtime.InteropServices;
using SwiftlyS2.Shared.SteamAPI;

namespace SwiftlyS2.Shared.Natives;

public enum NetworkAddressType
{
    NA_NULL = 0,
    NA_LOOPBACK,
    NA_BROADCAST,
    NA_IP,
}

public enum ENSAddressType
{
    kAddressDirect,
    kAddressP2P,
    kAddressProxiedGameServer,
    kAddressProxiedClient,

    kAddressMax
};

[StructLayout(LayoutKind.Sequential)]
public unsafe struct NetworkAddress
{
    public NetworkAddressType Type;
    public fixed byte IP[4];
    public ushort Port;
}

[StructLayout(LayoutKind.Sequential)]
public struct NSAddress
{
    public NetworkAddress NetworkAddress;
    public CSteamID ID;
    public ushort RemotePort;
    public int UnkVar;
    public ENSAddressType AddressType;
}