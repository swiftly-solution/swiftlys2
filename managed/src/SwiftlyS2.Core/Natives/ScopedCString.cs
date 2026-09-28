using System.Buffers;
using System.Text;

namespace SwiftlyS2.Core.Natives;

internal ref struct ScopedCString
{
    private static readonly ArrayPool<byte> Pool = ArrayPool<byte>.Shared;

    private byte[]? _buffer;
    private int _length;

    public ScopedCString( string value )
    {
        _buffer = null;
        _length = 0;

        var byteCount = Encoding.UTF8.GetByteCount(value);
        var buffer = Pool.Rent(byteCount + 1);

        try
        {
            var bytesWritten = Encoding.UTF8.GetBytes(value.AsSpan(), buffer.AsSpan(0, byteCount));
            buffer[bytesWritten] = 0;

            _buffer = buffer;
            _length = bytesWritten + 1;
        }
        catch
        {
            Pool.Return(buffer);
            throw;
        }
    }

    public readonly ref byte GetPinnableReference()
    {
        return ref _buffer![0];
    }

    public readonly Span<byte> Span => _buffer.AsSpan(0, _length);

    public void Dispose()
    {
        var buffer = _buffer;
        if (buffer is null)
        {
            return;
        }

        _buffer = null;
        _length = 0;
        Pool.Return(buffer);
    }
}
