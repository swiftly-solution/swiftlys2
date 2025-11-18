
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_RoundBackupFilenames : ITypedProtobuf<CCSUsrMsg_RoundBackupFilenames>, INetMessage<CCSUsrMsg_RoundBackupFilenames>, IDisposable
{
  static int INetMessage<CCSUsrMsg_RoundBackupFilenames>.MessageId => 362;
  
  static string INetMessage<CCSUsrMsg_RoundBackupFilenames>.MessageName => "CCSUsrMsg_RoundBackupFilenames";

  static CCSUsrMsg_RoundBackupFilenames ITypedProtobuf<CCSUsrMsg_RoundBackupFilenames>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_RoundBackupFilenamesImpl(handle, isManuallyAllocated);


  public int Count { get; set; }


  public int Index { get; set; }


  public string Filename { get; set; }


  public string Nicename { get; set; }

}
