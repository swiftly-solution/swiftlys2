
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgClientWelcome_Location : ITypedProtobuf<CMsgClientWelcome_Location>
{
  static CMsgClientWelcome_Location ITypedProtobuf<CMsgClientWelcome_Location>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgClientWelcome_LocationImpl(handle, isManuallyAllocated);


  public float Latitude { get; set; }


  public float Longitude { get; set; }


  public string Country { get; set; }

}
