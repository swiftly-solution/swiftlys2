
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSource2VProfLiteReportItem : ITypedProtobuf<CMsgSource2VProfLiteReportItem>
{
  static CMsgSource2VProfLiteReportItem ITypedProtobuf<CMsgSource2VProfLiteReportItem>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSource2VProfLiteReportItemImpl(handle, isManuallyAllocated);


  public string Name { get; set; }


  public uint ActiveSamples { get; set; }


  public uint ActiveSamples1secmax { get; set; }


  public uint UsecMax { get; set; }


  public uint UsecAvgActive { get; set; }


  public uint UsecP50Active { get; set; }


  public uint UsecP99Active { get; set; }


  public uint UsecAvgAll { get; set; }


  public uint UsecP50All { get; set; }


  public uint UsecP99All { get; set; }


  public uint Usec1secmaxAvgActive { get; set; }


  public uint Usec1secmaxP50Active { get; set; }


  public uint Usec1secmaxP95Active { get; set; }


  public uint Usec1secmaxP99Active { get; set; }


  public uint Usec1secmaxAvgAll { get; set; }


  public uint Usec1secmaxP50All { get; set; }


  public uint Usec1secmaxP95All { get; set; }


  public uint Usec1secmaxP99All { get; set; }

}
