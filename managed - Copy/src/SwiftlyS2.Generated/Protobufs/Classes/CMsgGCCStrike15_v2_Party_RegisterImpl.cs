
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_Party_RegisterImpl : TypedProtobuf<CMsgGCCStrike15_v2_Party_Register>, CMsgGCCStrike15_v2_Party_Register
{
  public CMsgGCCStrike15_v2_Party_RegisterImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Id
  { get => Accessor.GetUInt32("id"); set => Accessor.SetUInt32("id", value); }


  public uint Ver
  { get => Accessor.GetUInt32("ver"); set => Accessor.SetUInt32("ver", value); }


  public uint Apr
  { get => Accessor.GetUInt32("apr"); set => Accessor.SetUInt32("apr", value); }


  public uint Ark
  { get => Accessor.GetUInt32("ark"); set => Accessor.SetUInt32("ark", value); }


  public uint Nby
  { get => Accessor.GetUInt32("nby"); set => Accessor.SetUInt32("nby", value); }


  public uint Grp
  { get => Accessor.GetUInt32("grp"); set => Accessor.SetUInt32("grp", value); }


  public uint Slots
  { get => Accessor.GetUInt32("slots"); set => Accessor.SetUInt32("slots", value); }


  public uint Launcher
  { get => Accessor.GetUInt32("launcher"); set => Accessor.SetUInt32("launcher", value); }


  public uint GameType
  { get => Accessor.GetUInt32("game_type"); set => Accessor.SetUInt32("game_type", value); }

}
