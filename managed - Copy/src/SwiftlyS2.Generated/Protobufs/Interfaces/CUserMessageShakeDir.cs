
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageShakeDir : ITypedProtobuf<CUserMessageShakeDir>, INetMessage<CUserMessageShakeDir>, IDisposable
{
  static int INetMessage<CUserMessageShakeDir>.MessageId => 121;
  
  static string INetMessage<CUserMessageShakeDir>.MessageName => "CUserMessageShakeDir";

  static CUserMessageShakeDir ITypedProtobuf<CUserMessageShakeDir>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageShakeDirImpl(handle, isManuallyAllocated);


  public CUserMessageShake Shake { get; }


  public Vector Direction { get; set; }

}
