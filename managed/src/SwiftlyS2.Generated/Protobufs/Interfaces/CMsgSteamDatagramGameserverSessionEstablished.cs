using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramGameserverSessionEstablished : ITypedProtobuf<CMsgSteamDatagramGameserverSessionEstablished>
{
    static CMsgSteamDatagramGameserverSessionEstablished ITypedProtobuf<CMsgSteamDatagramGameserverSessionEstablished>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramGameserverSessionEstablishedImpl(handle, isManuallyAllocated);

    public uint ConnectionId { get; set; }
    public string GameserverIdentityString { get; set; }
    public uint SecondsUntilShutdown { get; set; }
    public uint SeqNumR2c { get; set; }
    public byte[] DummyLegacyIdentityBinary { get; set; }
    public ulong LegacyGameserverSteamid { get; set; }
}