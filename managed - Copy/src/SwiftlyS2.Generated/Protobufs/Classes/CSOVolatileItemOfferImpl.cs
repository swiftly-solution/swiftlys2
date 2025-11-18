
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOVolatileItemOfferImpl : TypedProtobuf<CSOVolatileItemOffer>, CSOVolatileItemOffer
{
  public CSOVolatileItemOfferImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Defidx
  { get => Accessor.GetUInt32("defidx"); set => Accessor.SetUInt32("defidx", value); }


  public IProtobufRepeatedFieldValueType<ulong> FauxItemid
  { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "faux_itemid"); }


  public IProtobufRepeatedFieldValueType<uint> GenerationTime
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "generation_time"); }

}
