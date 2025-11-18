
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageHapticsManagerPulse : ITypedProtobuf<CUserMessageHapticsManagerPulse>, INetMessage<CUserMessageHapticsManagerPulse>, IDisposable
{
  static int INetMessage<CUserMessageHapticsManagerPulse>.MessageId => 150;
  
  static string INetMessage<CUserMessageHapticsManagerPulse>.MessageName => "CUserMessageHapticsManagerPulse";

  static CUserMessageHapticsManagerPulse ITypedProtobuf<CUserMessageHapticsManagerPulse>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageHapticsManagerPulseImpl(handle, isManuallyAllocated);


  public int HandId { get; set; }


  public float EffectAmplitude { get; set; }


  public float EffectFrequency { get; set; }


  public float EffectDuration { get; set; }

}
