
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_SendPlayerLoadout : ITypedProtobuf<CCSUsrMsg_SendPlayerLoadout>, INetMessage<CCSUsrMsg_SendPlayerLoadout>, IDisposable
{
  static int INetMessage<CCSUsrMsg_SendPlayerLoadout>.MessageId => 388;
  
  static string INetMessage<CCSUsrMsg_SendPlayerLoadout>.MessageName => "CCSUsrMsg_SendPlayerLoadout";

  static CCSUsrMsg_SendPlayerLoadout ITypedProtobuf<CCSUsrMsg_SendPlayerLoadout>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_SendPlayerLoadoutImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_SendPlayerLoadout_LoadoutItem> Loadout { get; }


  public int Playerslot { get; set; }

}
