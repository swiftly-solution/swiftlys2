
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSUsrMsg_VGUIMenu_Keys : ITypedProtobuf<CCSUsrMsg_VGUIMenu_Keys>
{
  static CCSUsrMsg_VGUIMenu_Keys ITypedProtobuf<CCSUsrMsg_VGUIMenu_Keys>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_VGUIMenu_KeysImpl(handle, isManuallyAllocated);


  public string Name { get; set; }


  public string Value { get; set; }

}
