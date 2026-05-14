using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Shared.Natives;

public enum KeyValuesDataType
{
    TYPE_NONE = 0,
    TYPE_STRING,
    TYPE_INT,
    TYPE_FLOAT,
    TYPE_PTR,
    TYPE_WSTRING,
    TYPE_COLOR,
    TYPE_UINT64,
    TYPE_COMPILED_INT_BYTE,
    TYPE_COMPILED_INT_0,
    TYPE_COMPILED_INT_1,
    TYPE_NUMTYPES,
};

[StructLayout(LayoutKind.Explicit, Pack = 1)]
public unsafe struct KeyValues
{
    [FieldOffset(0)] public int IntegerValue;
    [FieldOffset(0)] public float FloatValue;
    [FieldOffset(0)] public nint PointerValue;
    [FieldOffset(0)] public Color ColorValue;
    [FieldOffset(0)] public ulong UInt64Value;
    [FieldOffset(0)] public nint StringValue;
    [FieldOffset(0)] public KeyValues* SubKeyValue;
    [FieldOffset(8)] public nint KVSystem;
    [FieldOffset(16)] public int BitFieldVariables;

    public int KeyNameCaseSensitive {
        get => BitFieldHelper.GetBits(ref BitFieldVariables, 0, 24);
        set => BitFieldHelper.SetBits(ref BitFieldVariables, 0, 24, value);
    }

    public KeyValuesDataType DataType {
        get => (KeyValuesDataType)BitFieldHelper.GetBits(ref BitFieldVariables, 24, 3);
        set => BitFieldHelper.SetBits(ref BitFieldVariables, 24, 3, (int)value);
    }

    public bool HasEscapeSequences {
        get => BitFieldHelper.GetBit(ref BitFieldVariables, 27);
        set => BitFieldHelper.SetBit(ref BitFieldVariables, 27, value);
    }

    public bool AllocatedExternalMemory {
        get => BitFieldHelper.GetBit(ref BitFieldVariables, 28);
        set => BitFieldHelper.SetBit(ref BitFieldVariables, 28, value);
    }

    public bool KeySymbolCaseSensitiveMatchesCaseInsensitive {
        get => BitFieldHelper.GetBit(ref BitFieldVariables, 29);
        set => BitFieldHelper.SetBit(ref BitFieldVariables, 29, value);
    }

    public bool StoredSubKey {
        get => BitFieldHelper.GetBit(ref BitFieldVariables, 30);
        set => BitFieldHelper.SetBit(ref BitFieldVariables, 30, value);
    }

    [FieldOffset(20)] public KeyValues* PeerKeyValue;

    public KeyValues* FindKey( HKeySymbol keyName, bool create = false )
    {
        KeyValues* lastItem = null;
        fixed (KeyValues* ptr = &this)
        {
            for (var current = ptr != null ? SubKeyValue : null; current != null; current = current->PeerKeyValue)
            {
                lastItem = current;
                if (current->KeyNameCaseSensitive == keyName.Hash)
                {
                    return current;
                }
            }
        }

        if (create)
        {
            var item = new KeyValues();
            item.SetName(keyName.Value);

            if (lastItem == null)
            {
                SubKeyValue = &item;
            }
            else
            {
                lastItem->PeerKeyValue = &item;
            }
            item.PeerKeyValue = null;

            DataType = KeyValuesDataType.TYPE_NONE;
            return &item;
        }

        return null;
    }

    public KeyValues* FindKey( string keyName, bool create = false )
    {
        var keySymbol = new HKeySymbol {
            Value = keyName
        };

        return FindKey(keySymbol, create);
    }

    public int GetInt( string keyName, int defaultValue = 0 )
    {
        var key = FindKey(keyName);
        return key == null ? defaultValue : key->DataType != KeyValuesDataType.TYPE_INT ? defaultValue : key->IntegerValue;
    }

    public ulong GetUint64( string keyName, ulong defaultValue = 0 )
    {
        var key = FindKey(keyName);
        return key == null ? defaultValue : key->DataType != KeyValuesDataType.TYPE_UINT64 ? defaultValue : key->UInt64Value;
    }

    public float GetFloat( string keyName, float defaultValue = 0f )
    {
        var key = FindKey(keyName);
        return key == null ? defaultValue : key->DataType != KeyValuesDataType.TYPE_FLOAT ? defaultValue : key->FloatValue;
    }

    public string GetString( string keyName, string defaultValue = "" )
    {
        var key = FindKey(keyName);
        return key == null || key->DataType != KeyValuesDataType.TYPE_STRING
            ? defaultValue
            : key->AllocatedExternalMemory
            ? key->StringValue == nint.Zero ? string.Empty : Marshal.PtrToStringUTF8(key->StringValue)!
            : Marshal.PtrToStringUTF8((nint)key)!;
    }

    public nint GetPtr( string keyName, nint defaultValue = 0 )
    {
        var key = FindKey(keyName);
        return key == null ? defaultValue : key->DataType != KeyValuesDataType.TYPE_PTR ? defaultValue : key->PointerValue;
    }

    public Color GetColor( string keyName, Color defaultValue )
    {
        var key = FindKey(keyName);
        return key == null ? defaultValue : key->DataType != KeyValuesDataType.TYPE_COLOR ? defaultValue : key->ColorValue;
    }

    public bool GetBool( string keyName, bool defaultValue = false )
    {
        var intValue = GetInt(keyName, defaultValue ? 1 : 0);
        return intValue != 0;
    }

    public bool IsEmpty()
    {
        fixed (KeyValues* ptr = &this)
        {
            return ptr->SubKeyValue == null;
        }
    }

    public string GetName()
    {
        fixed (KeyValues* ptr = &this)
        {
            if (ptr == null) return string.Empty;
            var symbol = new HKeySymbol { Hash = (uint)ptr->KeyNameCaseSensitive };
            return symbol.Value;
        }
    }

    public void SetName( string name )
    {
        fixed (KeyValues* ptr = &this)
        {
            if (ptr == null) return;
            var symbol = new HKeySymbol();
            symbol.Value = name;
            ptr->KeyNameCaseSensitive = (int)symbol.Hash;
        }
    }

    public void SetString( string keyName, string value )
    {
        var key = FindKey(keyName, true);
        if (key == null) return;

        key->DataType = KeyValuesDataType.TYPE_STRING;
        key->StringValue = StringPool.Allocate(value);
        key->AllocatedExternalMemory = true;
    }

    public void SetInt( string keyName, int value )
    {
        var key = FindKey(keyName, true);
        if (key == null) return;

        key->DataType = KeyValuesDataType.TYPE_INT;
        key->IntegerValue = value;
    }

    public void SetFloat( string keyName, float value )
    {
        var key = FindKey(keyName, true);
        if (key == null) return;

        key->DataType = KeyValuesDataType.TYPE_FLOAT;
        key->FloatValue = value;
    }

    public void SetBool( string keyName, bool value )
    {
        SetInt(keyName, value ? 1 : 0);
    }

    public void SetPtr( string keyName, nint value )
    {
        var key = FindKey(keyName, true);
        if (key == null) return;

        key->DataType = KeyValuesDataType.TYPE_PTR;
        key->PointerValue = value;
    }

    public void SetColor( string keyName, Color value )
    {
        var key = FindKey(keyName, true);
        if (key == null) return;

        key->DataType = KeyValuesDataType.TYPE_COLOR;
        key->ColorValue = value;
    }

    public KeyValues* GetFirstSubKey()
    {
        fixed (KeyValues* ptr = &this)
        {
            return ptr != null ? ptr->SubKeyValue : null;
        }
    }

    public KeyValues* GetNextKey()
    {
        fixed (KeyValues* ptr = &this)
        {
            return ptr != null ? ptr->PeerKeyValue : null;
        }
    }

    public KeyValues* FindLastSubKey()
    {
        KeyValues* lastItem = null;
        fixed (KeyValues* ptr = &this)
        {
            for (var current = ptr != null ? SubKeyValue : null; current != null; current = current->PeerKeyValue)
            {
                lastItem = current;
            }
        }
        return lastItem;
    }

    public KeyValues* GetFirstTrueSubKey()
    {
        fixed (KeyValues* ptr = &this)
        {
            for (var current = ptr != null ? SubKeyValue : null; current != null; current = current->PeerKeyValue)
            {
                if (current->DataType != KeyValuesDataType.TYPE_NONE)
                {
                    return current;
                }
            }
            return null;
        }
    }

    public KeyValues* GetNextTrueSubKey()
    {
        fixed (KeyValues* ptr = &this)
        {
            for (var current = ptr != null ? ptr->PeerKeyValue : null; current != null; current = current->PeerKeyValue)
            {
                if (current->DataType != KeyValuesDataType.TYPE_NONE)
                {
                    return current;
                }
            }
            return null;
        }
    }

    public KeyValues* GetFirstValue()
    {
        return GetFirstTrueSubKey();
    }

    public KeyValues* GetNextValue()
    {
        return GetNextTrueSubKey();
    }

    public KeyValuesDataType GetDataType()
    {
        fixed (KeyValues* ptr = &this)
        {
            return ptr != null ? ptr->DataType : KeyValuesDataType.TYPE_NONE;
        }
    }

    public KeyValuesDataType GetDataType( string keyName )
    {
        var key = FindKey(keyName);
        return key == null ? KeyValuesDataType.TYPE_NONE : key->DataType;
    }
}