
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTELargeFunnelImpl : NetMessage<CMsgTELargeFunnel>, CMsgTELargeFunnel
{
  public CMsgTELargeFunnelImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public Vector Origin
  { get => Accessor.GetVector("origin"); set => Accessor.SetVector("origin", value); }


  public uint Reversed
  { get => Accessor.GetUInt32("reversed"); set => Accessor.SetUInt32("reversed", value); }

}
