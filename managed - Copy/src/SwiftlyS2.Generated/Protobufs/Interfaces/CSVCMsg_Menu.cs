
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_Menu : ITypedProtobuf<CSVCMsg_Menu>, INetMessage<CSVCMsg_Menu>, IDisposable
{
  static int INetMessage<CSVCMsg_Menu>.MessageId => 57;
  
  static string INetMessage<CSVCMsg_Menu>.MessageName => "CSVCMsg_Menu";

  static CSVCMsg_Menu ITypedProtobuf<CSVCMsg_Menu>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_MenuImpl(handle, isManuallyAllocated);


  public int DialogType { get; set; }


  public byte[] MenuKeyValues { get; set; }

}
