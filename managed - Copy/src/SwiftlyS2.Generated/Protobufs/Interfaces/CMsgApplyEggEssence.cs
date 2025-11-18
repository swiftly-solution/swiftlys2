
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgApplyEggEssence : ITypedProtobuf<CMsgApplyEggEssence>
{
  static CMsgApplyEggEssence ITypedProtobuf<CMsgApplyEggEssence>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgApplyEggEssenceImpl(handle, isManuallyAllocated);


  public ulong EssenceItemId { get; set; }


  public ulong EggItemId { get; set; }

}
