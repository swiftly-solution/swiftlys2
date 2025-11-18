
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessage_DllStatus_CModuleImpl : TypedProtobuf<CUserMessage_DllStatus_CModule>, CUserMessage_DllStatus_CModule
{
  public CUserMessage_DllStatus_CModuleImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong BaseAddr
  { get => Accessor.GetUInt64("base_addr"); set => Accessor.SetUInt64("base_addr", value); }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public uint Size
  { get => Accessor.GetUInt32("size"); set => Accessor.SetUInt32("size", value); }


  public uint Timestamp
  { get => Accessor.GetUInt32("timestamp"); set => Accessor.SetUInt32("timestamp", value); }

}
