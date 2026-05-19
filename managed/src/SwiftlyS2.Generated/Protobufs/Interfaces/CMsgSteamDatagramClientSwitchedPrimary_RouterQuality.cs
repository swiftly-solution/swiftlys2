using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramClientSwitchedPrimary_RouterQuality : ITypedProtobuf<CMsgSteamDatagramClientSwitchedPrimary_RouterQuality>
{
    static CMsgSteamDatagramClientSwitchedPrimary_RouterQuality ITypedProtobuf<CMsgSteamDatagramClientSwitchedPrimary_RouterQuality>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramClientSwitchedPrimary_RouterQualityImpl(handle, isManuallyAllocated);

    public uint Score { get; set; }
    public uint FrontPing { get; set; }
    public uint BackPing { get; set; }
    public uint SecondsUntilDown { get; set; }
}