
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSerializedSOCacheImpl : TypedProtobuf<CMsgSerializedSOCache>, CMsgSerializedSOCache
{
  public CMsgSerializedSOCacheImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint FileVersion
  { get => Accessor.GetUInt32("file_version"); set => Accessor.SetUInt32("file_version", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgSerializedSOCache_Cache> Caches
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgSerializedSOCache_Cache>(Accessor, "caches"); }


  public uint GcSocacheFileVersion
  { get => Accessor.GetUInt32("gc_socache_file_version"); set => Accessor.SetUInt32("gc_socache_file_version", value); }

}
