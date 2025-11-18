
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgLegacySource1ClientWelcome_Location : ITypedProtobuf<CMsgLegacySource1ClientWelcome_Location>
{
  static CMsgLegacySource1ClientWelcome_Location ITypedProtobuf<CMsgLegacySource1ClientWelcome_Location>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgLegacySource1ClientWelcome_LocationImpl(handle, isManuallyAllocated);


  public float Latitude { get; set; }


  public float Longitude { get; set; }


  public string Country { get; set; }

}
