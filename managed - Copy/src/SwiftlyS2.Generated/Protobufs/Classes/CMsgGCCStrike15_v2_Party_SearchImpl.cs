
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_Party_SearchImpl : TypedProtobuf<CMsgGCCStrike15_v2_Party_Search>, CMsgGCCStrike15_v2_Party_Search
{
  public CMsgGCCStrike15_v2_Party_SearchImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Ver
  { get => Accessor.GetUInt32("ver"); set => Accessor.SetUInt32("ver", value); }


  public uint Apr
  { get => Accessor.GetUInt32("apr"); set => Accessor.SetUInt32("apr", value); }


  public uint Ark
  { get => Accessor.GetUInt32("ark"); set => Accessor.SetUInt32("ark", value); }


  public IProtobufRepeatedFieldValueType<uint> Grps
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "grps"); }


  public uint Launcher
  { get => Accessor.GetUInt32("launcher"); set => Accessor.SetUInt32("launcher", value); }


  public uint GameType
  { get => Accessor.GetUInt32("game_type"); set => Accessor.SetUInt32("game_type", value); }

}
