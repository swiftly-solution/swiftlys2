
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_TempEntitiesImpl : TypedProtobuf<CSVCMsg_TempEntities>, CSVCMsg_TempEntities
{
  public CSVCMsg_TempEntitiesImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool Reliable
  { get => Accessor.GetBool("reliable"); set => Accessor.SetBool("reliable", value); }


  public int NumEntries
  { get => Accessor.GetInt32("num_entries"); set => Accessor.SetInt32("num_entries", value); }


  public byte[] EntityData
  { get => Accessor.GetBytes("entity_data"); set => Accessor.SetBytes("entity_data", value); }

}
