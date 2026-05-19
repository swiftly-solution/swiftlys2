using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramDiagnostic : ITypedProtobuf<CMsgSteamDatagramDiagnostic>
{
    static CMsgSteamDatagramDiagnostic ITypedProtobuf<CMsgSteamDatagramDiagnostic>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramDiagnosticImpl(handle, isManuallyAllocated);

    public uint Severity { get; set; }
    public string Text { get; set; }
}