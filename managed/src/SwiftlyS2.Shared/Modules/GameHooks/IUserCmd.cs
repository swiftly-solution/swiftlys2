using SwiftlyS2.Core.GameHooks;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public interface IUserCmd
{
    public nint Address { get; }

    public uint CommandNumber { get; set; }

    public CSGOUserCmdPB CSGOUserCmd { get; }

    public CInButtonState ButtonState { get; }

    public static IUserCmd From( nint address ) => new CUserCmd { Address = address };
}
