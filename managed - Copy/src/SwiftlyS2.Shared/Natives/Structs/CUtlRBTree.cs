using System.Numerics;
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
    public delegate bool LessFunc(ref TValue lhs, ref TValue rhs);

    public LessFunc LFunc;
    public CUtlLeanVector<CUtlRBTreeNode<TKey, TValue>, TKey> Elements;
    public TKey Root;
    public TKey NumElements;
    public TKey FirstFree;
    public CUtlLeanVector<CUtlRBTreeNode<TKey, TValue>, TKey>.Iterator_t LastAlloc;

    public CUtlRBTree(TKey growSize, TKey initSize, LessFunc func)
    {
        LFunc = func;
        Elements = new CUtlLeanVector<CUtlRBTreeNode<TKey, TValue>, TKey>(growSize, initSize);
        Root = -TKey.One;
        NumElements = TKey.Zero;
        FirstFree = -TKey.One;
        LastAlloc = new(-TKey.One);
    }

    public CUtlRBTree(LessFunc func)
    {
        LFunc = func;
        Elements = new CUtlLeanVector<CUtlRBTreeNode<TKey, TValue>, TKey>(TKey.Zero, TKey.Zero);
        Root = -TKey.One;
        NumElements = TKey.Zero;
        FirstFree = -TKey.One;
        LastAlloc = new(-TKey.One);
    }

    public void EnsureCapacity(TKey num)
    {
        Elements.EnsureCapacity(int.CreateChecked(num), false);
    }
    public ref CUtlRBTreeLinks<TKey> Links(TKey i) => ref Elements[i].Links;

    public ref TKey Parent(TKey i) => ref Links(i).Parent;
    public ref TKey LeftChild(TKey i) => ref Links(i).Left;
    public ref TKey RightChild(TKey i) => ref Links(i).Right;
    public bool IsLeftChild(TKey i) => LeftChild(Parent(i)) == i;
    public bool IsRightChild(TKey i) => RightChild(Parent(i)) == i;
    public bool IsRoot(TKey i) => Root == i;
    public bool IsLeaf(TKey i) => LeftChild(i) == -TKey.One && RightChild(i) == -TKey.One;
    public bool IsValidIndex(TKey i)
    {
        if (!Elements.IsIdxValid(i))
            return false;

        if (i > Elements.Count - TKey.One)
            return false;

        return LeftChild(i) != i;
    }
    public TKey InvalidIndex() => -TKey.One;
    public int Depth() => Depth(Root);
    private int Depth(TKey i)
    {
        if (!IsValidIndex(i))
            return 0;

        int leftDepth = Depth(LeftChild(i));
        int rightDepth = Depth(RightChild(i));

        return Math.Max(leftDepth, rightDepth) + 1;
    }
    public void SetParent(TKey i, TKey p) => Parent(i) = p;
    public void SetLeftChild(TKey i, TKey l) => LeftChild(i) = l;
    public void SetRightChild(TKey i, TKey r) => RightChild(i) = r;
    public bool IsRed(TKey i) => Links(i).Tag == TKey.CreateChecked((int)NodeColor_t.RED);
    public bool IsBlack(TKey i) => Links(i).Tag == TKey.CreateChecked((int)NodeColor_t.BLACK);
    public NodeColor_t Color(TKey i) => (NodeColor_t)int.CreateChecked(Links(i).Tag);
    public void SetColor(TKey i, NodeColor_t c) => Links(i).Tag = TKey.CreateChecked((int)c);

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

    public void FreeNode(TKey i)
    {
        if (!IsValidIndex(i))
            throw new IndexOutOfRangeException($"Index {i} is out of range (0 - {Elements.Count - TKey.One})");

        SetLeftChild(i, i);
        SetRightChild(i, FirstFree);
        FirstFree = i;
    }

    public void RotateLeft(TKey elem)
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

    public void RotateRight(TKey elem)
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
    public void InsertRebalance(TKey elem)
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
                    }

                    SetColor(parent, NodeColor_t.BLACK);
                    SetColor(grandparent, NodeColor_t.RED);
                    RotateLeft(grandparent);
                }
            }
        }

        SetColor(Root, NodeColor_t.BLACK);
    }

    public void LinkToParent(TKey i, TKey parent, bool isLeft)
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

    public TKey InsertAt(TKey parent, bool leftchild)
    {
        TKey i = NewNode();
        LinkToParent(i, parent, leftchild);
        NumElements++;
        return i;
    }

    public void RemoveRebalance(TKey elem)
    {
        while (elem != Root && IsBlack(elem))
        {
            TKey parent = Parent(elem);

            if (elem == LeftChild(parent))
            {
                TKey sibling = RightChild(parent);
                if (IsRed(sibling))
                {
                    SetColor(sibling, NodeColor_t.BLACK);
                    SetColor(parent, NodeColor_t.RED);
                    RotateLeft(parent);

                    parent = Parent(elem);
                    sibling = RightChild(parent);
                }
                if (IsBlack(LeftChild(sibling)) && IsBlack(RightChild(sibling)))
                {
                    if (sibling != InvalidIndex())
                        SetColor(sibling, NodeColor_t.RED);
                    elem = parent;
                }
                else
                {
                    if (IsBlack(RightChild(sibling)))
                    {
                        SetColor(LeftChild(sibling), NodeColor_t.BLACK);
                        SetColor(sibling, NodeColor_t.RED);
                        RotateRight(sibling);

                        parent = Parent(elem);
                        sibling = RightChild(parent);
                    }
                    SetColor(sibling, Color(parent));
                    SetColor(parent, NodeColor_t.BLACK);
                    SetColor(RightChild(sibling), NodeColor_t.BLACK);
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

                    parent = Parent(elem);
                    sibling = LeftChild(parent);
                }
                if (IsBlack(RightChild(sibling)) && IsBlack(LeftChild(sibling)))
                {
                    if (sibling != InvalidIndex())
                        SetColor(sibling, NodeColor_t.RED);
                    elem = parent;
                }
                else
                {
                    if (IsBlack(LeftChild(sibling)))
                    {
                        SetColor(RightChild(sibling), NodeColor_t.BLACK);
                        SetColor(sibling, NodeColor_t.RED);
                        RotateLeft(sibling);

                        parent = Parent(elem);
                        sibling = LeftChild(parent);
                    }
                    SetColor(sibling, Color(parent));
                    SetColor(parent, NodeColor_t.BLACK);
                    SetColor(LeftChild(sibling), NodeColor_t.BLACK);
                    RotateRight(parent);
                    elem = Root;
                }
            }
        }
        SetColor(elem, NodeColor_t.BLACK);
    }

    public void Unlink(TKey elem)
    {
        if (elem != InvalidIndex())
        {
            TKey x, y;

            if ((LeftChild(elem) == InvalidIndex()) ||
                (RightChild(elem) == InvalidIndex()))
            {
                y = elem;
            }
            else
            {
                y = RightChild(elem);
                while (LeftChild(y) != InvalidIndex())
                    y = LeftChild(y);
            }

            if (LeftChild(y) != InvalidIndex())
                x = LeftChild(y);
            else
                x = RightChild(y);

            if (x != InvalidIndex())
                SetParent(x, Parent(y));
            if (!IsRoot(y))
            {
                if (IsLeftChild(y))
                    SetLeftChild(Parent(y), x);
                else
                    SetRightChild(Parent(y), x);
            }
            else
                Root = x;

            NodeColor_t ycolor = Color(y);
            if (y != elem)
            {
                SetParent(y, Parent(elem));
                SetRightChild(y, RightChild(elem));
                SetLeftChild(y, LeftChild(elem));

                if (!IsRoot(elem))
                    if (IsLeftChild(elem))
                        SetLeftChild(Parent(elem), y);
                    else
                        SetRightChild(Parent(elem), y);
                else
                    Root = y;

                if (LeftChild(y) != InvalidIndex())
                    SetParent(LeftChild(y), y);
                if (RightChild(y) != InvalidIndex())
                    SetParent(RightChild(y), y);

                SetColor(y, Color(elem));
            }

            if ((x != InvalidIndex()) && (ycolor == NodeColor_t.BLACK))
                RemoveRebalance(x);
        }
    }

    public void Link(TKey elem)
    {
        if (elem != InvalidIndex())
        {
            FindInsertionPosition(this[elem], out TKey parent, out bool leftchild);

            LinkToParent(elem, parent, leftchild);
        }
    }

    void FindInsertionPosition(TValue val, out TKey parent, out bool leftchild)
    {
        parent = InvalidIndex();
        leftchild = false;

        TKey current = Root;
        while (IsValidIndex(current))
        {
            parent = current;
            if (LFunc(ref val, ref this[current]))
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

    public void RemoveAt(TKey elem)
    {
        if (!IsValidIndex(elem))
            return;

        Unlink(elem);
        FreeNode(elem);
        --NumElements;
    }

    public bool Remove(TValue value)
    {
        TKey node = Find(value);
        if (node != -TKey.One)
            RemoveAt(node);

        return node != -TKey.One;
    }

    public TKey Find(TValue value)
    {
        TKey current = Root;
        while (IsValidIndex(current))
        {
            if (LFunc(ref value, ref this[current]))
            {
                current = LeftChild(current);
            }
            else if (LFunc(ref this[current], ref value))
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

        for (TKey i = TKey.Zero; i <= LastAlloc.Index; i++)
        {
            if (IsValidIndex(i))
            {
                SetRightChild(i, FirstFree);
                SetLeftChild(i, i);
                FirstFree = i;
            }

            Elements[i] = default;
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

    public TKey NextInorder(TKey i)
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

    public TKey PrevInorder(TKey i)
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

    public TKey NextPreorder(TKey i)
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

    public TKey PrevPreorder(TKey i) => -TKey.One;

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

    public TKey NextPostorder(TKey i)
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

    public void Reinsert(TKey i)
    {
        if (!IsValidIndex(i))
            return;

        Unlink(i);
        Link(i);
    }

    public bool IsValid()
    {
        if (Count == 0)
            return true;

        if (LastAlloc.Index == -TKey.One)
            return false;

        if (!Elements.IsIdxValid(Root))
            return false;

        if (Parent(Root) != InvalidIndex())
            return false;

        return true;
    }

    public void SetLessFunc(LessFunc func)
    {
        LFunc = func;
    }

    public TKey Insert(TValue val)
    {
        FindInsertionPosition(val, out TKey parent, out bool leftchild);

        TKey newNode = InsertAt(parent, leftchild);
        this[newNode] = val;
        return newNode;
    }

    public TKey InsertIfNotFound(TValue val)
    {
        TKey node = Find(val);
        if (node == -TKey.One)
            node = Insert(val);

        return node;
    }

    public ref TValue this[TKey i] => ref Elements[i].Data;
    public uint Count => uint.CreateChecked(NumElements);
    public TKey MaxElement => TKey.CreateChecked(Elements.NumAllocated);
}