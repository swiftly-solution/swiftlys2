
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_RoundBackupFilenamesImpl : NetMessage<CCSUsrMsg_RoundBackupFilenames>, CCSUsrMsg_RoundBackupFilenames
{
  public CCSUsrMsg_RoundBackupFilenamesImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Count
  { get => Accessor.GetInt32("count"); set => Accessor.SetInt32("count", value); }


  public int Index
  { get => Accessor.GetInt32("index"); set => Accessor.SetInt32("index", value); }


  public string Filename
  { get => Accessor.GetString("filename"); set => Accessor.SetString("filename", value); }


  public string Nicename
  { get => Accessor.GetString("nicename"); set => Accessor.SetString("nicename", value); }

}
