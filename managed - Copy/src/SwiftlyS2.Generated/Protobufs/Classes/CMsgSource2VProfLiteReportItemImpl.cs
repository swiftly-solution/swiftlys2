
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSource2VProfLiteReportItemImpl : TypedProtobuf<CMsgSource2VProfLiteReportItem>, CMsgSource2VProfLiteReportItem
{
  public CMsgSource2VProfLiteReportItemImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public uint ActiveSamples
  { get => Accessor.GetUInt32("active_samples"); set => Accessor.SetUInt32("active_samples", value); }


  public uint ActiveSamples1secmax
  { get => Accessor.GetUInt32("active_samples_1secmax"); set => Accessor.SetUInt32("active_samples_1secmax", value); }


  public uint UsecMax
  { get => Accessor.GetUInt32("usec_max"); set => Accessor.SetUInt32("usec_max", value); }


  public uint UsecAvgActive
  { get => Accessor.GetUInt32("usec_avg_active"); set => Accessor.SetUInt32("usec_avg_active", value); }


  public uint UsecP50Active
  { get => Accessor.GetUInt32("usec_p50_active"); set => Accessor.SetUInt32("usec_p50_active", value); }


  public uint UsecP99Active
  { get => Accessor.GetUInt32("usec_p99_active"); set => Accessor.SetUInt32("usec_p99_active", value); }


  public uint UsecAvgAll
  { get => Accessor.GetUInt32("usec_avg_all"); set => Accessor.SetUInt32("usec_avg_all", value); }


  public uint UsecP50All
  { get => Accessor.GetUInt32("usec_p50_all"); set => Accessor.SetUInt32("usec_p50_all", value); }


  public uint UsecP99All
  { get => Accessor.GetUInt32("usec_p99_all"); set => Accessor.SetUInt32("usec_p99_all", value); }


  public uint Usec1secmaxAvgActive
  { get => Accessor.GetUInt32("usec_1secmax_avg_active"); set => Accessor.SetUInt32("usec_1secmax_avg_active", value); }


  public uint Usec1secmaxP50Active
  { get => Accessor.GetUInt32("usec_1secmax_p50_active"); set => Accessor.SetUInt32("usec_1secmax_p50_active", value); }


  public uint Usec1secmaxP95Active
  { get => Accessor.GetUInt32("usec_1secmax_p95_active"); set => Accessor.SetUInt32("usec_1secmax_p95_active", value); }


  public uint Usec1secmaxP99Active
  { get => Accessor.GetUInt32("usec_1secmax_p99_active"); set => Accessor.SetUInt32("usec_1secmax_p99_active", value); }


  public uint Usec1secmaxAvgAll
  { get => Accessor.GetUInt32("usec_1secmax_avg_all"); set => Accessor.SetUInt32("usec_1secmax_avg_all", value); }


  public uint Usec1secmaxP50All
  { get => Accessor.GetUInt32("usec_1secmax_p50_all"); set => Accessor.SetUInt32("usec_1secmax_p50_all", value); }


  public uint Usec1secmaxP95All
  { get => Accessor.GetUInt32("usec_1secmax_p95_all"); set => Accessor.SetUInt32("usec_1secmax_p95_all", value); }


  public uint Usec1secmaxP99All
  { get => Accessor.GetUInt32("usec_1secmax_p99_all"); set => Accessor.SetUInt32("usec_1secmax_p99_all", value); }

}
