
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTEDecalImpl : NetMessage<CMsgTEDecal>, CMsgTEDecal
{
  public CMsgTEDecalImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public Vector Origin
  { get => Accessor.GetVector("origin"); set => Accessor.SetVector("origin", value); }


  public Vector Start
  { get => Accessor.GetVector("start"); set => Accessor.SetVector("start", value); }


  public int Entity
  { get => Accessor.GetInt32("entity"); set => Accessor.SetInt32("entity", value); }


  public uint Hitbox
  { get => Accessor.GetUInt32("hitbox"); set => Accessor.SetUInt32("hitbox", value); }


  public uint Index
  { get => Accessor.GetUInt32("index"); set => Accessor.SetUInt32("index", value); }

}
