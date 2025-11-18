
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientPerfReport_EntryImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientPerfReport_Entry>, CMsgGCCStrike15_v2_ClientPerfReport_Entry
{
  public CMsgGCCStrike15_v2_ClientPerfReport_EntryImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Perfcounter
  { get => Accessor.GetUInt32("perfcounter"); set => Accessor.SetUInt32("perfcounter", value); }


  public uint Length
  { get => Accessor.GetUInt32("length"); set => Accessor.SetUInt32("length", value); }


  public byte[] Reference
  { get => Accessor.GetBytes("reference"); set => Accessor.SetBytes("reference", value); }


  public byte[] Actual
  { get => Accessor.GetBytes("actual"); set => Accessor.SetBytes("actual", value); }


  public uint Sourceid
  { get => Accessor.GetUInt32("sourceid"); set => Accessor.SetUInt32("sourceid", value); }


  public uint Status
  { get => Accessor.GetUInt32("status"); set => Accessor.SetUInt32("status", value); }

}
