
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoStringTables_items_tImpl : TypedProtobuf<CDemoStringTables_items_t>, CDemoStringTables_items_t
{
  public CDemoStringTables_items_tImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Str
  { get => Accessor.GetString("str"); set => Accessor.SetString("str", value); }


  public byte[] Data
  { get => Accessor.GetBytes("data"); set => Accessor.SetBytes("data", value); }

}
