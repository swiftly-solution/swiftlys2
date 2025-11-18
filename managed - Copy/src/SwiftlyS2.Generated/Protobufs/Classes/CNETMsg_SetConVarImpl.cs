
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CNETMsg_SetConVarImpl : NetMessage<CNETMsg_SetConVar>, CNETMsg_SetConVar
{
  public CNETMsg_SetConVarImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public CMsg_CVars Convars
  { get => new CMsg_CVarsImpl(NativeNetMessages.GetNestedMessage(Address, "convars"), false); }

}
