
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageCloseCaptionPlaceholder : ITypedProtobuf<CUserMessageCloseCaptionPlaceholder>, INetMessage<CUserMessageCloseCaptionPlaceholder>, IDisposable
{
  static int INetMessage<CUserMessageCloseCaptionPlaceholder>.MessageId => 142;
  
  static string INetMessage<CUserMessageCloseCaptionPlaceholder>.MessageName => "CUserMessageCloseCaptionPlaceholder";

  static CUserMessageCloseCaptionPlaceholder ITypedProtobuf<CUserMessageCloseCaptionPlaceholder>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageCloseCaptionPlaceholderImpl(handle, isManuallyAllocated);


  public string String { get; set; }


  public float Duration { get; set; }


  public bool FromPlayer { get; set; }


  public int EntIndex { get; set; }

}
