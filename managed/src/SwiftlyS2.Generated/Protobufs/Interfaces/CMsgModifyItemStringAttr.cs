using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgModifyItemStringAttr : ITypedProtobuf<CMsgModifyItemStringAttr>
{
    static CMsgModifyItemStringAttr ITypedProtobuf<CMsgModifyItemStringAttr>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgModifyItemStringAttrImpl(handle, isManuallyAllocated);

    public ulong ToolItemId { get; set; }
    public ulong SubjectItemId { get; set; }
    public uint AttrDef { get; set; }
    public string AttrValue { get; set; }
}