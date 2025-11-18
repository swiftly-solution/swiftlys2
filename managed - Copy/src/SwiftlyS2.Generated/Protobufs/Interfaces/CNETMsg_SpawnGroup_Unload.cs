
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CNETMsg_SpawnGroup_Unload : ITypedProtobuf<CNETMsg_SpawnGroup_Unload>, INetMessage<CNETMsg_SpawnGroup_Unload>, IDisposable
{
  static int INetMessage<CNETMsg_SpawnGroup_Unload>.MessageId => 12;
  
  static string INetMessage<CNETMsg_SpawnGroup_Unload>.MessageName => "CNETMsg_SpawnGroup_Unload";

  static CNETMsg_SpawnGroup_Unload ITypedProtobuf<CNETMsg_SpawnGroup_Unload>.Wrap(nint handle, bool isManuallyAllocated) => new CNETMsg_SpawnGroup_UnloadImpl(handle, isManuallyAllocated);


  public uint Spawngrouphandle { get; set; }


  public uint Flags { get; set; }


  public int Tickcount { get; set; }

}
