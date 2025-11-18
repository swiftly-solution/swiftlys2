
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_ProcessSpottedEntityUpdateImpl : NetMessage<CCSUsrMsg_ProcessSpottedEntityUpdate>, CCSUsrMsg_ProcessSpottedEntityUpdate
{
  public CCSUsrMsg_ProcessSpottedEntityUpdateImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public bool NewUpdate
  { get => Accessor.GetBool("new_update"); set => Accessor.SetBool("new_update", value); }


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_ProcessSpottedEntityUpdate_SpottedEntityUpdate> EntityUpdates
  { get => new ProtobufRepeatedFieldSubMessageType<CCSUsrMsg_ProcessSpottedEntityUpdate_SpottedEntityUpdate>(Accessor, "entity_updates"); }

}
