
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageRequestDllStatusImpl : NetMessage<CUserMessageRequestDllStatus>, CUserMessageRequestDllStatus
{
  public CUserMessageRequestDllStatusImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public string DllAction
  { get => Accessor.GetString("dll_action"); set => Accessor.SetString("dll_action", value); }


  public bool FullReport
  { get => Accessor.GetBool("full_report"); set => Accessor.SetBool("full_report", value); }

}
