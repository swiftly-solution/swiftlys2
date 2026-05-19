using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgICERendezvous : ITypedProtobuf<CMsgICERendezvous>
{
    static CMsgICERendezvous ITypedProtobuf<CMsgICERendezvous>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgICERendezvousImpl(handle, isManuallyAllocated);

    public CMsgICERendezvous_Auth Auth { get; }
    public CMsgICECandidate AddCandidate { get; }
}