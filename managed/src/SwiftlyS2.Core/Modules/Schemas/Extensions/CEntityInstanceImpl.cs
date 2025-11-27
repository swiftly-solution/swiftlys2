using System.Buffers;
using System.Text;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.EntitySystem;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CEntityInstanceImpl : CEntityInstance
{
    public uint Index => Entity?.EntityHandle.EntityIndex ?? uint.MaxValue;
    public string DesignerName => Entity?.DesignerName ?? string.Empty;

    public unsafe void AcceptInput<T>( string input, T value, CEntityInstance? activator = null, CEntityInstance? caller = null, int outputID = 0 )
    {
        var variant = new CVariant();
        switch (value)
        {
            case bool boolValue:
                variant.Data.BoolValue = boolValue;
                variant.DataType = VariantFieldType.FIELD_BOOLEAN;
                break;
            case int intValue:
                variant.Data.IntValue = intValue;
                variant.DataType = VariantFieldType.FIELD_INT32;
                break;
            case uint uintValue:
                variant.Data.UIntValue = uintValue;
                variant.DataType = VariantFieldType.FIELD_UINT32;
                break;
            case long longValue:
                variant.Data.Int64Value = longValue;
                variant.DataType = VariantFieldType.FIELD_INT64;
                break;
            case ulong ulongValue:
                variant.Data.UInt64Value = ulongValue;
                variant.DataType = VariantFieldType.FIELD_UINT64;
                break;
            case float floatValue:
                variant.Data.FloatValue = floatValue;
                variant.DataType = VariantFieldType.FIELD_FLOAT32;
                break;
            case double doubleValue:
                variant.Data.DoubleValue = doubleValue;
                variant.DataType = VariantFieldType.FIELD_FLOAT64;
                break;
            case string stringValue:
                variant.Data.StringValue = new CString();
                variant.Data.StringValue.Value = stringValue;
                variant.DataType = VariantFieldType.FIELD_CSTRING;
                break;
            case Vector vectorValue:
                variant.Data.VectorValue = &vectorValue;
                variant.DataType = VariantFieldType.FIELD_VECTOR;
                break;
            case QAngle qangleValue:
                variant.Data.QAngleValue = &qangleValue;
                variant.DataType = VariantFieldType.FIELD_QANGLE;
                break;
            case Vector2D vector2DValue:
                variant.Data.Vector2DValue = &vector2DValue;
                variant.DataType = VariantFieldType.FIELD_VECTOR2D;
                break;
            case Vector4D vector4DValue:
                variant.Data.Vector4DValue = &vector4DValue;
                variant.DataType = VariantFieldType.FIELD_VECTOR4D;
                break;
            case Quaternion quaternionValue:
                variant.Data.QuaternionValue = &quaternionValue;
                variant.DataType = VariantFieldType.FIELD_QUATERNION;
                break;
            case Color colorValue:
                variant.Data.ColorValue = &colorValue;
                variant.DataType = VariantFieldType.FIELD_COLOR32;
                break;
            default:
                throw new InvalidOperationException($"Unsupported type: {typeof(T).Name}");
        }

        NativeEntitySystem.AcceptInput(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, (nint)(&variant), outputID);
    }

    public unsafe void AddEntityIOEvent<T>( string input, T value, CEntityInstance? activator = null, CEntityInstance? caller = null, float delay = 0f )
    {
        var variant = new CVariant();
        switch (value)
        {
            case bool boolValue:
                variant.Data.BoolValue = boolValue;
                variant.DataType = VariantFieldType.FIELD_BOOLEAN;
                break;
            case int intValue:
                variant.Data.IntValue = intValue;
                variant.DataType = VariantFieldType.FIELD_INT32;
                break;
            case uint uintValue:
                variant.Data.UIntValue = uintValue;
                variant.DataType = VariantFieldType.FIELD_UINT32;
                break;
            case long longValue:
                variant.Data.Int64Value = longValue;
                variant.DataType = VariantFieldType.FIELD_INT64;
                break;
            case ulong ulongValue:
                variant.Data.UInt64Value = ulongValue;
                variant.DataType = VariantFieldType.FIELD_UINT64;
                break;
            case float floatValue:
                variant.Data.FloatValue = floatValue;
                variant.DataType = VariantFieldType.FIELD_FLOAT32;
                break;
            case double doubleValue:
                variant.Data.DoubleValue = doubleValue;
                variant.DataType = VariantFieldType.FIELD_FLOAT64;
                break;
            case string stringValue:
                variant.Data.StringValue = new CString();
                variant.Data.StringValue.Value = stringValue;
                variant.DataType = VariantFieldType.FIELD_CSTRING;
                break;
            case Vector vectorValue:
                variant.Data.VectorValue = &vectorValue;
                variant.DataType = VariantFieldType.FIELD_VECTOR;
                break;
            case QAngle qangleValue:
                variant.Data.QAngleValue = &qangleValue;
                variant.DataType = VariantFieldType.FIELD_QANGLE;
                break;
            case Vector2D vector2DValue:
                variant.Data.Vector2DValue = &vector2DValue;
                variant.DataType = VariantFieldType.FIELD_VECTOR2D;
                break;
            case Vector4D vector4DValue:
                variant.Data.Vector4DValue = &vector4DValue;
                variant.DataType = VariantFieldType.FIELD_VECTOR4D;
                break;
            case Quaternion quaternionValue:
                variant.Data.QuaternionValue = &quaternionValue;
                variant.DataType = VariantFieldType.FIELD_QUATERNION;
                break;
            case Color colorValue:
                variant.Data.ColorValue = &colorValue;
                variant.DataType = VariantFieldType.FIELD_COLOR32;
                break;
            default:
                throw new InvalidOperationException($"Unsupported type: {typeof(T).Name}");
        }

        NativeEntitySystem.AddEntityIOEvent(Address, input, activator?.Address ?? nint.Zero, caller?.Address ?? nint.Zero, (nint)(&variant), delay);
    }

    public void SetTransmitState( bool transmitting, int playerId )
    {
        NativePlayer.ShouldBlockTransmitEntity(playerId, (int)Index, !transmitting);
    }

    public void SetTransmitState( bool transmitting )
    {
        NativePlayerManager.ShouldBlockTransmitEntity((int)Index, !transmitting);
    }

    public bool IsTransmitting( int playerId )
    {
        return !NativePlayer.IsTransmitEntityBlocked(playerId, (int)Index);
    }

    public void DispatchSpawn( CEntityKeyValues? entityKV = null )
    {
        NativeEntitySystem.Spawn(Address, entityKV?.Address ?? nint.Zero);
    }

    public void Despawn()
    {
        NativeEntitySystem.Despawn(Address);
    }
}