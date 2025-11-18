
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_ChangeControlPointAttachmentImpl : TypedProtobuf<CUserMsg_ParticleManager_ChangeControlPointAttachment>, CUserMsg_ParticleManager_ChangeControlPointAttachment
{
  public CUserMsg_ParticleManager_ChangeControlPointAttachmentImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int AttachmentOld
  { get => Accessor.GetInt32("attachment_old"); set => Accessor.SetInt32("attachment_old", value); }


  public int AttachmentNew
  { get => Accessor.GetInt32("attachment_new"); set => Accessor.SetInt32("attachment_new", value); }


  public uint EntityHandle
  { get => Accessor.GetUInt32("entity_handle"); set => Accessor.SetUInt32("entity_handle", value); }

}
