
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSource2SystemSpecs : ITypedProtobuf<CMsgSource2SystemSpecs>
{
  static CMsgSource2SystemSpecs ITypedProtobuf<CMsgSource2SystemSpecs>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSource2SystemSpecsImpl(handle, isManuallyAllocated);


  public string CpuId { get; set; }


  public string CpuBrand { get; set; }


  public uint CpuModel { get; set; }


  public uint CpuNumPhysical { get; set; }


  public uint RamPhysicalTotalMb { get; set; }


  public string GpuRendersystemDllName { get; set; }


  public uint GpuVendorId { get; set; }


  public string GpuDriverName { get; set; }


  public uint GpuDriverVersionHigh { get; set; }


  public uint GpuDriverVersionLow { get; set; }


  public uint GpuDxSupportLevel { get; set; }


  public uint GpuTextureMemorySizeMb { get; set; }


  public uint BackbufferWidth { get; set; }


  public uint BackbufferHeight { get; set; }

}
