
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTEImpactImpl : NetMessage<CMsgTEImpact>, CMsgTEImpact
{
  public CMsgTEImpactImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public Vector Origin
  { get => Accessor.GetVector("origin"); set => Accessor.SetVector("origin", value); }


  public Vector Normal
  { get => Accessor.GetVector("normal"); set => Accessor.SetVector("normal", value); }


  public uint Type
  { get => Accessor.GetUInt32("type"); set => Accessor.SetUInt32("type", value); }

}
