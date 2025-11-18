
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageShakeDirImpl : NetMessage<CUserMessageShakeDir>, CUserMessageShakeDir
{
  public CUserMessageShakeDirImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public CUserMessageShake Shake
  { get => new CUserMessageShakeImpl(NativeNetMessages.GetNestedMessage(Address, "shake"), false); }


  public Vector Direction
  { get => Accessor.GetVector("direction"); set => Accessor.SetVector("direction", value); }

}
