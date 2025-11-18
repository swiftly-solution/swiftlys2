
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_HLTVStatusImpl : NetMessage<CSVCMsg_HLTVStatus>, CSVCMsg_HLTVStatus
{
  public CSVCMsg_HLTVStatusImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public string Master
  { get => Accessor.GetString("master"); set => Accessor.SetString("master", value); }


  public int Clients
  { get => Accessor.GetInt32("clients"); set => Accessor.SetInt32("clients", value); }


  public int Slots
  { get => Accessor.GetInt32("slots"); set => Accessor.SetInt32("slots", value); }


  public int Proxies
  { get => Accessor.GetInt32("proxies"); set => Accessor.SetInt32("proxies", value); }

}
