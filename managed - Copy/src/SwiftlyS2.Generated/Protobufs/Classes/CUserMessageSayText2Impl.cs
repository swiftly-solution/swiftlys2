
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageSayText2Impl : NetMessage<CUserMessageSayText2>, CUserMessageSayText2
{
  public CUserMessageSayText2Impl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Entityindex
  { get => Accessor.GetInt32("entityindex"); set => Accessor.SetInt32("entityindex", value); }


  public bool Chat
  { get => Accessor.GetBool("chat"); set => Accessor.SetBool("chat", value); }


  public string Messagename
  { get => Accessor.GetString("messagename"); set => Accessor.SetString("messagename", value); }


  public string Param1
  { get => Accessor.GetString("param1"); set => Accessor.SetString("param1", value); }


  public string Param2
  { get => Accessor.GetString("param2"); set => Accessor.SetString("param2", value); }


  public string Param3
  { get => Accessor.GetString("param3"); set => Accessor.SetString("param3", value); }


  public string Param4
  { get => Accessor.GetString("param4"); set => Accessor.SetString("param4", value); }

}
