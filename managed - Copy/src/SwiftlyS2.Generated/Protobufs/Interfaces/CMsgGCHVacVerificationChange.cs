
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCHVacVerificationChange : ITypedProtobuf<CMsgGCHVacVerificationChange>
{
  static CMsgGCHVacVerificationChange ITypedProtobuf<CMsgGCHVacVerificationChange>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCHVacVerificationChangeImpl(handle, isManuallyAllocated);


  public ulong Steamid { get; set; }


  public uint Appid { get; set; }


  public bool IsVerified { get; set; }

}
