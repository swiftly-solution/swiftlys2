
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CClientMsg_ClientUIEventImpl : TypedProtobuf<CClientMsg_ClientUIEvent>, CClientMsg_ClientUIEvent
{
  public CClientMsg_ClientUIEventImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public EClientUIEvent Event
  { get => (EClientUIEvent)Accessor.GetInt32("event"); set => Accessor.SetInt32("event", (int)value); }


  public uint EntEhandle
  { get => Accessor.GetUInt32("ent_ehandle"); set => Accessor.SetUInt32("ent_ehandle", value); }


  public uint ClientEhandle
  { get => Accessor.GetUInt32("client_ehandle"); set => Accessor.SetUInt32("client_ehandle", value); }


  public string Data1
  { get => Accessor.GetString("data1"); set => Accessor.SetString("data1", value); }


  public string Data2
  { get => Accessor.GetString("data2"); set => Accessor.SetString("data2", value); }

}
