
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CNETMsg_SpawnGroup_ManifestUpdate : ITypedProtobuf<CNETMsg_SpawnGroup_ManifestUpdate>, INetMessage<CNETMsg_SpawnGroup_ManifestUpdate>, IDisposable
{
  static int INetMessage<CNETMsg_SpawnGroup_ManifestUpdate>.MessageId => 9;
  
  static string INetMessage<CNETMsg_SpawnGroup_ManifestUpdate>.MessageName => "CNETMsg_SpawnGroup_ManifestUpdate";

  static CNETMsg_SpawnGroup_ManifestUpdate ITypedProtobuf<CNETMsg_SpawnGroup_ManifestUpdate>.Wrap(nint handle, bool isManuallyAllocated) => new CNETMsg_SpawnGroup_ManifestUpdateImpl(handle, isManuallyAllocated);


  public uint Spawngrouphandle { get; set; }


  public byte[] Spawngroupmanifest { get; set; }


  public bool Manifestincomplete { get; set; }

}
