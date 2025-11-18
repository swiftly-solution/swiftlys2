
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CNETMsg_DebugOverlay : ITypedProtobuf<CNETMsg_DebugOverlay>, INetMessage<CNETMsg_DebugOverlay>, IDisposable
{
  static int INetMessage<CNETMsg_DebugOverlay>.MessageId => 15;
  
  static string INetMessage<CNETMsg_DebugOverlay>.MessageName => "CNETMsg_DebugOverlay";

  static CNETMsg_DebugOverlay ITypedProtobuf<CNETMsg_DebugOverlay>.Wrap(nint handle, bool isManuallyAllocated) => new CNETMsg_DebugOverlayImpl(handle, isManuallyAllocated);


  public int Etype { get; set; }


  public IProtobufRepeatedFieldValueType<Vector> Vectors { get; }


  public IProtobufRepeatedFieldValueType<Color> Colors { get; }


  public IProtobufRepeatedFieldValueType<float> Dimensions { get; }


  public IProtobufRepeatedFieldValueType<float> Times { get; }


  public IProtobufRepeatedFieldValueType<bool> Bools { get; }


  public IProtobufRepeatedFieldValueType<ulong> Uint64s { get; }


  public IProtobufRepeatedFieldValueType<string> Strings { get; }

}
