
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageColoredText : ITypedProtobuf<CUserMessageColoredText>, INetMessage<CUserMessageColoredText>, IDisposable
{
  static int INetMessage<CUserMessageColoredText>.MessageId => 113;
  
  static string INetMessage<CUserMessageColoredText>.MessageName => "CUserMessageColoredText";

  static CUserMessageColoredText ITypedProtobuf<CUserMessageColoredText>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageColoredTextImpl(handle, isManuallyAllocated);


  public uint Color { get; set; }


  public string Text { get; set; }


  public bool Reset { get; set; }


  public int ContextPlayerSlot { get; set; }


  public int ContextValue { get; set; }


  public int ContextTeamId { get; set; }

}
