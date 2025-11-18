
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMessageRequestDiagnostic_Diagnostic : ITypedProtobuf<CUserMessageRequestDiagnostic_Diagnostic>
{
  static CUserMessageRequestDiagnostic_Diagnostic ITypedProtobuf<CUserMessageRequestDiagnostic_Diagnostic>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageRequestDiagnostic_DiagnosticImpl(handle, isManuallyAllocated);


  public int Index { get; set; }


  public long Offset { get; set; }


  public int Param { get; set; }


  public int Length { get; set; }


  public int Type { get; set; }


  public long Base { get; set; }


  public long Range { get; set; }


  public long Extent { get; set; }


  public long Detail { get; set; }


  public string Name { get; set; }


  public string Alias { get; set; }


  public byte[] Vardetail { get; set; }


  public int Context { get; set; }

}
