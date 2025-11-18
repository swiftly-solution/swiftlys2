
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCDev_SchemaReservationRequestImpl : TypedProtobuf<CMsgGCDev_SchemaReservationRequest>, CMsgGCDev_SchemaReservationRequest
{
  public CMsgGCDev_SchemaReservationRequestImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string SchemaTypename
  { get => Accessor.GetString("schema_typename"); set => Accessor.SetString("schema_typename", value); }


  public string InstanceName
  { get => Accessor.GetString("instance_name"); set => Accessor.SetString("instance_name", value); }


  public ulong Context
  { get => Accessor.GetUInt64("context"); set => Accessor.SetUInt64("context", value); }


  public ulong Id
  { get => Accessor.GetUInt64("id"); set => Accessor.SetUInt64("id", value); }

}
