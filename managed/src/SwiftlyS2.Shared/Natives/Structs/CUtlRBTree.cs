using System.Collections.Concurrent;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

/** I don't want it any more, i don't like this file */

public enum NodeColor_t
{
    RED = 0,
    BLACK
};

[StructLayout(LayoutKind.Sequential)]
public struct CUtlRBTree<TValue, TKey> : IDisposable
    where TKey : unmanaged, IBinaryInteger<TKey>, IMinMaxValue<TKey>
{
    public delegate bool LessFunc( ref TValue lhs, ref TValue rhs );

    public nint LFunc;
    public CUtlLeanVector<CUtlRBTreeNode<TKey, TValue>, TKey> Elements;
    public TKey Root;
    public TKey NumElements;
    public TKey FirstFree;
    public CUtlLeanVector<CUtlRBTreeNode<TKey, TValue>, TKey>.Iterator_t LastAlloc;

    public CUtlRBTree( TKey growSize, TKey initSize, LessFunc func )
    {
        ArgumentNullException.ThrowIfNull(func);
        LFunc = 0;
        Elements = new CUtlLeanVector<CUtlRBTreeNode<TKey, TValue>, TKey>(growSize, initSize);
        Root = -TKey.One;
        NumElements = TKey.Zero;
        FirstFree = -TKey.One;
        LastAlloc = new(-TKey.One);
        try
        {
            SetLessFunc(func);
        }
        catch
        {
            Elements.Purge();
            throw;
        }
    }

    public CUtlRBTree( LessFunc func ) : this(TKey.Zero, TKey.Zero, func)
    {
    }

    public void EnsureCapacity( TKey num )
    {
        Elements.EnsureCapacity(int.CreateChecked(num), false);
    }
    public ref CUtlRBTreeLinks<TKey> Links( TKey i ) => ref Elements[i].Links;

    public ref TKey Parent( TKey i ) => ref Links(i).Parent;
    public ref TKey LeftChild( TKey i ) => ref Links(i).Left;
    public ref TKey RightChild( TKey i ) => ref Links(i).Right;
    public bool IsLeftChild( TKey i ) => i != InvalidIndex() && Parent(i) != InvalidIndex() && LeftChild(Parent(i)) == i;
    public bool IsRightChild( TKey i ) => i != InvalidIndex() && Parent(i) != InvalidIndex() && RightChild(Parent(i)) == i;
    public bool IsRoot( TKey i ) => Root == i;
    public bool IsLeaf( TKey i ) => LeftChild(i) == -TKey.One && RightChild(i) == -TKey.One;
    public bool IsValidIndex( TKey i )
    {
        return Elements.IsIdxValid(i) && i <= Elements.Count - TKey.One && LeftChild(i) != i;
    }
    public TKey InvalidIndex() => -TKey.One;
    public int Depth() => Depth(Root);
    private int Depth( TKey i )
    {
        if (!IsValidIndex(i))
            return 0;

        int leftDepth = Depth(LeftChild(i));
        int rightDepth = Depth(RightChild(i));

        return Math.Max(leftDepth, rightDepth) + 1;
    }
    public void SetParent( TKey i, TKey p ) => Parent(i) = p;
    public void SetLeftChild( TKey i, TKey l ) => LeftChild(i) = l;
    public void SetRightChild( TKey i, TKey r ) => RightChild(i) = r;
    public bool IsRed( TKey i ) => i != InvalidIndex() && Links(i).Tag == TKey.CreateChecked((int)NodeColor_t.RED);
    public bool IsBlack( TKey i ) => i == InvalidIndex() || Links(i).Tag == TKey.CreateChecked((int)NodeColor_t.BLACK);
    public NodeColor_t Color( TKey i ) => i == InvalidIndex() ? NodeColor_t.BLACK : (NodeColor_t)int.CreateChecked(Links(i).Tag);
    public void SetColor( TKey i, NodeColor_t c )
    {
        if (i != InvalidIndex())
            Links(i).Tag = TKey.CreateChecked((int)c);
    }

    public TKey NewNode()
    {
        TKey elem;

        if (FirstFree == InvalidIndex())
        {
            LastAlloc = new(Elements.AddToTail());
            elem = LastAlloc.Index;
        }
        else
        {
            elem = FirstFree;
            FirstFree = RightChild(FirstFree);
        }

        return elem;
    }

    public void FreeNode( TKey i )
    {
        if (!IsValidIndex(i))
            throw new IndexOutOfRangeException($"Index {i} is out of range (0 - {Elements.Count - TKey.One})");

        SetLeftChild(i, i);
        SetRightChild(i, FirstFree);
        FirstFree = i;
    }

    public void RotateLeft( TKey elem )
    {
        TKey right = RightChild(elem);
        SetRightChild(elem, LeftChild(right));
        if (LeftChild(right) != -TKey.One)
            SetParent(LeftChild(right), elem);

        if (right != -TKey.One)
            SetParent(right, Parent(elem));
        if (!IsRoot(elem))
        {
            if (IsLeftChild(elem))
                SetLeftChild(Parent(elem), right);
            else
                SetRightChild(Parent(elem), right);
        }
        else
        {
            Root = right;
        }

        SetLeftChild(right, elem);
        if (elem != -TKey.One)
            SetParent(elem, right);
    }

    public void RotateRight( TKey elem )
    {
        TKey left = LeftChild(elem);
        SetLeftChild(elem, RightChild(left));
        if (RightChild(left) != -TKey.One)
            SetParent(RightChild(left), elem);

        if (left != -TKey.One)
            SetParent(left, Parent(elem));
        if (!IsRoot(elem))
        {
            if (IsLeftChild(elem))
                SetLeftChild(Parent(elem), left);
            else
                SetRightChild(Parent(elem), left);
        }
        else
        {
            Root = left;
        }

        SetRightChild(left, elem);
        if (elem != -TKey.One)
            SetParent(elem, left);
    }

    // i hate RB trees
    public void InsertRebalance( TKey elem )
    {
        SetColor(elem, NodeColor_t.RED);

        while (elem != Root && IsRed(Parent(elem)))
        {
            TKey parent = Parent(elem);
            TKey grandparent = Parent(parent);

            if (IsLeftChild(parent))
            {
                TKey uncle = RightChild(grandparent);
                if (IsValidIndex(uncle) && IsRed(uncle))
                {
                    SetColor(parent, NodeColor_t.BLACK);
                    SetColor(uncle, NodeColor_t.BLACK);
                    SetColor(grandparent, NodeColor_t.RED);
                    elem = grandparent;
                }
                else
                {
                    if (IsRightChild(elem))
                    {
                        elem = parent;
                        RotateLeft(elem);
                        parent = Parent(elem);
                        grandparent = Parent(parent);
                    }

                    SetColor(parent, NodeColor_t.BLACK);
                    SetColor(grandparent, NodeColor_t.RED);
                    RotateRight(grandparent);
                }
            }
            else
            {
                TKey uncle = LeftChild(grandparent);
                if (IsValidIndex(uncle) && IsRed(uncle))
                {
                    SetColor(parent, NodeColor_t.BLACK);
                    SetColor(uncle, NodeColor_t.BLACK);
                    SetColor(grandparent, NodeColor_t.RED);
                    elem = grandparent;
                }
                else
                {
                    if (IsLeftChild(elem))
                    {
                        elem = parent;
                        RotateRight(elem);
                        parent = Parent(elem);
                        grandparent = Parent(parent);
                    }

                    SetColor(parent, NodeColor_t.BLACK);
                    SetColor(grandparent, NodeColor_t.RED);
                    RotateLeft(grandparent);
                }
            }
        }

        SetColor(Root, NodeColor_t.BLACK);
    }

    public void LinkToParent( TKey i, TKey parent, bool isLeft )
    {
        Links(i).Parent = parent;
        Links(i).Left = -TKey.One;
        Links(i).Right = -TKey.One;
        Links(i).Tag = TKey.CreateChecked((int)NodeColor_t.RED);

        if (parent != -TKey.One)
        {
            if (isLeft)
                Links(parent).Left = i;
            else
                Links(parent).Right = i;
        }
        else
        {
            Root = i;
        }

        InsertRebalance(i);
    }

    public TKey InsertAt( TKey parent, bool leftchild )
    {
        TKey i = NewNode();
        LinkToParent(i, parent, leftchild);
        NumElements++;
        return i;
    }

    public void RemoveRebalance( TKey elem )
    {
        RemoveRebalance(elem, elem == InvalidIndex() ? InvalidIndex() : Parent(elem));
    }

    private TKey LeftOf( TKey index ) => index == InvalidIndex() ? InvalidIndex() : LeftChild(index);
    private TKey RightOf( TKey index ) => index == InvalidIndex() ? InvalidIndex() : RightChild(index);

    private void RemoveRebalance( TKey elem, TKey parent )
    {
        while (elem != Root && IsBlack(elem))
        {
            if (parent == InvalidIndex()) break;

            if (elem == LeftChild(parent))
            {
                TKey sibling = RightChild(parent);
                if (IsRed(sibling))
                {
                    SetColor(sibling, NodeColor_t.BLACK);
                    SetColor(parent, NodeColor_t.RED);
                    RotateLeft(parent);

                    sibling = RightChild(parent);
                }
                if (IsBlack(LeftOf(sibling)) && IsBlack(RightOf(sibling)))
                {
                    if (sibling != InvalidIndex())
                        SetColor(sibling, NodeColor_t.RED);
                    elem = parent;
                    parent = Parent(elem);
                }
                else
                {
                    if (IsBlack(RightOf(sibling)))
                    {
                        SetColor(LeftOf(sibling), NodeColor_t.BLACK);
                        SetColor(sibling, NodeColor_t.RED);
                        RotateRight(sibling);

                        sibling = RightChild(parent);
                    }
                    SetColor(sibling, Color(parent));
                    SetColor(parent, NodeColor_t.BLACK);
                    SetColor(RightOf(sibling), NodeColor_t.BLACK);
                    RotateLeft(parent);
                    elem = Root;
                }
            }
            else
            {
                TKey sibling = LeftChild(parent);
                if (IsRed(sibling))
                {
                    SetColor(sibling, NodeColor_t.BLACK);
                    SetColor(parent, NodeColor_t.RED);
                    RotateRight(parent);

                    sibling = LeftChild(parent);
                }
                if (IsBlack(RightOf(sibling)) && IsBlack(LeftOf(sibling)))
                {
                    if (sibling != InvalidIndex())
                        SetColor(sibling, NodeColor_t.RED);
                    elem = parent;
                    parent = Parent(elem);
                }
                else
                {
                    if (IsBlack(LeftOf(sibling)))
                    {
                        SetColor(RightOf(sibling), NodeColor_t.BLACK);
                        SetColor(sibling, NodeColor_t.RED);
                        RotateLeft(sibling);

                        sibling = LeftChild(parent);
                    }
                    SetColor(sibling, Color(parent));
                    SetColor(parent, NodeColor_t.BLACK);
                    SetColor(LeftOf(sibling), NodeColor_t.BLACK);
                    RotateRight(parent);
                    elem = Root;
                }
            }
        }
        SetColor(elem, NodeColor_t.BLACK);
    }

    public void Unlink( TKey elem )
    {
        if (elem == InvalidIndex()) return;

        var removed = elem;
        var removedColor = Color(removed);
        TKey replacement;
        TKey replacementParent;

        if (LeftChild(elem) == InvalidIndex())
        {
            replacement = RightChild(elem);
            replacementParent = Parent(elem);
            Transplant(elem, replacement);
        }
        else if (RightChild(elem) == InvalidIndex())
        {
            replacement = LeftChild(elem);
            replacementParent = Parent(elem);
            Transplant(elem, replacement);
        }
        else
        {
            removed = NextInorder(elem);
            removedColor = Color(removed);
            replacement = RightChild(removed);
            if (Parent(removed) == elem)
            {
                replacementParent = removed;
                if (replacement != InvalidIndex()) SetParent(replacement, removed);
            }
            else
            {
                replacementParent = Parent(removed);
                Transplant(removed, replacement);
                SetRightChild(removed, RightChild(elem));
                SetParent(RightChild(removed), removed);
            }
            Transplant(elem, removed);
            SetLeftChild(removed, LeftChild(elem));
            SetParent(LeftChild(removed), removed);
            SetColor(removed, Color(elem));
        }

        if (removedColor == NodeColor_t.BLACK)
            RemoveRebalance(replacement, replacementParent);
    }

    private void Transplant( TKey previous, TKey replacement )
    {
        var parent = Parent(previous);
        if (parent == InvalidIndex()) Root = replacement;
        else if (LeftChild(parent) == previous) SetLeftChild(parent, replacement);
        else SetRightChild(parent, replacement);

        if (replacement != InvalidIndex()) SetParent(replacement, parent);
    }

    public void Link( TKey elem )
    {
        if (elem != InvalidIndex())
        {
            FindInsertionPosition(this[elem], out TKey parent, out bool leftchild);

            LinkToParent(elem, parent, leftchild);
        }
    }

    public void FindInsertionPosition( TValue val, out TKey parent, out bool leftchild )
    {
        parent = InvalidIndex();
        leftchild = false;
        var current = Root;
        while (IsValidIndex(current))
        {
            parent = current;
            if (Less(ref val, ref this[current]))
            {
                leftchild = true;
                current = LeftChild(current);
            }
            else
            {
                leftchild = false;
                current = RightChild(current);
            }
        }
    }

    public void RemoveAt( TKey elem )
    {
        if (!IsValidIndex(elem))
            return;

        Unlink(elem);
        FreeNode(elem);
        --NumElements;
    }

    public bool Remove( TValue value )
    {
        TKey node = Find(value);
        if (node != -TKey.One)
            RemoveAt(node);

        return node != -TKey.One;
    }

    public TKey Find( TValue value )
    {
        TKey current = Root;
        while (IsValidIndex(current))
        {
            if (Less(ref value, ref this[current]))
            {
                current = LeftChild(current);
            }
            else if (Less(ref this[current], ref value))
            {
                current = RightChild(current);
            }
            else
            {
                return current;
            }
        }

        return -TKey.One;
    }

    public void RemoveAll()
    {
        if (LastAlloc.Index == -TKey.One)
            return;

        for (var i = TKey.Zero; i <= LastAlloc.Index; i++)
        {
            if (IsValidIndex(i))
            {
                SetRightChild(i, FirstFree);
                SetLeftChild(i, i);
                FirstFree = i;
            }

            Elements[i].Data = default!;
            if (i == LastAlloc.Index)
                break;
        }

        Root = -TKey.One;
        NumElements = TKey.Zero;
    }

    public void Purge()
    {
        RemoveAll();
        FirstFree = -TKey.One;
        Elements.Purge();
        LastAlloc = new(-TKey.One);
    }

    public void Dispose()
    {
        Purge();
        NativeRBTreeComparers.Release(LFunc);
        LFunc = 0;
    }

    public TKey FirstInorder()
    {
        TKey current = Root;
        if (!IsValidIndex(current))
            return -TKey.One;

        while (IsValidIndex(LeftChild(current)))
            current = LeftChild(current);

        return current;
    }

    public TKey NextInorder( TKey i )
    {
        if (!IsValidIndex(i))
            return -TKey.One;

        if (IsValidIndex(RightChild(i)))
        {
            i = RightChild(i);
            while (IsValidIndex(LeftChild(i)))
                i = LeftChild(i);
            return i;
        }

        TKey parent = Parent(i);
        while (IsValidIndex(parent) && i == RightChild(parent))
        {
            i = parent;
            parent = Parent(i);
        }

        return parent;
    }

    public TKey PrevInorder( TKey i )
    {
        if (!IsValidIndex(i))
            return -TKey.One;

        if (IsValidIndex(LeftChild(i)))
        {
            i = LeftChild(i);
            while (IsValidIndex(RightChild(i)))
                i = RightChild(i);
            return i;
        }

        TKey parent = Parent(i);
        while (IsValidIndex(parent) && i == LeftChild(parent))
        {
            i = parent;
            parent = Parent(i);
        }

        return parent;
    }

    public TKey LastInorder()
    {
        TKey current = Root;
        if (!IsValidIndex(current))
            return -TKey.One;

        while (IsValidIndex(RightChild(current)))
            current = RightChild(current);

        return current;
    }

    public TKey FirstPreorder()
    {
        return Root;
    }

    public TKey NextPreorder( TKey i )
    {
        if (!IsValidIndex(i))
            return -TKey.One;

        if (IsValidIndex(LeftChild(i)))
            return LeftChild(i);

        if (IsValidIndex(RightChild(i)))
            return RightChild(i);

        TKey parent = Parent(i);
        while (IsValidIndex(parent))
        {
            if (IsLeftChild(i) && IsValidIndex(RightChild(parent)))
                return RightChild(parent);

            i = parent;
            parent = Parent(i);
        }

        return -TKey.One;
    }

    public TKey PrevPreorder( TKey i ) => -TKey.One;

    public TKey LastPreorder()
    {
        TKey current = Root;
        if (!IsValidIndex(current))
            return -TKey.One;

        while (true)
        {
            if (IsValidIndex(RightChild(current)))
                current = RightChild(current);
            else if (IsValidIndex(LeftChild(current)))
                current = LeftChild(current);
            else
                break;
        }

        return current;
    }

    public TKey FirstPostorder()
    {
        TKey current = Root;
        if (!IsValidIndex(current))
            return -TKey.One;

        while (!IsLeaf(current))
        {
            if (IsValidIndex(LeftChild(current)))
                current = LeftChild(current);
            else if (IsValidIndex(RightChild(current)))
                current = RightChild(current);
        }

        return current;
    }

    public TKey NextPostorder( TKey i )
    {
        if (!IsValidIndex(i))
            return -TKey.One;

        if (IsRoot(i))
            return -TKey.One;

        TKey parent = Parent(i);
        if (IsLeftChild(i) && IsValidIndex(RightChild(parent)))
        {
            i = RightChild(parent);
            while (!IsLeaf(i))
            {
                if (IsValidIndex(LeftChild(i)))
                    i = LeftChild(i);
                else if (IsValidIndex(RightChild(i)))
                    i = RightChild(i);
            }
            return i;
        }

        return parent;
    }

    public void Reinsert( TKey i )
    {
        if (!IsValidIndex(i))
            return;

        Unlink(i);
        Link(i);
    }

    public bool IsValid()
    {
        return Count == 0 || (LastAlloc.Index != -TKey.One && Elements.IsIdxValid(Root) && Parent(Root) == InvalidIndex());
    }

    public void SetLessFunc( LessFunc func )
    {
        ArgumentNullException.ThrowIfNull(func);
        unsafe
        {
            NativeRBTreeLessFunc callback = (left, right) => func(
                ref Unsafe.AsRef<TValue>((void*)left),
                ref Unsafe.AsRef<TValue>((void*)right)) ? (byte)1 : (byte)0;
            var pointer = NativeRBTreeComparers.Register(callback);
            NativeRBTreeComparers.Release(LFunc);
            LFunc = pointer;
        }
    }

    private unsafe bool Less( ref TValue left, ref TValue right )
    {
        if (LFunc == 0) throw new InvalidOperationException("The tree has no comparison function.");
        fixed (byte* leftPointer = &Unsafe.As<TValue, byte>(ref left))
        fixed (byte* rightPointer = &Unsafe.As<TValue, byte>(ref right))
        {
            return ((delegate* unmanaged[Cdecl]<nint, nint, byte>)LFunc)((nint)leftPointer, (nint)rightPointer) != 0;
        }
    }

    public TKey Insert( TValue val )
    {
        FindInsertionPosition(val, out TKey parent, out bool leftchild);

        TKey newNode = InsertAt(parent, leftchild);
        this[newNode] = val;
        return newNode;
    }

    public TKey InsertIfNotFound( TValue val )
    {
        var node = Find(val);
        if (node == -TKey.One)
            node = Insert(val);

        return node;
    }

    public ref TValue this[TKey i] => ref Elements[i].Data;
    public readonly uint Count => uint.CreateChecked(NumElements);
    public TKey MaxElement => TKey.CreateChecked(Elements.NumAllocated);
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate byte NativeRBTreeLessFunc( nint left, nint right );

internal static class NativeRBTreeComparers
{
    private static readonly ConcurrentDictionary<nint, NativeRBTreeLessFunc> callbacks = new();

    internal static nint Register( NativeRBTreeLessFunc callback )
    {
        var pointer = Marshal.GetFunctionPointerForDelegate(callback);
        callbacks[pointer] = callback;
        return pointer;
    }

    internal static void Release( nint pointer ) => callbacks.TryRemove(pointer, out _);
}
