
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCMultiplexMessageImpl : TypedProtobuf<CMsgGCMultiplexMessage>, CMsgGCMultiplexMessage
{
  public CMsgGCMultiplexMessageImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Msgtype
  { get => Accessor.GetUInt32("msgtype"); set => Accessor.SetUInt32("msgtype", value); }


  public byte[] Payload
  { get => Accessor.GetBytes("payload"); set => Accessor.SetBytes("payload", value); }


  public IProtobufRepeatedFieldValueType<ulong> Steamids
  { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "steamids"); }


  public bool Replytogc
  { get => Accessor.GetBool("replytogc"); set => Accessor.SetBool("replytogc", value); }

}
