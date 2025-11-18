
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgPlaceDecalEvent : ITypedProtobuf<CMsgPlaceDecalEvent>, INetMessage<CMsgPlaceDecalEvent>, IDisposable
{
  static int INetMessage<CMsgPlaceDecalEvent>.MessageId => 201;
  
  static string INetMessage<CMsgPlaceDecalEvent>.MessageName => "CMsgPlaceDecalEvent";

  static CMsgPlaceDecalEvent ITypedProtobuf<CMsgPlaceDecalEvent>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgPlaceDecalEventImpl(handle, isManuallyAllocated);


  public Vector Position { get; set; }


  public Vector Normal { get; set; }


  public Vector Saxis { get; set; }


  public int Boneindex { get; set; }


  public int Triangleindex { get; set; }


  public uint Flags { get; set; }


  public uint Color { get; set; }


  public int RandomSeed { get; set; }


  public uint DecalGroupName { get; set; }


  public float SizeOverride { get; set; }


  public uint Entityhandle { get; set; }


  public ulong MaterialId { get; set; }


  public uint SequenceName { get; set; }

}
