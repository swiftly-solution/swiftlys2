
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_ServerRankUpdate_RankUpdateImpl : TypedProtobuf<CCSUsrMsg_ServerRankUpdate_RankUpdate>, CCSUsrMsg_ServerRankUpdate_RankUpdate
{
  public CCSUsrMsg_ServerRankUpdate_RankUpdateImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int AccountId
  { get => Accessor.GetInt32("account_id"); set => Accessor.SetInt32("account_id", value); }


  public int RankOld
  { get => Accessor.GetInt32("rank_old"); set => Accessor.SetInt32("rank_old", value); }


  public int RankNew
  { get => Accessor.GetInt32("rank_new"); set => Accessor.SetInt32("rank_new", value); }


  public int NumWins
  { get => Accessor.GetInt32("num_wins"); set => Accessor.SetInt32("num_wins", value); }


  public float RankChange
  { get => Accessor.GetFloat("rank_change"); set => Accessor.SetFloat("rank_change", value); }


  public int RankTypeId
  { get => Accessor.GetInt32("rank_type_id"); set => Accessor.SetInt32("rank_type_id", value); }

}
