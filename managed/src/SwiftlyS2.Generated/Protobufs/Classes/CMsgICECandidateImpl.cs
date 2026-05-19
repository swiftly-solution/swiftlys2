using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgICECandidateImpl : TypedProtobuf<CMsgICECandidate>, CMsgICECandidate
{
    public CMsgICECandidateImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public string Candidate
    { get => Accessor.GetString("candidate"); set => Accessor.SetString("candidate", value); }
}