
using SwiftlyS2.Core.ProtobufDefinitions;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

using SwiftlyS2.Shared.NetMessages;

public interface CMsgTEEffectDispatch : ITypedProtobuf<CMsgTEEffectDispatch>, INetMessage<CMsgTEEffectDispatch>, IDisposable
{
    static int INetMessage<CMsgTEEffectDispatch>.MessageId => 400;

    static string INetMessage<CMsgTEEffectDispatch>.MessageName => "CMsgTEEffectDispatch";

    static CMsgTEEffectDispatch ITypedProtobuf<CMsgTEEffectDispatch>.Wrap( nint handle, bool isManuallyAllocated ) => new CMsgTEEffectDispatchImpl(handle, isManuallyAllocated);


    public CMsgEffectData Effectdata { get; }

}
