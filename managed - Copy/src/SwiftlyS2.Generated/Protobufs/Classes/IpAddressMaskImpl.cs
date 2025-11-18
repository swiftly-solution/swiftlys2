
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class IpAddressMaskImpl : TypedProtobuf<IpAddressMask>, IpAddressMask
{
  public IpAddressMaskImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint A
  { get => Accessor.GetUInt32("a"); set => Accessor.SetUInt32("a", value); }


  public uint B
  { get => Accessor.GetUInt32("b"); set => Accessor.SetUInt32("b", value); }


  public uint C
  { get => Accessor.GetUInt32("c"); set => Accessor.SetUInt32("c", value); }


  public uint D
  { get => Accessor.GetUInt32("d"); set => Accessor.SetUInt32("d", value); }


  public uint Bits
  { get => Accessor.GetUInt32("bits"); set => Accessor.SetUInt32("bits", value); }


  public uint Token
  { get => Accessor.GetUInt32("token"); set => Accessor.SetUInt32("token", value); }

}
