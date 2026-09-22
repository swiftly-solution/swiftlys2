using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMessage_RemoteServerCommand : ITypedProtobuf<CUserMessage_RemoteServerCommand>, INetMessage<CUserMessage_RemoteServerCommand>, IDisposable
{
    static int INetMessage<CUserMessage_RemoteServerCommand>.MessageId => 169;

    static string INetMessage<CUserMessage_RemoteServerCommand>.MessageName => "CUserMessage_RemoteServerCommand";

    static CUserMessage_RemoteServerCommand ITypedProtobuf<CUserMessage_RemoteServerCommand>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessage_RemoteServerCommandImpl(handle, isManuallyAllocated);

    public CUserMessage_RemoteServerCommand_ECommand Command { get; set; }
    public string Convar { get; set; }
    public string Value { get; set; }
}