
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface IpAddressMask : ITypedProtobuf<IpAddressMask>
{
  static IpAddressMask ITypedProtobuf<IpAddressMask>.Wrap(nint handle, bool isManuallyAllocated) => new IpAddressMaskImpl(handle, isManuallyAllocated);


  public uint A { get; set; }


  public uint B { get; set; }


  public uint C { get; set; }


  public uint D { get; set; }


  public uint Bits { get; set; }


  public uint Token { get; set; }

}
