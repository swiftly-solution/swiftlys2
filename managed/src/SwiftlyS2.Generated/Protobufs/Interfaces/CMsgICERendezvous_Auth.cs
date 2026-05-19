using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgICERendezvous_Auth : ITypedProtobuf<CMsgICERendezvous_Auth>
{
    static CMsgICERendezvous_Auth ITypedProtobuf<CMsgICERendezvous_Auth>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgICERendezvous_AuthImpl(handle, isManuallyAllocated);

    public string PwdFrag { get; set; }
}