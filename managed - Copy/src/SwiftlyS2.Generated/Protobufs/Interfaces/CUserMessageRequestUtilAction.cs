
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageRequestUtilAction : ITypedProtobuf<CUserMessageRequestUtilAction>, INetMessage<CUserMessageRequestUtilAction>, IDisposable
{
  static int INetMessage<CUserMessageRequestUtilAction>.MessageId => 157;
  
  static string INetMessage<CUserMessageRequestUtilAction>.MessageName => "CUserMessageRequestUtilAction";

  static CUserMessageRequestUtilAction ITypedProtobuf<CUserMessageRequestUtilAction>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageRequestUtilActionImpl(handle, isManuallyAllocated);


  public int Util1 { get; set; }


  public int Util2 { get; set; }


  public int Util3 { get; set; }


  public int Util4 { get; set; }


  public int Util5 { get; set; }

}
