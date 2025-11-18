
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_ChangeControlPointAttachment : ITypedProtobuf<CUserMsg_ParticleManager_ChangeControlPointAttachment>
{
  static CUserMsg_ParticleManager_ChangeControlPointAttachment ITypedProtobuf<CUserMsg_ParticleManager_ChangeControlPointAttachment>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_ChangeControlPointAttachmentImpl(handle, isManuallyAllocated);


  public int AttachmentOld { get; set; }


  public int AttachmentNew { get; set; }


  public uint EntityHandle { get; set; }

}
