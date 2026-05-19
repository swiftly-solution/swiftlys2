using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgICERendezvousImpl : TypedProtobuf<CMsgICERendezvous>, CMsgICERendezvous
{
    public CMsgICERendezvousImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CMsgICERendezvous_Auth Auth
    { get => new CMsgICERendezvous_AuthImpl(NativeNetMessages.GetNestedMessage(Address, "auth"), false); }
    public CMsgICECandidate AddCandidate
    { get => new CMsgICECandidateImpl(NativeNetMessages.GetNestedMessage(Address, "add_candidate"), false); }
}