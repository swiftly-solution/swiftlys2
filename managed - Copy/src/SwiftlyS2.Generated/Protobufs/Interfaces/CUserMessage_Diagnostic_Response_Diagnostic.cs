
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMessage_Diagnostic_Response_Diagnostic : ITypedProtobuf<CUserMessage_Diagnostic_Response_Diagnostic>
{
  static CUserMessage_Diagnostic_Response_Diagnostic ITypedProtobuf<CUserMessage_Diagnostic_Response_Diagnostic>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessage_Diagnostic_Response_DiagnosticImpl(handle, isManuallyAllocated);


  public int Index { get; set; }


  public long Offset { get; set; }


  public int Param { get; set; }


  public int Length { get; set; }


  public byte[] Detail { get; set; }


  public long Base { get; set; }


  public long Range { get; set; }


  public int Type { get; set; }


  public string Name { get; set; }


  public string Alias { get; set; }


  public byte[] Backup { get; set; }


  public int Context { get; set; }


  public long Control { get; set; }


  public long Augment { get; set; }


  public long Placebo { get; set; }

}
