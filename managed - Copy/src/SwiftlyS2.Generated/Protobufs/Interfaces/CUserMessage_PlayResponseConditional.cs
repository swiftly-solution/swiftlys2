
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessage_PlayResponseConditional : ITypedProtobuf<CUserMessage_PlayResponseConditional>, INetMessage<CUserMessage_PlayResponseConditional>, IDisposable
{
  static int INetMessage<CUserMessage_PlayResponseConditional>.MessageId => 166;
  
  static string INetMessage<CUserMessage_PlayResponseConditional>.MessageName => "CUserMessage_PlayResponseConditional";

  static CUserMessage_PlayResponseConditional ITypedProtobuf<CUserMessage_PlayResponseConditional>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessage_PlayResponseConditionalImpl(handle, isManuallyAllocated);


  public int EntIndex { get; set; }


  public IProtobufRepeatedFieldValueType<int> PlayerSlots { get; }


  public string Response { get; set; }


  public Vector EntOrigin { get; set; }


  public float PreDelay { get; set; }


  public int MixPriority { get; set; }

}
