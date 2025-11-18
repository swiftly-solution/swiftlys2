
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOEconItemAttributeImpl : TypedProtobuf<CSOEconItemAttribute>, CSOEconItemAttribute
{
  public CSOEconItemAttributeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint DefIndex
  { get => Accessor.GetUInt32("def_index"); set => Accessor.SetUInt32("def_index", value); }


  public uint Value
  { get => Accessor.GetUInt32("value"); set => Accessor.SetUInt32("value", value); }


  public byte[] ValueBytes
  { get => Accessor.GetBytes("value_bytes"); set => Accessor.SetBytes("value_bytes", value); }

}
