
using SwiftlyS2.Core.ProtobufDefinitions;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_DisconnectToLobby : ITypedProtobuf<CCSUsrMsg_DisconnectToLobby>, INetMessage<CCSUsrMsg_DisconnectToLobby>, IDisposable
{
    static int INetMessage<CCSUsrMsg_DisconnectToLobby>.MessageId => 335;

    static string INetMessage<CCSUsrMsg_DisconnectToLobby>.MessageName => "CCSUsrMsg_DisconnectToLobby";

    static CCSUsrMsg_DisconnectToLobby ITypedProtobuf<CCSUsrMsg_DisconnectToLobby>.Wrap( nint handle, bool isManuallyAllocated ) => new CCSUsrMsg_DisconnectToLobbyImpl(handle, isManuallyAllocated);


    public int Dummy { get; set; }

}
