using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Sounds;

internal enum SosFieldType : byte
{
    Bool = 1,
    Int = 2,
    UInt32 = 3,
    Float = 8,
    Float3 = 0xA,
}

[InlineArray(12)]
internal struct ParamBuffer
{
    private byte _element0;
}

internal struct ParamValue
{
    public SosFieldType Type;
    public byte Length;
    public ParamBuffer Data;
}


public class SoundEvent : IDisposable
{
    private const uint NameHashSeed = 0x53524332;

    private readonly SortedDictionary<string, ParamValue> _parameters = new(StringComparer.Ordinal);

    private string _name {get;set;} = "";
    private bool _nameDirty = true;
    private uint _soundEventHashCache;

    private bool _paramsDirty = true;
    private byte[] _packedParamsBuffer = [];

    private CMsgSosStartSoundEvent? _message;

    public string Name {
        get => _name;
        set {
            _name = value;
            _nameDirty = true;
        }
    }

    public int SourceEntityIndex { get; set; }

    public float Volume {
        get => GetFloat("public.volume");
        set => SetFloat("public.volume", value);
    }

    public float Pitch {
        get => GetFloat("public.pitch");
        set => SetFloat("public.pitch", value);
    }

    private CRecipientFilter _recipients = new();

    public ref CRecipientFilter Recipients {
        get => ref _recipients;
    }

    public SoundEvent()
    {
        Name = "";
        Volume = 1.0f;
        Pitch = 1.0f;
        SourceEntityIndex = -1;
    }

    public SoundEvent( string name, float volume = 1.0f, float pitch = 1.0f )
    {
        Name = name;
        Volume = volume;
        Pitch = pitch;
        SourceEntityIndex = -1;
    }

    public void SetSourceEntity( CEntityInstance entity )
    {
        SourceEntityIndex = (int)entity.Index;
    }

    public void SetBool( string fieldName, bool value )
    {
        var param = new ParamValue { Type = SosFieldType.Bool, Length = 1 };
        param.Data[0] = value ? (byte)1 : (byte)0;
        _parameters[fieldName] = param;
        _paramsDirty = true;
    }

    public bool GetBool( string fieldName )
    {
        return _parameters.TryGetValue(fieldName, out var field) && field.Type == SosFieldType.Bool && field.Data[0] != 0;
    }

    public void SetInt32( string fieldName, int value )
    {
        var param = new ParamValue { Type = SosFieldType.Int, Length = 4 };
        BinaryPrimitives.WriteInt32LittleEndian(param.Data, value);
        _parameters[fieldName] = param;
        _paramsDirty = true;
    }

    public int GetInt32( string fieldName )
    {
        return _parameters.TryGetValue(fieldName, out var field) && field.Type == SosFieldType.Int
            ? BinaryPrimitives.ReadInt32LittleEndian(field.Data)
            : 0;
    }

    public void SetUInt32( string fieldName, uint value )
    {
        var param = new ParamValue { Type = SosFieldType.UInt32, Length = 4 };
        BinaryPrimitives.WriteUInt32LittleEndian(param.Data, value);
        _parameters[fieldName] = param;
        _paramsDirty = true;
    }

    public uint GetUInt32( string fieldName )
    {
        return _parameters.TryGetValue(fieldName, out var field) && field.Type == SosFieldType.UInt32
            ? BinaryPrimitives.ReadUInt32LittleEndian(field.Data)
            : 0;
    }

    public void SetFloat( string fieldName, float value )
    {
        var param = new ParamValue { Type = SosFieldType.Float, Length = 4 };
        BinaryPrimitives.WriteSingleLittleEndian(param.Data, value);
        _parameters[fieldName] = param;
        _paramsDirty = true;
    }

    public float GetFloat( string fieldName )
    {
        return _parameters.TryGetValue(fieldName, out var field) && field.Type == SosFieldType.Float
            ? BinaryPrimitives.ReadSingleLittleEndian(field.Data)
            : 0.0f;
    }

    public void SetFloat3( string fieldName, float x, float y, float z )
    {
        SetFloat3(fieldName, new Vector(x, y, z));
    }

    public void SetFloat3( string fieldName, Vector vec )
    {
        var param = new ParamValue { Type = SosFieldType.Float3, Length = 12 };
        Span<byte> span = param.Data;
        BinaryPrimitives.WriteSingleLittleEndian(span[..4], vec.X);
        BinaryPrimitives.WriteSingleLittleEndian(span.Slice(4, 4), vec.Y);
        BinaryPrimitives.WriteSingleLittleEndian(span.Slice(8, 4), vec.Z);
        _parameters[fieldName] = param;
        _paramsDirty = true;
    }

    public Vector GetFloat3( string fieldName )
    {
        if (_parameters.TryGetValue(fieldName, out var field) && field.Type == SosFieldType.Float3)
        {
            ReadOnlySpan<byte> span = field.Data;
            return new Vector(
                BinaryPrimitives.ReadSingleLittleEndian(span[..4]),
                BinaryPrimitives.ReadSingleLittleEndian(span.Slice(4, 4)),
                BinaryPrimitives.ReadSingleLittleEndian(span.Slice(8, 4))
            );
        }
        return new Vector(0, 0, 0);
    }

    private static T CreateNetMessage<T>() where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable
    {
        var handle = NativeNetMessages.AllocateNetMessageByID(T.MessageId);
        return T.Wrap(handle, true);
    }

    private void RebuildPackedParams()
    {
        var totalSize = 0;
        foreach (var (_, field) in _parameters)
            totalSize += 4 + 1 + 1 + 1 + field.Length;

        if (_packedParamsBuffer.Length != totalSize)
            _packedParamsBuffer = new byte[totalSize];

        var packedParams = _packedParamsBuffer;
        var offset = 0;
        foreach (var (fieldName, field) in _parameters)
        {
            BinaryPrimitives.WriteUInt32LittleEndian(packedParams.AsSpan(offset, 4), MurmurHash2.HashStringLowercase(fieldName));
            offset += 4;
            packedParams[offset++] = (byte)field.Type;
            packedParams[offset++] = field.Length;
            packedParams[offset++] = 0;
            ReadOnlySpan<byte> fieldData = field.Data;
            fieldData[..field.Length].CopyTo(packedParams.AsSpan(offset, field.Length));
            offset += field.Length;
        }
    }

    [ThreadUnsafe]
    public uint Emit()
    {
        NativeBinding.ThrowIfNonMainThread();

        if (_paramsDirty)
        {
            RebuildPackedParams();
            _paramsDirty = false;
        }

        if (_nameDirty)
        {
            _soundEventHashCache = MurmurHash2.HashStringLowercase(Name, NameHashSeed);
            _nameDirty = false;
        }

        var guid = GameFunctions.CSoundSystem_TakeGuid();

        _message ??= CreateNetMessage<CMsgSosStartSoundEvent>();
        _message.SoundeventHash = _soundEventHashCache;
        _message.SourceEntityIndex = SourceEntityIndex;
        _message.SoundeventGuid = (int)guid;
        _message.Seed = (int)guid;
        _message.PackedParams = _packedParamsBuffer;
        _message.Recipients = Recipients;
        _message.Send();

        return guid;
    }

    public Task<uint> EmitAsync()
    {
        return SchedulerManager.QueueOrNow(Emit);
    }

    public void Dispose()
    {
        _message?.Dispose();
    }
}
