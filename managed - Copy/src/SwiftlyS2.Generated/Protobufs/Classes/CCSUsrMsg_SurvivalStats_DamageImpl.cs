
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_SurvivalStats_DamageImpl : TypedProtobuf<CCSUsrMsg_SurvivalStats_Damage>, CCSUsrMsg_SurvivalStats_Damage
{
  public CCSUsrMsg_SurvivalStats_DamageImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Xuid
  { get => Accessor.GetUInt64("xuid"); set => Accessor.SetUInt64("xuid", value); }


  public int To
  { get => Accessor.GetInt32("to"); set => Accessor.SetInt32("to", value); }


  public int ToHits
  { get => Accessor.GetInt32("to_hits"); set => Accessor.SetInt32("to_hits", value); }


  public int From
  { get => Accessor.GetInt32("from"); set => Accessor.SetInt32("from", value); }


  public int FromHits
  { get => Accessor.GetInt32("from_hits"); set => Accessor.SetInt32("from_hits", value); }

}
