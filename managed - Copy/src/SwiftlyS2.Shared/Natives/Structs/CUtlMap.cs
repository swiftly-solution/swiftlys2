using System.Numerics;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct CUtlMap<TKey, TValue, TIndex> : IDisposable
    where TIndex : unmanaged, IBinaryInteger<TIndex>, IMinMaxValue<TIndex>
{
    public CUtlRBTree<CUtlMapTreeNode<TKey, TValue>, TIndex> Tree;

    public CUtlMap(TIndex growSize, TIndex initSize, CUtlRBTree<CUtlMapTreeNode<TKey, TValue>, TIndex>.LessFunc func)
    {
        Tree = new CUtlRBTree<CUtlMapTreeNode<TKey, TValue>, TIndex>(growSize, initSize, func);
    }

    public CUtlMap(CUtlRBTree<CUtlMapTreeNode<TKey, TValue>, TIndex>.LessFunc func)
    {
        Tree = new CUtlRBTree<CUtlMapTreeNode<TKey, TValue>, TIndex>(func);
    }

    public void EnsureCapacity(TIndex num)
    {
        Tree.EnsureCapacity(num);
    }

    public ref TValue this[TIndex idx] => ref Tree[idx].Element;
    public ref TKey Key(TIndex idx) => ref Tree[idx].Key;
    public int Count => (int)Tree.Count;
    public TIndex MaxElement => Tree.MaxElement;
    public bool IsValidIndex(TIndex idx) => Tree.IsValidIndex(idx);
    public bool IsValid() => Tree.IsValid();
    public TIndex InvalidIndex() => Tree.InvalidIndex();
    public void SetLessFunc(CUtlRBTree<CUtlMapTreeNode<TKey, TValue>, TIndex>.LessFunc func) => Tree.SetLessFunc(func);
    public TIndex Insert(TKey key, TValue element) => Tree.Insert(new CUtlMapTreeNode<TKey, TValue> { Key = key, Element = element });
    public TIndex Insert(TKey key) => Tree.Insert(new CUtlMapTreeNode<TKey, TValue> { Key = key });
    public TIndex Find(TKey key)
    {
        var node = new CUtlMapTreeNode<TKey, TValue> { Key = key };
        return Tree.Find(node);
    }
    public void RemoveAt(TIndex idx) => Tree.RemoveAt(idx);
    public bool Remove(TKey key)
    {
        var node = new CUtlMapTreeNode<TKey, TValue> { Key = key };
        return Tree.Remove(node);
    }
    public void RemoveAll() => Tree.RemoveAll();
    public void Purge() => Tree.Purge();
    public TIndex FirstInOrdered() => Tree.FirstInorder();
    public TIndex NextInOrdered(TIndex idx) => Tree.NextInorder(idx);
    public TIndex PrevInOrdered(TIndex idx) => Tree.PrevInorder(idx);
    public TIndex LastInOrdered() => Tree.LastInorder();

    public void Reinsert(TKey key, TIndex idx)
    {
        Tree[idx].Key = key;
        Tree.Reinsert(idx);
    }

    public TIndex InsertOrReplace(TKey key, TValue element)
    {
        var node = new CUtlMapTreeNode<TKey, TValue> { Key = key };
        var idx = Tree.Find(node);
        if (idx != Tree.InvalidIndex())
        {
            Tree[idx].Element = element;
            return idx;
        }
        return Tree.Insert(new CUtlMapTreeNode<TKey, TValue> { Key = key, Element = element });
    }

    public void Dispose()
    {
        Purge();
    }
}