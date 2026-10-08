using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgModifyItemStringAttrImpl : TypedProtobuf<CMsgModifyItemStringAttr>, CMsgModifyItemStringAttr
{
    public CMsgModifyItemStringAttrImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public ulong ToolItemId
    { get => Accessor.GetUInt64("tool_item_id"); set => Accessor.SetUInt64("tool_item_id", value); }
    public ulong SubjectItemId
    { get => Accessor.GetUInt64("subject_item_id"); set => Accessor.SetUInt64("subject_item_id", value); }
    public uint AttrDef
    { get => Accessor.GetUInt32("attr_def"); set => Accessor.SetUInt32("attr_def", value); }
    public string AttrValue
    { get => Accessor.GetString("attr_value"); set => Accessor.SetString("attr_value", value); }
}