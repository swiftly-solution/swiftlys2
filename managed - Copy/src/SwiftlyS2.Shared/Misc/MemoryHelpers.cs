using System.Runtime.CompilerServices;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Shared.Misc;

public static class MemoryHelpers
{
    public static int CalcNewDoublingCount(int oldCount, int requestedCount, int minCount, int maxCount)
    {
        int newCount = oldCount;

        while (newCount < requestedCount)
        {
            if (newCount < maxCount / 2)
            {
                newCount *= 2;
                if (newCount < minCount)
                    newCount = minCount;
            }
            else
            {
                newCount = maxCount;
                break;
            }
        }

        return newCount;
    }

    public static void ShiftElementsRight(nint memory, int elem, int num, int size, int elementSize)
    {
        int numToMove = size - elem - num;
        if (numToMove > 0 && num > 0)
        {
            NativeAllocator.Move(memory + ((elem + num) * elementSize), memory + (elem * elementSize), (ulong)(numToMove * elementSize));
        }
    }

    public static void ShiftElementsLeft(nint memory, int elem, int num, int size, int elementSize)
    {
        int numToMove = size - elem - num;
        if (numToMove > 0 && num > 0)
        {
            NativeAllocator.Move(memory + (elem * elementSize), memory + ((elem + num) * elementSize), (ulong)(numToMove * elementSize));
        }
    }
}