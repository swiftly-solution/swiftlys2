using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgICERendezvous_AuthImpl : TypedProtobuf<CMsgICERendezvous_Auth>, CMsgICERendezvous_Auth
{
    public CMsgICERendezvous_AuthImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public string PwdFrag
    { get => Accessor.GetString("pwd_frag"); set => Accessor.SetString("pwd_frag", value); }
}