
using SwiftlyS2.Core.ProtobufDefinitions;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageShowMenu : ITypedProtobuf<CUserMessageShowMenu>, INetMessage<CUserMessageShowMenu>, IDisposable
{
    static int INetMessage<CUserMessageShowMenu>.MessageId => 134;

    static string INetMessage<CUserMessageShowMenu>.MessageName => "CUserMessageShowMenu";

    static CUserMessageShowMenu ITypedProtobuf<CUserMessageShowMenu>.Wrap( nint handle, bool isManuallyAllocated ) => new CUserMessageShowMenuImpl(handle, isManuallyAllocated);


    public uint Validslots { get; set; }


    public uint Displaytime { get; set; }


    public bool Needmore { get; set; }


    public string Menustring { get; set; }

}
