
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessage_Diagnostic_Response_DiagnosticImpl : TypedProtobuf<CUserMessage_Diagnostic_Response_Diagnostic>, CUserMessage_Diagnostic_Response_Diagnostic
{
  public CUserMessage_Diagnostic_Response_DiagnosticImpl(nint handle, bool isManuallyAllocated): base(handle)
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


  public byte[] Detail
  { get => Accessor.GetBytes("detail"); set => Accessor.SetBytes("detail", value); }


  public long Base
  { get => Accessor.GetInt64("base"); set => Accessor.SetInt64("base", value); }


  public long Range
  { get => Accessor.GetInt64("range"); set => Accessor.SetInt64("range", value); }


  public int Type
  { get => Accessor.GetInt32("type"); set => Accessor.SetInt32("type", value); }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public string Alias
  { get => Accessor.GetString("alias"); set => Accessor.SetString("alias", value); }


  public byte[] Backup
  { get => Accessor.GetBytes("backup"); set => Accessor.SetBytes("backup", value); }


  public int Context
  { get => Accessor.GetInt32("context"); set => Accessor.SetInt32("context", value); }


  public long Control
  { get => Accessor.GetInt64("control"); set => Accessor.SetInt64("control", value); }


  public long Augment
  { get => Accessor.GetInt64("augment"); set => Accessor.SetInt64("augment", value); }


  public long Placebo
  { get => Accessor.GetInt64("placebo"); set => Accessor.SetInt64("placebo", value); }

}
