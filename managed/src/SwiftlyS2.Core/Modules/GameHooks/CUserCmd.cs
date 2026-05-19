using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.GameHooks;

internal class CUserCmd : IUserCmd, IDisposable
{
    private bool _disposed = false;
    private CSGOUserCmdPBImpl? _csgoUserCmd = null;
    private CInButtonStateImpl? _buttonState = null;

    public required nint Address { get; init; }

    public uint CommandNumber {
        get {
            ThrowIfInvalid();
            return Address.Read<uint>(0x8);
        }
        set {
            ThrowIfInvalid();
            Address.Write(0x8, value);
        }
    }

    public CSGOUserCmdPB CSGOUserCmd {
        get {
            if (_csgoUserCmd == null)
            {
                ThrowIfInvalid();
                _csgoUserCmd = new CSGOUserCmdPBImpl(Address + 0x10, false);
            }

            return _csgoUserCmd;
        }
    }

    public CInButtonState ButtonState {
        get {
            if (_buttonState == null)
            {
                ThrowIfInvalid();
                _buttonState = new CInButtonStateImpl(Address + 0x58);
            }
            return _buttonState;
        }
    }

    ~CUserCmd()
    {
        Dispose();
    }

    public void Dispose()
    {
        if (_disposed) return;

        _disposed = true;
        GC.SuppressFinalize(this);
    }

    private void ThrowIfInvalid()
    {
        if (!_disposed)
        {
            if (Address == 0)
                throw new InvalidOperationException("CUserCmd is not valid.");
        }
        else throw new ObjectDisposedException(nameof(CUserCmd));
    }
}
