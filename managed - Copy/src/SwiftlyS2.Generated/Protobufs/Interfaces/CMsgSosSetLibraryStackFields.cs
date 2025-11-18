
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgSosSetLibraryStackFields : ITypedProtobuf<CMsgSosSetLibraryStackFields>, INetMessage<CMsgSosSetLibraryStackFields>, IDisposable
{
  static int INetMessage<CMsgSosSetLibraryStackFields>.MessageId => 211;
  
  static string INetMessage<CMsgSosSetLibraryStackFields>.MessageName => "CMsgSosSetLibraryStackFields";

  static CMsgSosSetLibraryStackFields ITypedProtobuf<CMsgSosSetLibraryStackFields>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSosSetLibraryStackFieldsImpl(handle, isManuallyAllocated);


  public uint StackHash { get; set; }


  public byte[] PackedFields { get; set; }

}
