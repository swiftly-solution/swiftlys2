using System.Collections;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Core.NetMessages;

internal class ProtobufRepeatedFieldValueType<T>( IProtobufAccessor protobuf, string fieldName ) : IProtobufRepeatedFieldValueType<T>
{
    public int Count => protobuf.GetRepeatedFieldSize(fieldName);

    public bool IsReadOnly => false;

    public T this[int index] {
        get => protobuf.GetRepeated<T>(fieldName, index);
        set => protobuf.SetRepeated<T>(fieldName, index, value);
    }

    public void Add( T item )
    {
        protobuf.Add<T>(fieldName, item);
    }

    public void Clear()
    {
        protobuf.ClearRepeatedField(fieldName);
    }

    public bool Contains( T item )
    {
        for (var i = 0; i < Count; i++)
        {
            if (this[i]?.Equals(item) ?? false)
            {
                return true;
            }
        }
        return false;
    }

    public void CopyTo( T[] array, int arrayIndex )
    {
        for (var i = 0; i < Count; i++)
        {
            array[arrayIndex + i] = this[i];
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in this)
        {
            yield return item;
        }
    }

    public int IndexOf( T item )
    {
        for (var i = 0; i < Count; i++)
        {
            if (this[i]?.Equals(item) ?? false)
            {
                return i;
            }
        }
        return -1;
    }

    public void Insert( int index, T item )
    {
        protobuf.Add<T>(fieldName, item);
    }

    public bool Remove( T item )
    {
        throw new NotSupportedException("Removing element from a protobuf repeated field is not supported.");
    }

    public void RemoveAt( int index )
    {
        throw new NotSupportedException("Removing element from a protobuf repeated field is not supported.");
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}


internal class ProtobufRepeatedFieldSubMessageType<T>( IProtobufAccessor protobuf, string fieldName ) : IProtobufRepeatedFieldSubMessageType<T> where T : ITypedProtobuf<T>
{
    public int Count => protobuf.GetRepeatedFieldSize(fieldName);

    public T Get( int index )
    {
        return T.Wrap(protobuf.GetRepeatedNestedMessage(fieldName, index), false);
    }

    public T Add()
    {
        return T.Wrap(protobuf.AddNestedMessage(fieldName), false);
    }

    public void Clear()
    {
        protobuf.ClearRepeatedField(fieldName);
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var i = 0; i < Count; i++)
        {
            yield return Get(i);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}