
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgStoreGetUserData : ITypedProtobuf<CMsgStoreGetUserData>
{
  static CMsgStoreGetUserData ITypedProtobuf<CMsgStoreGetUserData>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgStoreGetUserDataImpl(handle, isManuallyAllocated);


  public uint PriceSheetVersion { get; set; }


  public int Currency { get; set; }

}
