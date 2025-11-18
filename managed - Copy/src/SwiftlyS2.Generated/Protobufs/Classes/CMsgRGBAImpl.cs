
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgRGBAImpl : TypedProtobuf<CMsgRGBA>, CMsgRGBA
{
  public CMsgRGBAImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int R
  { get => Accessor.GetInt32("r"); set => Accessor.SetInt32("r", value); }


  public int G
  { get => Accessor.GetInt32("g"); set => Accessor.SetInt32("g", value); }


  public int B
  { get => Accessor.GetInt32("b"); set => Accessor.SetInt32("b", value); }


  public int A
  { get => Accessor.GetInt32("a"); set => Accessor.SetInt32("a", value); }

}
