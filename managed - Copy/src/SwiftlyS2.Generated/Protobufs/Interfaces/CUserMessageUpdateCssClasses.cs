
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageUpdateCssClasses : ITypedProtobuf<CUserMessageUpdateCssClasses>, INetMessage<CUserMessageUpdateCssClasses>, IDisposable
{
  static int INetMessage<CUserMessageUpdateCssClasses>.MessageId => 153;
  
  static string INetMessage<CUserMessageUpdateCssClasses>.MessageName => "CUserMessageUpdateCssClasses";

  static CUserMessageUpdateCssClasses ITypedProtobuf<CUserMessageUpdateCssClasses>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageUpdateCssClassesImpl(handle, isManuallyAllocated);


  public int TargetWorldPanel { get; set; }


  public string CssClasses { get; set; }


  public bool IsAdd { get; set; }

}
