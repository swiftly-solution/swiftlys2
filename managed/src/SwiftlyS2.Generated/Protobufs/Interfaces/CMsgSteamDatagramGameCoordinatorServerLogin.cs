using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramGameCoordinatorServerLogin : ITypedProtobuf<CMsgSteamDatagramGameCoordinatorServerLogin>
{
    static CMsgSteamDatagramGameCoordinatorServerLogin ITypedProtobuf<CMsgSteamDatagramGameCoordinatorServerLogin>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramGameCoordinatorServerLoginImpl(handle, isManuallyAllocated);

    public uint TimeGenerated { get; set; }
    public uint Appid { get; set; }
    public byte[] Routing { get; set; }
    public byte[] Appdata { get; set; }
    public byte[] LegacyIdentityBinary { get; set; }
    public string IdentityString { get; set; }
    public ulong DummySteamId { get; set; }
}