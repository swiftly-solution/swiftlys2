
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CClientMsg_ClientUIEvent : ITypedProtobuf<CClientMsg_ClientUIEvent>
{
  static CClientMsg_ClientUIEvent ITypedProtobuf<CClientMsg_ClientUIEvent>.Wrap(nint handle, bool isManuallyAllocated) => new CClientMsg_ClientUIEventImpl(handle, isManuallyAllocated);


  public EClientUIEvent Event { get; set; }


  public uint EntEhandle { get; set; }


  public uint ClientEhandle { get; set; }


  public string Data1 { get; set; }


  public string Data2 { get; set; }

}
