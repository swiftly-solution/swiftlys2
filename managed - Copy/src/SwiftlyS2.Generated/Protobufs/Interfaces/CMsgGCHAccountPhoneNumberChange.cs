
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCHAccountPhoneNumberChange : ITypedProtobuf<CMsgGCHAccountPhoneNumberChange>
{
  static CMsgGCHAccountPhoneNumberChange ITypedProtobuf<CMsgGCHAccountPhoneNumberChange>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCHAccountPhoneNumberChangeImpl(handle, isManuallyAllocated);


  public ulong Steamid { get; set; }


  public uint Appid { get; set; }


  public ulong PhoneId { get; set; }


  public bool IsVerified { get; set; }


  public bool IsIdentifying { get; set; }

}
