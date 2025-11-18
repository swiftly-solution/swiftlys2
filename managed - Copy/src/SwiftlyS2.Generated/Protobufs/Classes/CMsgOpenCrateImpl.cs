
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgOpenCrateImpl : TypedProtobuf<CMsgOpenCrate>, CMsgOpenCrate
{
  public CMsgOpenCrateImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong ToolItemId
  { get => Accessor.GetUInt64("tool_item_id"); set => Accessor.SetUInt64("tool_item_id", value); }


  public ulong SubjectItemId
  { get => Accessor.GetUInt64("subject_item_id"); set => Accessor.SetUInt64("subject_item_id", value); }


  public bool ForRental
  { get => Accessor.GetBool("for_rental"); set => Accessor.SetBool("for_rental", value); }


  public uint PointsRemaining
  { get => Accessor.GetUInt32("points_remaining"); set => Accessor.SetUInt32("points_remaining", value); }

}
