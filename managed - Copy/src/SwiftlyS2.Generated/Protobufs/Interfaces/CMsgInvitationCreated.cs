
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgInvitationCreated : ITypedProtobuf<CMsgInvitationCreated>
{
  static CMsgInvitationCreated ITypedProtobuf<CMsgInvitationCreated>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgInvitationCreatedImpl(handle, isManuallyAllocated);


  public ulong GroupId { get; set; }


  public ulong SteamId { get; set; }

}
