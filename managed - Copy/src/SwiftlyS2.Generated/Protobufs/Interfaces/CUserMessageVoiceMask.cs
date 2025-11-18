
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageVoiceMask : ITypedProtobuf<CUserMessageVoiceMask>, INetMessage<CUserMessageVoiceMask>, IDisposable
{
  static int INetMessage<CUserMessageVoiceMask>.MessageId => 128;
  
  static string INetMessage<CUserMessageVoiceMask>.MessageName => "CUserMessageVoiceMask";

  static CUserMessageVoiceMask ITypedProtobuf<CUserMessageVoiceMask>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageVoiceMaskImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldValueType<uint> GamerulesMasks { get; }


  public IProtobufRepeatedFieldValueType<uint> BanMasks { get; }


  public bool ModEnable { get; set; }

}
