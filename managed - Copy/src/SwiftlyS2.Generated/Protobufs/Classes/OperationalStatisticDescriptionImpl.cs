
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class OperationalStatisticDescriptionImpl : TypedProtobuf<OperationalStatisticDescription>, OperationalStatisticDescription
{
  public OperationalStatisticDescriptionImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public uint Idkey
  { get => Accessor.GetUInt32("idkey"); set => Accessor.SetUInt32("idkey", value); }

}
