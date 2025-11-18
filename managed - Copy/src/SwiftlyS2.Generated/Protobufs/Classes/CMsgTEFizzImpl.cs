
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTEFizzImpl : NetMessage<CMsgTEFizz>, CMsgTEFizz
{
  public CMsgTEFizzImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Entity
  { get => Accessor.GetInt32("entity"); set => Accessor.SetInt32("entity", value); }


  public uint Density
  { get => Accessor.GetUInt32("density"); set => Accessor.SetUInt32("density", value); }


  public int Current
  { get => Accessor.GetInt32("current"); set => Accessor.SetInt32("current", value); }

}
