using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramDiagnosticImpl : TypedProtobuf<CMsgSteamDatagramDiagnostic>, CMsgSteamDatagramDiagnostic
{
    public CMsgSteamDatagramDiagnosticImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Severity
    { get => Accessor.GetUInt32("severity"); set => Accessor.SetUInt32("severity", value); }
    public string Text
    { get => Accessor.GetString("text"); set => Accessor.SetString("text", value); }
}