
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface PlayerMedalsInfo : ITypedProtobuf<PlayerMedalsInfo>
{
  static PlayerMedalsInfo ITypedProtobuf<PlayerMedalsInfo>.Wrap(nint handle, bool isManuallyAllocated) => new PlayerMedalsInfoImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldValueType<uint> DisplayItemsDefidx { get; }


  public uint FeaturedDisplayItemDefidx { get; set; }

}
