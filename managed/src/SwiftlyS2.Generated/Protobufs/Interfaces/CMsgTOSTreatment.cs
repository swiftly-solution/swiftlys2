using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgTOSTreatment : ITypedProtobuf<CMsgTOSTreatment>
{
    static CMsgTOSTreatment ITypedProtobuf<CMsgTOSTreatment>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTOSTreatmentImpl(handle, isManuallyAllocated);

    public string L4sDetect { get; set; }
    public string UpEcn1 { get; set; }
    public string DownDscp45 { get; set; }
}