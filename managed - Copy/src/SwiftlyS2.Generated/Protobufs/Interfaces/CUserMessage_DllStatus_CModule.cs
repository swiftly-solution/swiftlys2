
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMessage_DllStatus_CModule : ITypedProtobuf<CUserMessage_DllStatus_CModule>
{
  static CUserMessage_DllStatus_CModule ITypedProtobuf<CUserMessage_DllStatus_CModule>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessage_DllStatus_CModuleImpl(handle, isManuallyAllocated);


  public ulong BaseAddr { get; set; }


  public string Name { get; set; }


  public uint Size { get; set; }


  public uint Timestamp { get; set; }

}
