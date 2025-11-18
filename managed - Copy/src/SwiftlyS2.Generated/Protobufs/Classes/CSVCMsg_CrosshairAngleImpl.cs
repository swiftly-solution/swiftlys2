
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_CrosshairAngleImpl : TypedProtobuf<CSVCMsg_CrosshairAngle>, CSVCMsg_CrosshairAngle
{
  public CSVCMsg_CrosshairAngleImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public QAngle Angle
  { get => Accessor.GetQAngle("angle"); set => Accessor.SetQAngle("angle", value); }

}
