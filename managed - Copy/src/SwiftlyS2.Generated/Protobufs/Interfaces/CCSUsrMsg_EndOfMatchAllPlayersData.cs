
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_EndOfMatchAllPlayersData : ITypedProtobuf<CCSUsrMsg_EndOfMatchAllPlayersData>, INetMessage<CCSUsrMsg_EndOfMatchAllPlayersData>, IDisposable
{
  static int INetMessage<CCSUsrMsg_EndOfMatchAllPlayersData>.MessageId => 375;
  
  static string INetMessage<CCSUsrMsg_EndOfMatchAllPlayersData>.MessageName => "CCSUsrMsg_EndOfMatchAllPlayersData";

  static CCSUsrMsg_EndOfMatchAllPlayersData ITypedProtobuf<CCSUsrMsg_EndOfMatchAllPlayersData>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_EndOfMatchAllPlayersDataImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_EndOfMatchAllPlayersData_PlayerData> Allplayerdata { get; }


  public int Scene { get; set; }

}
