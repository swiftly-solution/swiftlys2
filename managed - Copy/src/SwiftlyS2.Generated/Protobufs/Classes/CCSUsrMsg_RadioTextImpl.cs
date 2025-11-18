
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_RadioTextImpl : NetMessage<CCSUsrMsg_RadioText>, CCSUsrMsg_RadioText
{
  public CCSUsrMsg_RadioTextImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int MsgDst
  { get => Accessor.GetInt32("msg_dst"); set => Accessor.SetInt32("msg_dst", value); }


  public int Client
  { get => Accessor.GetInt32("client"); set => Accessor.SetInt32("client", value); }


  public string MsgName
  { get => Accessor.GetString("msg_name"); set => Accessor.SetString("msg_name", value); }


  public IProtobufRepeatedFieldValueType<string> Params
  { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "params"); }

}
