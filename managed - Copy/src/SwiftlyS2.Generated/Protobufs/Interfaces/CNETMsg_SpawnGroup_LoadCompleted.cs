
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CNETMsg_SpawnGroup_LoadCompleted : ITypedProtobuf<CNETMsg_SpawnGroup_LoadCompleted>, INetMessage<CNETMsg_SpawnGroup_LoadCompleted>, IDisposable
{
  static int INetMessage<CNETMsg_SpawnGroup_LoadCompleted>.MessageId => 13;
  
  static string INetMessage<CNETMsg_SpawnGroup_LoadCompleted>.MessageName => "CNETMsg_SpawnGroup_LoadCompleted";

  static CNETMsg_SpawnGroup_LoadCompleted ITypedProtobuf<CNETMsg_SpawnGroup_LoadCompleted>.Wrap(nint handle, bool isManuallyAllocated) => new CNETMsg_SpawnGroup_LoadCompletedImpl(handle, isManuallyAllocated);


  public uint Spawngrouphandle { get; set; }

}
