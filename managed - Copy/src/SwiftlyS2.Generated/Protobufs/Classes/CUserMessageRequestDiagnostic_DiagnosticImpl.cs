
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageRequestDiagnostic_DiagnosticImpl : TypedProtobuf<CUserMessageRequestDiagnostic_Diagnostic>, CUserMessageRequestDiagnostic_Diagnostic
{
  public CUserMessageRequestDiagnostic_DiagnosticImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Index
  { get => Accessor.GetInt32("index"); set => Accessor.SetInt32("index", value); }


  public long Offset
  { get => Accessor.GetInt64("offset"); set => Accessor.SetInt64("offset", value); }


  public int Param
  { get => Accessor.GetInt32("param"); set => Accessor.SetInt32("param", value); }


  public int Length
  { get => Accessor.GetInt32("length"); set => Accessor.SetInt32("length", value); }


  public int Type
  { get => Accessor.GetInt32("type"); set => Accessor.SetInt32("type", value); }


  public long Base
  { get => Accessor.GetInt64("base"); set => Accessor.SetInt64("base", value); }


  public long Range
  { get => Accessor.GetInt64("range"); set => Accessor.SetInt64("range", value); }


  public long Extent
  { get => Accessor.GetInt64("extent"); set => Accessor.SetInt64("extent", value); }


  public long Detail
  { get => Accessor.GetInt64("detail"); set => Accessor.SetInt64("detail", value); }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public string Alias
  { get => Accessor.GetString("alias"); set => Accessor.SetString("alias", value); }


  public byte[] Vardetail
  { get => Accessor.GetBytes("vardetail"); set => Accessor.SetBytes("vardetail", value); }


  public int Context
  { get => Accessor.GetInt32("context"); set => Accessor.SetInt32("context", value); }

}
