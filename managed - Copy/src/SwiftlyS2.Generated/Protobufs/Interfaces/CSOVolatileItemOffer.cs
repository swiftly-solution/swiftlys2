
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOVolatileItemOffer : ITypedProtobuf<CSOVolatileItemOffer>
{
  static CSOVolatileItemOffer ITypedProtobuf<CSOVolatileItemOffer>.Wrap(nint handle, bool isManuallyAllocated) => new CSOVolatileItemOfferImpl(handle, isManuallyAllocated);


  public uint Defidx { get; set; }


  public IProtobufRepeatedFieldValueType<ulong> FauxItemid { get; }


  public IProtobufRepeatedFieldValueType<uint> GenerationTime { get; }

}
