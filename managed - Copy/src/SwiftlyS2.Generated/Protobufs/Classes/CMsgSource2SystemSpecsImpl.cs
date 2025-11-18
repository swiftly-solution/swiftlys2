
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSource2SystemSpecsImpl : TypedProtobuf<CMsgSource2SystemSpecs>, CMsgSource2SystemSpecs
{
  public CMsgSource2SystemSpecsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string CpuId
  { get => Accessor.GetString("cpu_id"); set => Accessor.SetString("cpu_id", value); }


  public string CpuBrand
  { get => Accessor.GetString("cpu_brand"); set => Accessor.SetString("cpu_brand", value); }


  public uint CpuModel
  { get => Accessor.GetUInt32("cpu_model"); set => Accessor.SetUInt32("cpu_model", value); }


  public uint CpuNumPhysical
  { get => Accessor.GetUInt32("cpu_num_physical"); set => Accessor.SetUInt32("cpu_num_physical", value); }


  public uint RamPhysicalTotalMb
  { get => Accessor.GetUInt32("ram_physical_total_mb"); set => Accessor.SetUInt32("ram_physical_total_mb", value); }


  public string GpuRendersystemDllName
  { get => Accessor.GetString("gpu_rendersystem_dll_name"); set => Accessor.SetString("gpu_rendersystem_dll_name", value); }


  public uint GpuVendorId
  { get => Accessor.GetUInt32("gpu_vendor_id"); set => Accessor.SetUInt32("gpu_vendor_id", value); }


  public string GpuDriverName
  { get => Accessor.GetString("gpu_driver_name"); set => Accessor.SetString("gpu_driver_name", value); }


  public uint GpuDriverVersionHigh
  { get => Accessor.GetUInt32("gpu_driver_version_high"); set => Accessor.SetUInt32("gpu_driver_version_high", value); }


  public uint GpuDriverVersionLow
  { get => Accessor.GetUInt32("gpu_driver_version_low"); set => Accessor.SetUInt32("gpu_driver_version_low", value); }


  public uint GpuDxSupportLevel
  { get => Accessor.GetUInt32("gpu_dx_support_level"); set => Accessor.SetUInt32("gpu_dx_support_level", value); }


  public uint GpuTextureMemorySizeMb
  { get => Accessor.GetUInt32("gpu_texture_memory_size_mb"); set => Accessor.SetUInt32("gpu_texture_memory_size_mb", value); }


  public uint BackbufferWidth
  { get => Accessor.GetUInt32("backbuffer_width"); set => Accessor.SetUInt32("backbuffer_width", value); }


  public uint BackbufferHeight
  { get => Accessor.GetUInt32("backbuffer_height"); set => Accessor.SetUInt32("backbuffer_height", value); }

}
