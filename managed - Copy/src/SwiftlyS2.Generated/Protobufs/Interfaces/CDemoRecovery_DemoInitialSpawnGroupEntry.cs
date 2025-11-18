
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDemoRecovery_DemoInitialSpawnGroupEntry : ITypedProtobuf<CDemoRecovery_DemoInitialSpawnGroupEntry>
{
  static CDemoRecovery_DemoInitialSpawnGroupEntry ITypedProtobuf<CDemoRecovery_DemoInitialSpawnGroupEntry>.Wrap(nint handle, bool isManuallyAllocated) => new CDemoRecovery_DemoInitialSpawnGroupEntryImpl(handle, isManuallyAllocated);


  public uint Spawngrouphandle { get; set; }


  public bool WasCreated { get; set; }

}
