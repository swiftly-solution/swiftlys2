using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTOSTreatmentImpl : TypedProtobuf<CMsgTOSTreatment>, CMsgTOSTreatment
{
    public CMsgTOSTreatmentImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public string L4sDetect
    { get => Accessor.GetString("l4s_detect"); set => Accessor.SetString("l4s_detect", value); }
    public string UpEcn1
    { get => Accessor.GetString("up_ecn1"); set => Accessor.SetString("up_ecn1", value); }
    public string DownDscp45
    { get => Accessor.GetString("down_dscp45"); set => Accessor.SetString("down_dscp45", value); }
}