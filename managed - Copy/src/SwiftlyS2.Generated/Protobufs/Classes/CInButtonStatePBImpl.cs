
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CInButtonStatePBImpl : TypedProtobuf<CInButtonStatePB>, CInButtonStatePB
{
  public CInButtonStatePBImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Buttonstate1
  { get => Accessor.GetUInt64("buttonstate1"); set => Accessor.SetUInt64("buttonstate1", value); }


  public ulong Buttonstate2
  { get => Accessor.GetUInt64("buttonstate2"); set => Accessor.SetUInt64("buttonstate2", value); }


  public ulong Buttonstate3
  { get => Accessor.GetUInt64("buttonstate3"); set => Accessor.SetUInt64("buttonstate3", value); }

}
