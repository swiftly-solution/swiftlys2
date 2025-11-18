
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_EndOfMatchAllPlayersData_AccoladeImpl : TypedProtobuf<CCSUsrMsg_EndOfMatchAllPlayersData_Accolade>, CCSUsrMsg_EndOfMatchAllPlayersData_Accolade
{
  public CCSUsrMsg_EndOfMatchAllPlayersData_AccoladeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Eaccolade
  { get => Accessor.GetInt32("eaccolade"); set => Accessor.SetInt32("eaccolade", value); }


  public float Value
  { get => Accessor.GetFloat("value"); set => Accessor.SetFloat("value", value); }


  public int Position
  { get => Accessor.GetInt32("position"); set => Accessor.SetInt32("position", value); }

}
