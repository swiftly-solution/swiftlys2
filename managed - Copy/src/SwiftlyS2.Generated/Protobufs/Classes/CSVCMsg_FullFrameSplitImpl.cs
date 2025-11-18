
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_FullFrameSplitImpl : NetMessage<CSVCMsg_FullFrameSplit>, CSVCMsg_FullFrameSplit
{
  public CSVCMsg_FullFrameSplitImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Tick
  { get => Accessor.GetInt32("tick"); set => Accessor.SetInt32("tick", value); }


  public int Section
  { get => Accessor.GetInt32("section"); set => Accessor.SetInt32("section", value); }


  public int Total
  { get => Accessor.GetInt32("total"); set => Accessor.SetInt32("total", value); }


  public byte[] Data
  { get => Accessor.GetBytes("data"); set => Accessor.SetBytes("data", value); }

}
