using System.Collections;
using SwiftlyS2.Core.Natives.NativeObjects;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Core.NetMessages;

internal class ProtobufRepeatedFieldValueType<T> : IProtobufRepeatedFieldValueType<T>
{
  private IProtobufAccessor _Protobuf { get; init; }
  private string _FieldName { get; init; }
  public ProtobufRepeatedFieldValueType(IProtobufAccessor protobuf, string fieldName)
  {
    _Protobuf = protobuf;
    _FieldName = fieldName;
  }

  public T this[int index] { 
    get => _Protobuf.GetRepeated<T>(_FieldName, index);
    set => _Protobuf.SetRepeated<T>(_FieldName, index, value);
  }

  public int Count => _Protobuf.GetRepeatedFieldSize(_FieldName);

  public bool IsReadOnly => false;

  public void Add(T item)
  {
    _Protobuf.Add<T>(_FieldName, item);
  }

  public void Clear()
  {
    _Protobuf.ClearRepeatedField(_FieldName);
  }

  public bool Contains(T item)
  {
    for (int i = 0; i < Count; i++) {
      if (this[i].Equals(item)) {
        return true;
      }
    }
    return false;
  }

  public void CopyTo(T[] array, int arrayIndex)
  {
    for (int i = 0; i < Count; i++) {
      array[arrayIndex + i] = this[i];
    }
  }

  public IEnumerator<T> GetEnumerator()
  {
    foreach (var item in this) {
      yield return item;
    }
  }

  public int IndexOf(T item)
  {
    for (int i = 0; i < Count; i++) {
      if (this[i].Equals(item)) {
        return i;
      }
    }
    return -1;
  }

  public void Insert(int index, T item)
  {
    _Protobuf.Add<T>(_FieldName, item);
  }

  public bool Remove(T item)
  {
    throw new NotSupportedException("Removing element from a protobuf repeated field is not supported.");
  }

  public void RemoveAt(int index)
  {
    throw new NotSupportedException("Removing element from a protobuf repeated field is not supported.");
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
      return GetEnumerator();
  }
}


internal class ProtobufRepeatedFieldSubMessageType<T> : IProtobufRepeatedFieldSubMessageType<T> where T : ITypedProtobuf<T>
{
  private IProtobufAccessor _Protobuf { get; init; }
  private string _FieldName { get; init; }

  public int Count => _Protobuf.GetRepeatedFieldSize(_FieldName);

  public ProtobufRepeatedFieldSubMessageType(IProtobufAccessor protobuf, string fieldName)
  {
    _Protobuf = protobuf;
    _FieldName = fieldName;
  }

  public T Get(int index)
  {
    return T.Wrap(_Protobuf.GetRepeatedNestedMessage(_FieldName, index), false);
  }
  public T Add()
  {
    return T.Wrap(_Protobuf.AddNestedMessage(_FieldName), false);
  }

  public IEnumerator<T> GetEnumerator()
  {
    for (int i = 0; i < Count; i++) {
      yield return Get(i);
    }
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
      return GetEnumerator();
  }
}