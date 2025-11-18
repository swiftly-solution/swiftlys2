using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Natives.NativeObjects;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Core.NetMessages;

internal class NetMessage<T> : TypedProtobuf<T>, INativeHandle, IDisposable where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable {

  private NetMessageAllocableNativeHandle? _allocatedHandle;

  private CRecipientFilter _filter = new();

  public ref CRecipientFilter Recipients { get => ref _filter; }

  /*

  Evil hack to convert a CNetMessagePB<>* to google::protobuf::Message*

  CNetMessage memory layout:
  vtable -> 8 bytes
  members -> 40 bytes

  and CNetMessagePB inheritance: public CNetMessage, public PROTO_TYPE
  so we can cast to PROTO_TYPE* by adding 48 bytes to the handle

  */
  public NetMessage(nint handle, bool isManuallyAllocated) : base(handle + 48) {
    if (isManuallyAllocated) {
      _allocatedHandle = new(handle);
    }
  }

  public void Dispose() {
    _allocatedHandle?.Dispose();
  }

  private void CheckIsManuallyAllocated() {
    if (_allocatedHandle == null) {
      throw new InvalidOperationException("Send methods is not available for externally allocated net messages.");
    }
  }

  public void Send() {
    CheckIsManuallyAllocated();
    NativeNetMessages.SendMessageToPlayers(_allocatedHandle!.Address, T.MessageId, _filter.ToMask());
  }

  public void SendToAllPlayers()
  {
    CheckIsManuallyAllocated();
    _filter.AddAllPlayers();
    NativeNetMessages.SendMessageToPlayers(_allocatedHandle!.Address, T.MessageId, _filter.ToMask());
  }

  public void SendToPlayer(int playerId)
  {
    CheckIsManuallyAllocated();
    _filter.AddRecipient(playerId);
    NativeNetMessages.SendMessageToPlayers(_allocatedHandle!.Address, T.MessageId, _filter.ToMask());
  }

}