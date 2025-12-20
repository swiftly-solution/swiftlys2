namespace SwiftlyS2.Shared.NetMessages;

public interface IRepeatedField
{
}

public interface IProtobufRepeatedFieldValueType<T> : IRepeatedField, IList<T>
{
}

public interface IProtobufRepeatedFieldSubMessageType<T> : IRepeatedField, IEnumerable<T> where T : ITypedProtobuf<T>
{
    public int Count { get; }

    public T Get( int index );
    public T Add();
    public void Clear();
}