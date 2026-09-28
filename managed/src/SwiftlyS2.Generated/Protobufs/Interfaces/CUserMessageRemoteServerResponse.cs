using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMessageRemoteServerResponse : ITypedProtobuf<CUserMessageRemoteServerResponse>, INetMessage<CUserMessageRemoteServerResponse>, IDisposable
{
    static int INetMessage<CUserMessageRemoteServerResponse>.MessageId => 170;

    static string INetMessage<CUserMessageRemoteServerResponse>.MessageName => "CUserMessageRemoteServerResponse";

    static CUserMessageRemoteServerResponse ITypedProtobuf<CUserMessageRemoteServerResponse>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageRemoteServerResponseImpl(handle, isManuallyAllocated);

    public CUserMessageRemoteServerResponse_ECommandResult CommandResult { get; set; }
    public string Convar { get; set; }
    public string Results { get; set; }
}