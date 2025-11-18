
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_ShowMenu : ITypedProtobuf<CCSUsrMsg_ShowMenu>, INetMessage<CCSUsrMsg_ShowMenu>, IDisposable
{
  static int INetMessage<CCSUsrMsg_ShowMenu>.MessageId => 354;
  
  static string INetMessage<CCSUsrMsg_ShowMenu>.MessageName => "CCSUsrMsg_ShowMenu";

  static CCSUsrMsg_ShowMenu ITypedProtobuf<CCSUsrMsg_ShowMenu>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_ShowMenuImpl(handle, isManuallyAllocated);


  public int BitsValidSlots { get; set; }


  public int DisplayTime { get; set; }


  public string MenuString { get; set; }

}
