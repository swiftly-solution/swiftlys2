
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageSayText2 : ITypedProtobuf<CUserMessageSayText2>, INetMessage<CUserMessageSayText2>, IDisposable
{
  static int INetMessage<CUserMessageSayText2>.MessageId => 118;
  
  static string INetMessage<CUserMessageSayText2>.MessageName => "CUserMessageSayText2";

  static CUserMessageSayText2 ITypedProtobuf<CUserMessageSayText2>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageSayText2Impl(handle, isManuallyAllocated);


  public int Entityindex { get; set; }


  public bool Chat { get; set; }


  public string Messagename { get; set; }


  public string Param1 { get; set; }


  public string Param2 { get; set; }


  public string Param3 { get; set; }


  public string Param4 { get; set; }

}
