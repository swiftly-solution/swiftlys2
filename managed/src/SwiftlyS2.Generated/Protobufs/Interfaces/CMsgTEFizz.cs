
using SwiftlyS2.Core.ProtobufDefinitions;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

using SwiftlyS2.Shared.NetMessages;

public interface CMsgTEFizz : ITypedProtobuf<CMsgTEFizz>, INetMessage<CMsgTEFizz>, IDisposable
{
    static int INetMessage<CMsgTEFizz>.MessageId => 413;

    static string INetMessage<CMsgTEFizz>.MessageName => "CMsgTEFizz";

    static CMsgTEFizz ITypedProtobuf<CMsgTEFizz>.Wrap( nint handle, bool isManuallyAllocated ) => new CMsgTEFizzImpl(handle, isManuallyAllocated);


    public int Entity { get; set; }


    public uint Density { get; set; }


    public int Current { get; set; }

}
