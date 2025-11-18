
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoFullPacketImpl : TypedProtobuf<CDemoFullPacket>, CDemoFullPacket
{
  public CDemoFullPacketImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public CDemoStringTables StringTable
  { get => new CDemoStringTablesImpl(NativeNetMessages.GetNestedMessage(Address, "string_table"), false); }


  public CDemoPacket Packet
  { get => new CDemoPacketImpl(NativeNetMessages.GetNestedMessage(Address, "packet"), false); }

}
