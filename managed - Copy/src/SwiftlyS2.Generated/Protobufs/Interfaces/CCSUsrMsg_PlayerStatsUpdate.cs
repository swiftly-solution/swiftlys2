
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_PlayerStatsUpdate : ITypedProtobuf<CCSUsrMsg_PlayerStatsUpdate>, INetMessage<CCSUsrMsg_PlayerStatsUpdate>, IDisposable
{
  static int INetMessage<CCSUsrMsg_PlayerStatsUpdate>.MessageId => 336;
  
  static string INetMessage<CCSUsrMsg_PlayerStatsUpdate>.MessageName => "CCSUsrMsg_PlayerStatsUpdate";

  static CCSUsrMsg_PlayerStatsUpdate ITypedProtobuf<CCSUsrMsg_PlayerStatsUpdate>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_PlayerStatsUpdateImpl(handle, isManuallyAllocated);


  public int Version { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_PlayerStatsUpdate_Stat> Stats { get; }


  public uint Ehandle { get; set; }


  public int Crc { get; set; }

}
