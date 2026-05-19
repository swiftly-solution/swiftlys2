using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgICECandidate : ITypedProtobuf<CMsgICECandidate>
{
    static CMsgICECandidate ITypedProtobuf<CMsgICECandidate>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgICECandidateImpl(handle, isManuallyAllocated);

    public string Candidate { get; set; }
}