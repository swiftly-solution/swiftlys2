
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_RadioText : ITypedProtobuf<CCSUsrMsg_RadioText>, INetMessage<CCSUsrMsg_RadioText>, IDisposable
{
  static int INetMessage<CCSUsrMsg_RadioText>.MessageId => 322;
  
  static string INetMessage<CCSUsrMsg_RadioText>.MessageName => "CCSUsrMsg_RadioText";

  static CCSUsrMsg_RadioText ITypedProtobuf<CCSUsrMsg_RadioText>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_RadioTextImpl(handle, isManuallyAllocated);


  public int MsgDst { get; set; }


  public int Client { get; set; }


  public string MsgName { get; set; }


  public IProtobufRepeatedFieldValueType<string> Params { get; }

}
