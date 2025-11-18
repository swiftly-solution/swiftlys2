
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTESparksImpl : NetMessage<CMsgTESparks>, CMsgTESparks
{
  public CMsgTESparksImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public Vector Origin
  { get => Accessor.GetVector("origin"); set => Accessor.SetVector("origin", value); }


  public uint Magnitude
  { get => Accessor.GetUInt32("magnitude"); set => Accessor.SetUInt32("magnitude", value); }


  public uint Length
  { get => Accessor.GetUInt32("length"); set => Accessor.SetUInt32("length", value); }


  public Vector Direction
  { get => Accessor.GetVector("direction"); set => Accessor.SetVector("direction", value); }

}
