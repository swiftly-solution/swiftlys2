
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CInButtonStatePB : ITypedProtobuf<CInButtonStatePB>
{
  static CInButtonStatePB ITypedProtobuf<CInButtonStatePB>.Wrap(nint handle, bool isManuallyAllocated) => new CInButtonStatePBImpl(handle, isManuallyAllocated);


  public ulong Buttonstate1 { get; set; }


  public ulong Buttonstate2 { get; set; }


  public ulong Buttonstate3 { get; set; }

}
