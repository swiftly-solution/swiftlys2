
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCBannedWordImpl : TypedProtobuf<CMsgGCBannedWord>, CMsgGCBannedWord
{
  public CMsgGCBannedWordImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint WordId
  { get => Accessor.GetUInt32("word_id"); set => Accessor.SetUInt32("word_id", value); }


  public GC_BannedWordType WordType
  { get => (GC_BannedWordType)Accessor.GetInt32("word_type"); set => Accessor.SetInt32("word_type", (int)value); }


  public string Word
  { get => Accessor.GetString("word"); set => Accessor.SetString("word", value); }

}
