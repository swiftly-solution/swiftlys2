
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_GotvSyncPacketImpl : TypedProtobuf<CMsgGCCStrike15_GotvSyncPacket>, CMsgGCCStrike15_GotvSyncPacket
{
  public CMsgGCCStrike15_GotvSyncPacketImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public CEngineGotvSyncPacket Data
  { get => new CEngineGotvSyncPacketImpl(NativeNetMessages.GetNestedMessage(Address, "data"), false); }

}
